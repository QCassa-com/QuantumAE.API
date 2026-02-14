using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetBlockState integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: GetBlockState integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TGetBlockStateIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres blokkolási állapot lekérdezés (nem blokkolt)
  ///   <br />
  ///   en: Successful block state query (not blocked)
  /// </summary>
  [Fact]
  public async Task GetBlockStateAsync_Success_ReturnsNotBlocked()
  {
    var expectedResponse = new GetBlockStateResponse(
      RequestId: "test-001",
      ResultCode: 0,
      IsBlocked: false
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetBlockStateAsync("test-001");

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.False(response.IsBlocked);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - szolgáltatás hiba
  ///   <br />
  ///   en: Error response - service error
  /// </summary>
  [Fact]
  public async Task GetBlockStateAsync_Error_ReturnsErrorResponse()
  {
    var expectedResponse = new GetBlockStateResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "Szolgáltatás nem elérhető"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetBlockStateAsync("test-002");

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
  public async Task GetBlockStateAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.GetBlockStateAsync("test-003"));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy (GET + RequestId az URL-ben)
  ///   <br />
  ///   en: Request goes to the correct URL (GET + RequestId in URL)
  /// </summary>
  [Fact]
  public async Task GetBlockStateAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new GetBlockStateResponse("test-004", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    await client.GetBlockStateAsync("test-004");

    Assert.Equal("http://localhost:9090/navi/blockState/test-004", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Get, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A válasz RequestId-t helyesen tartalmazza
  ///   <br />
  ///   en: Response correctly contains RequestId
  /// </summary>
  [Fact]
  public async Task GetBlockStateAsync_PreservesRequestId()
  {
    var expectedResponse = new GetBlockStateResponse("my-unique-id", 0, IsBlocked: true);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var response = await client.GetBlockStateAsync("my-unique-id");

    Assert.Equal("my-unique-id", response.RequestId);
    Assert.True(response.IsBlocked);
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
