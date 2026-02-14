using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: OwnerChange integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: OwnerChange integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TOwnerChangeIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres tulajdonosváltás
  ///   <br />
  ///   en: Successful owner change
  /// </summary>
  [Fact]
  public async Task OwnerChangeAsync_Success_ReturnsSuccess()
  {
    var expectedResponse = new OwnerChangeResponse(
      RequestId: "test-001",
      ResultCode: 0,
      NewOperatorName: "Teszt Kft.",
      BlockUnblock: "UNBLOCK"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new OwnerChangeRequest("test-001", "A001B002", "1234567890123456");
    var response = await client.OwnerChangeAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.Equal("Teszt Kft.", response.NewOperatorName);
    Assert.Equal("UNBLOCK", response.BlockUnblock);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Hibás válasz - NAV kommunikációs hiba
  ///   <br />
  ///   en: Error response - NAV communication error
  /// </summary>
  [Fact]
  public async Task OwnerChangeAsync_NavError_ReturnsErrorResponse()
  {
    var expectedResponse = new OwnerChangeResponse(
      RequestId: "test-002",
      ResultCode: 100,
      ErrorMessage: "NAV-I kommunikációs hiba"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new OwnerChangeRequest("test-002", "A001B002", "1234567890123456");
    var response = await client.OwnerChangeAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("NAV-I kommunikációs hiba", response.ErrorMessage);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task OwnerChangeAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new OwnerChangeRequest("test-003", "A001B002", "1234567890123456");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.OwnerChangeAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task OwnerChangeAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new OwnerChangeResponse("test-004", 0, "Teszt Kft.", "UNBLOCK");

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new OwnerChangeRequest("test-004", "A001B002", "1234567890123456");
    await client.OwnerChangeAsync(request);

    Assert.Equal("http://localhost:9090/navi/ownerChange", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
  }

  /// <summary>
  ///   hu: A kérés RequestId-t helyesen továbbítja
  ///   <br />
  ///   en: Request correctly forwards RequestId
  /// </summary>
  [Fact]
  public async Task OwnerChangeAsync_PreservesRequestId()
  {
    var expectedResponse = new OwnerChangeResponse("my-unique-id", 0, "Teszt Kft.", "UNBLOCK");

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new OwnerChangeRequest("my-unique-id", "A001B002", "1234567890123456");
    var response = await client.OwnerChangeAsync(request);

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
