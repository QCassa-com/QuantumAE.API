using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetSoftwareUpdateStatus integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: GetSoftwareUpdateStatus integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TGetSoftwareUpdateStatusIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres szoftverfrissítés állapot lekérdezés (frissítés elérhető)
  ///   <br />
  ///   en: Successful software update status query (update available)
  /// </summary>
  [Fact]
  public async Task GetSoftwareUpdateStatusAsync_Success_ReturnsUpdateAvailable()
  {
    var expectedResponse = new GetSoftwareUpdateStatusResponse(
      RequestId: "test-001",
      ResultCode: 0,
      UpdateAvailable: true,
      FirmwareInstallDueDate: "2026-03-15"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetSoftwareUpdateStatusAsync("test-001");

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.True(response.UpdateAvailable);
    Assert.Equal("2026-03-15", response.FirmwareInstallDueDate);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - szolgáltatás nem elérhető
  ///   <br />
  ///   en: Error response - service not available
  /// </summary>
  [Fact]
  public async Task GetSoftwareUpdateStatusAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new GetSoftwareUpdateStatusResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Szolgáltatás nem elérhető"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetSoftwareUpdateStatusAsync("test-002");

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
  public async Task GetSoftwareUpdateStatusAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.GetSoftwareUpdateStatusAsync("test-003"));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy (GET + RequestId az URL-ben)
  ///   <br />
  ///   en: Request goes to the correct URL (GET + RequestId in URL)
  /// </summary>
  [Fact]
  public async Task GetSoftwareUpdateStatusAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new GetSoftwareUpdateStatusResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await client.GetSoftwareUpdateStatusAsync("test-004");

    Assert.Equal("http://localhost:9090/navi/softwareUpdateStatus/test-004", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A válasz RequestId-t helyesen tartalmazza
  ///   <br />
  ///   en: Response correctly contains RequestId
  /// </summary>
  [Fact]
  public async Task GetSoftwareUpdateStatusAsync_PreservesRequestId()
  {
    var expectedResponse = new GetSoftwareUpdateStatusResponse(
      "my-unique-id", 0,
      UpdateAvailable: false
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetSoftwareUpdateStatusAsync("my-unique-id");

    Assert.Equal("my-unique-id", response.RequestId);
    Assert.False(response.UpdateAvailable);
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
