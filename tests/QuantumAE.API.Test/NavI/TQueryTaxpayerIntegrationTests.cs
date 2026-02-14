using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: QueryTaxpayer integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: QueryTaxpayer integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TQueryTaxpayerIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres adószám lekérdezés - érvényes adózó
  ///   <br />
  ///   en: Successful tax number query - valid taxpayer
  /// </summary>
  [Fact]
  public async Task QueryTaxpayerAsync_ValidTaxpayer_ReturnsSuccess()
  {
    var expectedResponse = new QueryTaxpayerResponse(
      RequestId: "test-001",
      ResultCode: 0,
      TaxpayerValidity: true,
      TaxpayerName: "Teszt Kft.",
      TaxpayerAddress: "1111 Budapest, Példa utca 1",
      InfoDate: "2025-01-15T10:30:00.000Z"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new QueryTaxpayerRequest("test-001", "12345678");
    var response = await client.QueryTaxpayerAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.True(response.TaxpayerValidity);
    Assert.Equal("Teszt Kft.", response.TaxpayerName);
    Assert.Equal("1111 Budapest, Példa utca 1", response.TaxpayerAddress);
    Assert.Equal("2025-01-15T10:30:00.000Z", response.InfoDate);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Sikeres adószám lekérdezés - nem érvényes adózó
  ///   <br />
  ///   en: Successful tax number query - invalid taxpayer
  /// </summary>
  [Fact]
  public async Task QueryTaxpayerAsync_InvalidTaxpayer_ReturnsFalseValidity()
  {
    var expectedResponse = new QueryTaxpayerResponse(
      RequestId: "test-002",
      ResultCode: 0,
      TaxpayerValidity: false
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new QueryTaxpayerRequest("test-002", "99999999");
    var response = await client.QueryTaxpayerAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.False(response.TaxpayerValidity);
    Assert.Null(response.TaxpayerName);
  }

  /// <summary>
  ///   hu: Hibás válasz - NAV kommunikációs hiba
  ///   <br />
  ///   en: Error response - NAV communication error
  /// </summary>
  [Fact]
  public async Task QueryTaxpayerAsync_NavError_ReturnsErrorResponse()
  {
    var expectedResponse = new QueryTaxpayerResponse(
      RequestId: "test-003",
      ResultCode: 100,
      ErrorMessage: "NAV-I kommunikációs hiba"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new QueryTaxpayerRequest("test-003", "11111111");
    var response = await client.QueryTaxpayerAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("NAV-I kommunikációs hiba", response.ErrorMessage);
    Assert.False(response.TaxpayerValidity);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task QueryTaxpayerAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new QueryTaxpayerRequest("test-004", "22222222");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.QueryTaxpayerAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task QueryTaxpayerAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new QueryTaxpayerResponse("test-005", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new QueryTaxpayerRequest("test-005", "33333333");
    await client.QueryTaxpayerAsync(request);

    Assert.Equal("http://localhost:9090/navi/queryTaxpayer", handler.LastRequestUrl);
    Assert.Equal(HttpMethod.Post, handler.LastRequestMethod);
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
