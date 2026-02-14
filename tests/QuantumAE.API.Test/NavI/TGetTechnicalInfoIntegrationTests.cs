using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetTechnicalInfo integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: GetTechnicalInfo integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TGetTechnicalInfoIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres technikai tájékoztatás lekérdezés PrintMessage-dzsel
  ///   <br />
  ///   en: Successful technical info query with PrintMessage
  /// </summary>
  [Fact]
  public async Task GetTechnicalInfoAsync_Success_ReturnsPrintMessage()
  {
    var expectedResponse = new GetTechnicalInfoResponse(
      RequestId: "test-001",
      ResultCode: 0,
      PrintMessage: "Technikai tájékoztatás\\nNAV rendszer\\nVerzió: 1.4"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetTechnicalInfoAsync("test-001");

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.NotNull(response.PrintMessage);
    Assert.Contains("Technikai", response.PrintMessage);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - szolgáltatás nem elérhető
  ///   <br />
  ///   en: Error response - service not available
  /// </summary>
  [Fact]
  public async Task GetTechnicalInfoAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new GetTechnicalInfoResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Szolgáltatás nem elérhető"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetTechnicalInfoAsync("test-002");

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("Szolgáltatás nem elérhető", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task GetTechnicalInfoAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.GetTechnicalInfoAsync("test-003"));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy (GET + RequestId az URL-ben)
  ///   <br />
  ///   en: Request goes to the correct URL (GET + RequestId in URL)
  /// </summary>
  [Fact]
  public async Task GetTechnicalInfoAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new GetTechnicalInfoResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await client.GetTechnicalInfoAsync("test-004");

    Assert.Equal("http://localhost:9090/navi/technicalInfo/test-004", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A válasz RequestId-t helyesen tartalmazza
  ///   <br />
  ///   en: Response correctly contains RequestId
  /// </summary>
  [Fact]
  public async Task GetTechnicalInfoAsync_PreservesRequestId()
  {
    var expectedResponse = new GetTechnicalInfoResponse(
      "my-unique-id", 0,
      PrintMessage: "NAV teszt üzenet"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetTechnicalInfoAsync("my-unique-id");

    Assert.Equal("my-unique-id", response.RequestId);
    Assert.Equal("NAV teszt üzenet", response.PrintMessage);
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
