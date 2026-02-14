using System.Net.Http.Headers;
using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api.NavI;
using QuantumAE.Api.Sessions;
using QuantumAE.Api.Orders;
using QuantumAE.Models;

namespace QuantumAE;

public enum TApiFormat
{
  Json,
  MessagePack
}

/// <summary>
///   hu: QuantumAE API kliens (Json/MessagePack tamogatással).
///   Session kezeléssel (Connect/Disconnect) és automatikus Session-Id header küldéssel.
///   <br />
///   en: QuantumAE API client (Json/MessagePack support).
///   With session management (Connect/Disconnect) and automatic Session-Id header sending.
/// </summary>
public sealed class TQuantumAeApiClient : IDisposable
{
  private readonly HttpClient FHttp;
  private readonly string FBaseUrl;
  private readonly TApiFormat FFormat;
  private readonly JsonSerializerOptions FJson;
  private readonly MessagePackSerializerOptions FMsgpack;

  /// <summary>
  ///   hu: Az aktív session azonosítója (Connect után kerül beállításra).
  ///   <br />
  ///   en: The active session identifier (set after Connect).
  /// </summary>
  public string? SessionId { get; private set; }

  /// <summary>
  ///   hu: A pénztárgép AP száma (Connect után kerül beállításra).
  ///   <br />
  ///   en: The cash register AP number (set after Connect).
  /// </summary>
  public string? ApNumber { get; private set; }

  public TQuantumAeApiClient(string ABaseUrl, TApiFormat AFormat = TApiFormat.Json, HttpMessageHandler? AHandler = null)
  {
    if (string.IsNullOrWhiteSpace(ABaseUrl)) throw new ArgumentException("Base URL is required", nameof(ABaseUrl));
    FBaseUrl = ABaseUrl.TrimEnd('/');
    FFormat = AFormat;
    FHttp = AHandler is null ? new HttpClient() : new HttpClient(AHandler, disposeHandler: false);

    FJson = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      WriteIndented = false,
      DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    FMsgpack = MessagePackSerializerOptions.Standard
      .WithResolver(ContractlessStandardResolver.Instance);
  }

  public void Dispose()
  {
    FHttp.Dispose();
  }

  #region Session Management..

  /// <summary>
  ///   hu: Csatlakozás az Adóügyi Egységhez (session létrehozás).
  ///   <br />
  ///   en: Connect to the Tax Unit (session creation).
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="AApNumber">
  ///   hu: A pénztárgép AP száma.
  ///   <br />
  ///   en: The cash register AP number.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<ConnectResponse> ConnectAsync(string ARequestId, string AApNumber, CancellationToken ct = default)
  {
    var request = new ConnectRequest(ARequestId, AApNumber);
    var response = await PostAsync<ConnectRequest, ConnectResponse>("/sessions/connect", request, ct).ConfigureAwait(false);

    if (response.ResultCode == 0)
    {
      SessionId = response.SessionId;
      ApNumber = AApNumber;
    }

    return response;
  }

  /// <summary>
  ///   hu: Kapcsolat bontása (session lezárás).
  ///   <br />
  ///   en: Disconnect (session close).
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<DisconnectResponse> DisconnectAsync(string ARequestId, CancellationToken ct = default)
  {
    var request = new DisconnectRequest(ARequestId);
    var response = await PostAsync<DisconnectRequest, DisconnectResponse>("/sessions/disconnect", request, ct).ConfigureAwait(false);

    if (response.ResultCode == 0)
    {
      SessionId = null;
      ApNumber = null;
    }

    return response;
  }

  #endregion

  #region Order API..

  public async Task<OpenResponse> OpenAsync(OpenRequest ARequest, CancellationToken ct = default)
    => await PostAsync<OpenRequest, OpenResponse>("/orders/open", ARequest, ct).ConfigureAwait(false);

  public async Task<ItemsAddResponse> ItemAddAsync(ItemsAddRequest ARequest, CancellationToken ct = default)
    => await PostAsync<ItemsAddRequest, ItemsAddResponse>("/orders/items", ARequest, ct).ConfigureAwait(false);

  #endregion

  #region NAV-I API..

