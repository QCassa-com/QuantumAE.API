using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetCertificateStatus integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: GetCertificateStatus integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TGetCertificateStatusIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres tanúsítvány állapot lekérdezés
  ///   <br />
  ///   en: Successful certificate status query
  /// </summary>
  [Fact]
  public async Task GetCertificateStatusAsync_Success_ReturnsCertificateStatus()
  {
    var expectedResponse = new GetCertificateStatusResponse(
      RequestId: "test-001",
      ResultCode: 0,
      AuthCertExpiry: "2027-02-14T00:00:00.0000000Z",
      SignCertExpiry: "2027-06-14T00:00:00.0000000Z",
      DaysUntilExpiry: 365
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetCertificateStatusAsync("test-001");

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.NotNull(response.AuthCertExpiry);
    Assert.NotNull(response.SignCertExpiry);
    Assert.Equal(365, response.DaysUntilExpiry);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - tanúsítvány nem található
  ///   <br />
  ///   en: Error response - certificate not found
  /// </summary>
  [Fact]
  public async Task GetCertificateStatusAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new GetCertificateStatusResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Tanúsítvány nem található"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetCertificateStatusAsync("test-002");

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("Tanúsítvány nem található", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task GetCertificateStatusAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.GetCertificateStatusAsync("test-003"));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy (GET + RequestId az URL-ben)
  ///   <br />
  ///   en: Request goes to the correct URL (GET + RequestId in URL)
  /// </summary>
  [Fact]
  public async Task GetCertificateStatusAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new GetCertificateStatusResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await client.GetCertificateStatusAsync("test-004");

    Assert.Equal("http://localhost:9090/navi/certificateStatus/test-004", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A válasz RequestId-t helyesen tartalmazza
  ///   <br />
  ///   en: Response correctly contains RequestId
  /// </summary>
  [Fact]
  public async Task GetCertificateStatusAsync_PreservesRequestId()
  {
    var expectedResponse = new GetCertificateStatusResponse(
      "my-unique-id", 0,
      DaysUntilExpiry: 180
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetCertificateStatusAsync("my-unique-id");

    Assert.Equal("my-unique-id", response.RequestId);
    Assert.Equal(180, response.DaysUntilExpiry);
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
