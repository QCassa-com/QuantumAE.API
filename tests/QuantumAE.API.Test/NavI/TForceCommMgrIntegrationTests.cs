using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: ForceCommMgr integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: ForceCommMgr integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TForceCommMgrIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres CommMgr kényszerítés
  ///   <br />
  ///   en: Successful force CommMgr
  /// </summary>
  [Fact]
  public async Task ForceCommMgrAsync_Success_ReturnsSuccess()
  {
    var expectedResponse = new ForceCommMgrResponse(
      RequestId: "test-001",
      ResultCode: 0
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCommMgrRequest("test-001");
    var response = await client.ForceCommMgrAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - kommunikációs hiba
  ///   <br />
  ///   en: Error response - communication error
  /// </summary>
  [Fact]
  public async Task ForceCommMgrAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new ForceCommMgrResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "CommMgr kommunikációs hiba"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCommMgrRequest("test-002");
    var response = await client.ForceCommMgrAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("CommMgr kommunikációs hiba", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task ForceCommMgrAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCommMgrRequest("test-003");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.ForceCommMgrAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task ForceCommMgrAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new ForceCommMgrResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCommMgrRequest("test-004");
    await client.ForceCommMgrAsync(request);

    Assert.Equal("http://localhost:9090/navi/forceCommMgr", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A kérés RequestId-t helyesen továbbítja
  ///   <br />
  ///   en: Request correctly forwards RequestId
  /// </summary>
  [Fact]
  public async Task ForceCommMgrAsync_PreservesRequestId()
  {
    var expectedResponse = new ForceCommMgrResponse("my-unique-id", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCommMgrRequest("my-unique-id");
    var response = await client.ForceCommMgrAsync(request);

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