  /// <summary>
  ///   hu: Adószám lekérdezése a NAV-tól.
  ///   <br />
  ///   en: Query taxpayer data from NAV.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Adószám lekérdezési kérés.
  ///   <br />
  ///   en: Taxpayer query request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<QueryTaxpayerResponse> QueryTaxpayerAsync(QueryTaxpayerRequest ARequest, CancellationToken ct = default)
    => await PostAsync<QueryTaxpayerRequest, QueryTaxpayerResponse>("/navi/queryTaxpayer", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Üzemeltetés befejezése - a pénztárgép üzemen kívül helyezése a NAV-I felé.
  ///   <br />
  ///   en: End of operation - deactivate the cash register towards NAV-I.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Üzemeltetés befejezési kérés.
  ///   <br />
  ///   en: End of operation request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<EndOfOperationResponse> EndOfOperationAsync(EndOfOperationRequest ARequest, CancellationToken ct = default)
    => await PostAsync<EndOfOperationRequest, EndOfOperationResponse>("/navi/endOfOperation", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Üzemeltetés folytatása - a pénztárgép üzembe visszahelyezése a NAV-I felé.
  ///   <br />
  ///   en: Continue operation - reactivate the cash register towards NAV-I.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Üzemeltetés folytatási kérés.
  ///   <br />
  ///   en: Continue operation request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<ContinueOperationResponse> ContinueOperationAsync(ContinueOperationRequest ARequest, CancellationToken ct = default)
    => await PostAsync<ContinueOperationRequest, ContinueOperationResponse>("/navi/continueOperation", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Tulajdonosváltás (átszemélyesítés) - az e-pénztárgép új üzemeltetőhöz történő átadása.
  ///   <br />
  ///   en: Owner change (re-personalization) - transfer the e-cash register to a new operator.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Tulajdonosváltási kérés.
  ///   <br />
  ///   en: Owner change request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<OwnerChangeResponse> OwnerChangeAsync(OwnerChangeRequest ARequest, CancellationToken ct = default)
    => await PostAsync<OwnerChangeRequest, OwnerChangeResponse>("/navi/ownerChange", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Terméktörzs lekérdezés - termék keresése vonalkód/EAN kód alapján a NAV DTSZK adatbázisából.
  ///   <br />
  ///   en: Product catalog query - search product by barcode/EAN code from NAV DTSZK database.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Terméktörzs lekérdezési kérés.
  ///   <br />
  ///   en: Product catalog query request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<DownloadProductListResponse> DownloadProductListAsync(DownloadProductListRequest ARequest, CancellationToken ct = default)
    => await PostAsync<DownloadProductListRequest, DownloadProductListResponse>("/navi/downloadProductList", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: CommMgr heartbeat kényszerítése - azonnali CommMgr kommunikáció indítása diagnosztikai célra.
  ///   <br />
  ///   en: Force CommMgr heartbeat - trigger immediate CommMgr communication for diagnostics.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: CommMgr kényszerítési kérés.
  ///   <br />
  ///   en: Force CommMgr request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<ForceCommMgrResponse> ForceCommMgrAsync(ForceCommMgrRequest ARequest, CancellationToken ct = default)
    => await PostAsync<ForceCommMgrRequest, ForceCommMgrResponse>("/navi/forceCommMgr", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Offline szinkronizálás kényszerítése - azonnali offline szinkronizálás indítása diagnosztikai célra.
  ///   <br />
  ///   en: Force offline sync - trigger immediate offline synchronization for diagnostics.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Offline szinkronizálás kényszerítési kérés.
  ///   <br />
  ///   en: Force offline sync request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<ForceOfflineSyncResponse> ForceOfflineSyncAsync(ForceOfflineSyncRequest ARequest, CancellationToken ct = default)
    => await PostAsync<ForceOfflineSyncRequest, ForceOfflineSyncResponse>("/navi/forceOfflineSync", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Tanúsítvány ellenőrzés kényszerítése - azonnali tanúsítvány ellenőrzés indítása diagnosztikai célra.
  ///   <br />
  ///   en: Force certificate check - trigger immediate certificate check for diagnostics.
  /// </summary>
  /// <param name="ARequest">
  ///   hu: Tanúsítvány ellenőrzés kényszerítési kérés.
  ///   <br />
  ///   en: Force certificate check request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<ForceCertificateCheckResponse> ForceCertificateCheckAsync(ForceCertificateCheckRequest ARequest, CancellationToken ct = default)
    => await PostAsync<ForceCertificateCheckRequest, ForceCertificateCheckResponse>("/navi/forceCertificateCheck", ARequest, ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Blokkolási állapot lekérdezése - a pénztárgép aktuális blokkolási állapotának lekérdezése.
  ///   <br />
  ///   en: Get block state - query the current block state of the cash register.
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<GetBlockStateResponse> GetBlockStateAsync(string ARequestId, CancellationToken ct = default)
    => await GetAsync<GetBlockStateResponse>($"/navi/blockState/{ARequestId}", ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: NAV adatok lekérdezése - aktuális ÁFA kulcsok és üzemeltetői adatok lekérdezése.
  ///   <br />
  ///   en: Get NAV data - query current VAT rates and operator site data.
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<GetNavDataResponse> GetNavDataAsync(string ARequestId, CancellationToken ct = default)
    => await GetAsync<GetNavDataResponse>($"/navi/navData/{ARequestId}", ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Tanúsítvány állapot lekérdezése - a tanúsítványok lejárati idejének lekérdezése.
  ///   <br />
  ///   en: Get certificate status - query the expiry dates of certificates.
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<GetCertificateStatusResponse> GetCertificateStatusAsync(string ARequestId, CancellationToken ct = default)
    => await GetAsync<GetCertificateStatusResponse>($"/navi/certificateStatus/{ARequestId}", ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Technikai tájékoztatás lekérdezése - a NAV-tól kapott legutóbbi technikai üzenet lekérdezése.
  ///   <br />
  ///   en: Get technical info - query the latest technical message received from NAV.
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<GetTechnicalInfoResponse> GetTechnicalInfoAsync(string ARequestId, CancellationToken ct = default)
    => await GetAsync<GetTechnicalInfoResponse>($"/navi/technicalInfo/{ARequestId}", ct).ConfigureAwait(false);

  /// <summary>
  ///   hu: Szoftverfrissítés állapot lekérdezése - a firmware frissítés állapotának és határidejének lekérdezése.
  ///   <br />
  ///   en: Get software update status - query the firmware update status and installation deadline.
  /// </summary>
  /// <param name="ARequestId">
  ///   hu: Kérés egyedi azonosítója.
  ///   <br />
  ///   en: Unique identifier of the request.
  /// </param>
  /// <param name="ct">
  ///   hu: Lemondási token.
  ///   <br />
  ///   en: Cancellation token.
  /// </param>
  public async Task<GetSoftwareUpdateStatusResponse> GetSoftwareUpdateStatusAsync(string ARequestId, CancellationToken ct = default)
    => await GetAsync<GetSoftwareUpdateStatusResponse>($"/navi/softwareUpdateStatus/{ARequestId}", ct).ConfigureAwait(false);

  #endregion

  #region Device API..

  public async Task<TDeviceInfo> DeviceInfoAsync(CancellationToken ct = default)
  {
    try
    {
      return await GetAsync<TDeviceInfo>("/device/getDeviceInfo", ct).ConfigureAwait(false);
    }
    catch
    {
      // Fallback to an alternative endpoint if available
      return await GetAsync<TDeviceInfo>("/device/getDeviceInfo", ct).ConfigureAwait(false);
    }
  }

  #endregion

  #region Private Methods..

  private async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest payload, CancellationToken ct)
  {
    var url = FBaseUrl + path;

    using var req = new HttpRequestMessage(HttpMethod.Post, url);
    var (content, contentType) = Serialize(payload);
    req.Content = new ByteArrayContent(content);
    req.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
    req.Headers.Accept.Clear();
    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
    AddSessionHeaders(req);

    using var res = await FHttp.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
    if (!res.IsSuccessStatusCode)
    {
      var body = await SafeReadAsStringAsync(res.Content).ConfigureAwait(false);
      throw new HttpRequestException($"HTTP {(int)res.StatusCode} {res.ReasonPhrase} at {url}. Body: {body}");
    }

    var bytes = await res.Content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
    return Deserialize<TResponse>(bytes);
  }

  private async Task<TResponse> GetAsync<TResponse>(string path, CancellationToken ct)
  {
    var url = FBaseUrl + path;

    using var req = new HttpRequestMessage(HttpMethod.Get, url);
    var accept = FFormat == TApiFormat.MessagePack ? "application/msgpack" : "application/json";
    req.Headers.Accept.Clear();
    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
    AddSessionHeaders(req);

    using var res = await FHttp.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
    if (!res.IsSuccessStatusCode)
    {
      var body = await SafeReadAsStringAsync(res.Content).ConfigureAwait(false);
      throw new HttpRequestException($"HTTP {(int)res.StatusCode} {res.ReasonPhrase} at {url}. Body: {body}");
    }

    var bytes = await res.Content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
    return Deserialize<TResponse>(bytes);
  }

  /// <summary>
  ///   hu: Session és AP-Number headerek hozzáadása a HTTP kéréshez.
  ///   <br />
  ///   en: Add Session and AP-Number headers to the HTTP request.
  /// </summary>
  private void AddSessionHeaders(HttpRequestMessage ARequest)
  {
    if (SessionId is not null)
      ARequest.Headers.Add("Session-Id", SessionId);

    if (ApNumber is not null)
      ARequest.Headers.Add("Ap-Number", ApNumber);
  }

  private (byte[] data, string contentType) Serialize<T>(T value)
  {
    if (FFormat == TApiFormat.MessagePack)
      return (MessagePackSerializer.Serialize(value, FMsgpack), "application/msgpack");

    return (JsonSerializer.SerializeToUtf8Bytes(value, FJson), "application/json");
  }

  private T Deserialize<T>(byte[] bytes)
  {
    if (FFormat == TApiFormat.MessagePack)
      return MessagePackSerializer.Deserialize<T>(bytes, FMsgpack);

    return JsonSerializer.Deserialize<T>(bytes, FJson)!;
  }

  private static async Task<string> SafeReadAsStringAsync(HttpContent? content)
  {
    try
    {
      return content is null ? string.Empty : await content.ReadAsStringAsync().ConfigureAwait(false);
    }
    catch
    {
      return string.Empty;
    }
  }

  #endregion
}
