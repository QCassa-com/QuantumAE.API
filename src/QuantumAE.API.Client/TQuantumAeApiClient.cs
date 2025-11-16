using System.Net.Http.Headers;
using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api;
using QuantumAE.Api.Orders;
using QuantumAE.Models;

namespace QuantumAE;

public enum TApiFormat
{
  Json,
  MessagePack
}

/// <summary>
///  hu: QuantumAE API kliens (Json/MessagePack támogatással) az Order webszolgáltatáshoz.
///  <br />
///  en: QuantumAE API client (Json/MessagePack) for the Order webservice.
/// </summary>
public sealed class TQuantumAeApiClient : IDisposable
{
  private readonly HttpClient FHttp;
  private readonly string FBaseUrl;
  private readonly TApiFormat FFormat;
  private readonly JsonSerializerOptions FJson;
  private readonly MessagePackSerializerOptions FMsgpack;

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

  public async Task<TApiResult> OpenAsync(TOrderOpenQaeRequest AQaeRequest, CancellationToken ct = default)
    => await PostAsync<TOrderOpenQaeRequest, TApiResult>("/orders/open", AQaeRequest, ct).ConfigureAwait(false);

  public async Task<TApiResult> ItemAddAsync(TOrderItemsAddQaeRequest AQaeRequest, CancellationToken ct = default)
    => await PostAsync<TOrderItemsAddQaeRequest, TApiResult>("/orders/items", AQaeRequest, ct).ConfigureAwait(false);

  private async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest payload, CancellationToken ct)
  {
    var url = FBaseUrl + path;

    using var req = new HttpRequestMessage(HttpMethod.Post, url);
    var (content, contentType) = Serialize(payload);
    req.Content = new ByteArrayContent(content);
    req.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
    req.Headers.Accept.Clear();
    req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

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

    using var res = await FHttp.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
    if (!res.IsSuccessStatusCode)
    {
      var body = await SafeReadAsStringAsync(res.Content).ConfigureAwait(false);
      throw new HttpRequestException($"HTTP {(int)res.StatusCode} {res.ReasonPhrase} at {url}. Body: {body}");
    }

    var bytes = await res.Content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
    return Deserialize<TResponse>(bytes);
  }

  public async Task<TDeviceInfo> DeviceInfoAsync(CancellationToken ct = default)
  {
    try
    {
      return await GetAsync<TDeviceInfo>("/device/", ct).ConfigureAwait(false);
    }
    catch
    {
      // Fallback to an alternative endpoint if available
      return await GetAsync<TDeviceInfo>("/device/info", ct).ConfigureAwait(false);
    }
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
}