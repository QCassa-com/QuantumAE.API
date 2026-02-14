using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: ForceCertificateCheck integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: ForceCertificateCheck integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TForceCertificateCheckIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres tanúsítvány ellenőrzés kényszerítés
  ///   <br />
  ///   en: Successful force certificate check
  /// </summary>
  [Fact]
  public async Task ForceCertificateCheckAsync_Success_ReturnsSuccess()
  {
    var expectedResponse = new ForceCertificateCheckResponse(
      RequestId: "test-001",
      ResultCode: 0
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCertificateCheckRequest("test-001");
    var response = await client.ForceCertificateCheckAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - tanúsítvány hiba
  ///   <br />
  ///   en: Error response - certificate error
  /// </summary>
  [Fact]
  public async Task ForceCertificateCheckAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new ForceCertificateCheckResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Tanúsítvány lejárt"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCertificateCheckRequest("test-002");
    var response = await client.ForceCertificateCheckAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("Tanúsítvány lejárt", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task ForceCertificateCheckAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCertificateCheckRequest("test-003");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.ForceCertificateCheckAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task ForceCertificateCheckAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new ForceCertificateCheckResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCertificateCheckRequest("test-004");
    await client.ForceCertificateCheckAsync(request);

    Assert.Equal("http://localhost:9090/navi/forceCertificateCheck", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A kérés RequestId-t helyesen továbbítja
  ///   <br />
  ///   en: Request correctly forwards RequestId
  /// </summary>
  [Fact]
  public async Task ForceCertificateCheckAsync_PreservesRequestId()
  {
    var expectedResponse = new ForceCertificateCheckResponse("my-unique-id", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new ForceCertificateCheckRequest("my-unique-id");
    var response = await client.ForceCertificateCheckAsync(request);

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
