using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetNavData integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: GetNavData integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TGetNavDataIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres NAV adat lekérdezés
  ///   <br />
  ///   en: Successful NAV data query
  /// </summary>
  [Fact]
  public async Task GetNavDataAsync_Success_ReturnsNavData()
  {
    var expectedResponse = new GetNavDataResponse(
      RequestId: "test-001",
      ResultCode: 0,
      VatGroups: [new TVatGroupDto("A  ", 27m, 21.26m, "27%")],
      OperatorName: "Teszt Kft.",
      OperatingSiteName: "Teszt Üzlet"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetNavDataAsync("test-001");

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.NotNull(response.VatGroups);
    Assert.Single(response.VatGroups);
    Assert.Equal("Teszt Kft.", response.OperatorName);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - nincs elérhető adat
  ///   <br />
  ///   en: Error response - no data available
  /// </summary>
  [Fact]
  public async Task GetNavDataAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new GetNavDataResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Nincs elérhető NAV adat"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetNavDataAsync("test-002");

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("Nincs elérhető NAV adat", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task GetNavDataAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.GetNavDataAsync("test-003"));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy (GET + RequestId az URL-ben)
  ///   <br />
  ///   en: Request goes to the correct URL (GET + RequestId in URL)
  /// </summary>
  [Fact]
  public async Task GetNavDataAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new GetNavDataResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await client.GetNavDataAsync("test-004");

    Assert.Equal("http://localhost:9090/navi/navData/test-004", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A válasz RequestId-t helyesen tartalmazza
  ///   <br />
  ///   en: Response correctly contains RequestId
  /// </summary>
  [Fact]
  public async Task GetNavDataAsync_PreservesRequestId()
  {
    var expectedResponse = new GetNavDataResponse(
      "my-unique-id", 0,
      OperatorName: "Üzemeltető"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetNavDataAsync("my-unique-id");

    Assert.Equal("my-unique-id", response.RequestId);
    Assert.Equal("Üzemeltető", response.OperatorName);
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
