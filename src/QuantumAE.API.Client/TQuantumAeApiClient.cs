using System.Net.Http.Headers;
using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
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
