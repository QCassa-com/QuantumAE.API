using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: ForceOfflineSync integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: ForceOfflineSync integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TForceOfflineSyncIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres offline szinkronizálás kényszerítés
  ///   <br />
  ///   en: Successful force offline sync
  /// </summary>
  [Fact]
  public async Task ForceOfflineSyncAsync_Success_ReturnsSuccess()
  {
    var expectedResponse = new ForceOfflineSyncResponse(
      RequestId: "test-001",
      ResultCode: 0
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceOfflineSyncRequest("test-001");
    var response = await client.ForceOfflineSyncAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - szinkronizálási hiba
  ///   <br />
  ///   en: Error response - synchronization error
  /// </summary>
  [Fact]
  public async Task ForceOfflineSyncAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new ForceOfflineSyncResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Offline szinkronizálási hiba"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceOfflineSyncRequest("test-002");
    var response = await client.ForceOfflineSyncAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("Offline szinkronizálási hiba", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task ForceOfflineSyncAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceOfflineSyncRequest("test-003");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.ForceOfflineSyncAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task ForceOfflineSyncAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new ForceOfflineSyncResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceOfflineSyncRequest("test-004");
    await client.ForceOfflineSyncAsync(request);

    Assert.Equal("http://localhost:9090/navi/forceOfflineSync", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A kérés RequestId-t helyesen továbbítja
  ///   <br />
  ///   en: Request correctly forwards RequestId
  /// </summary>
  [Fact]
  public async Task ForceOfflineSyncAsync_PreservesRequestId()
  {
    var expectedResponse = new ForceOfflineSyncResponse("my-unique-id", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceOfflineSyncRequest("my-unique-id");
    var response = await client.ForceOfflineSyncAsync(request);

    Assert.Equal("my-unique-id", response.RequestId);
  }

  #endregion

  #region Mock HTTP Handler..

  /// <summary>
  ///   hu: Mock HTTP handler teszteléshez
  ///   <br />
  ///   en: Mock HTTP handler for testing
  /// </summary>
  private sealed class TMockHttpHandler : HttpMessageHandler
  {
    private readonly HttpStatusCode FStatusCode;
    private readonly byte[]? FResponseBytes;

    public string? LastRequestUrl { get; private set; }
    public HttpMethod? LastRequestMethod { get; private set; }

    public TMockHttpHandler(object AResponse, JsonSerializerOptions AJsonOptions)
    {
      FStatusCode = HttpStatusCode.OK;
      FResponseBytes = JsonSerializer.SerializeToUtf8Bytes(AResponse, AJsonOptions);
    }

    public TMockHttpHandler(HttpStatusCode AStatusCode)
    {
      FStatusCode = AStatusCode;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage ARequest, CancellationToken ACancellationToken)
    {
      LastRequestUrl = ARequest.RequestUri?.ToString();
      LastRequestMethod = ARequest.Method;

      var response = new HttpResponseMessage(FStatusCode);

      if (FResponseBytes is not null)
      {
        response.Content = new ByteArrayContent(FResponseBytes);
        response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      }

      return Task.FromResult(response);
    }
  }

  #endregion
}
