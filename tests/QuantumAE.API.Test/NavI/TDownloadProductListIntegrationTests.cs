using System.Net;
using System.Text.Json;
using QuantumAE;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: DownloadProductList integrációs tesztek - end-to-end flow Mock HTTP handler-rel
///   <br />
///   en: DownloadProductList integration tests - end-to-end flow with Mock HTTP handler
/// </summary>
public class TDownloadProductListIntegrationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  #region End-to-End Tests..

  /// <summary>
  ///   hu: Sikeres terméktörzs lekérdezés - találat van
  ///   <br />
  ///   en: Successful product catalog query - results found
  /// </summary>
  [Fact]
  public async Task DownloadProductListAsync_WithProducts_ReturnsSuccess()
  {
    var expectedResponse = new DownloadProductListResponse(
      RequestId: "test-001",
      ResultCode: 0,
      Products: new List<TProductDto>
      {
        new(
          DtszkId: "DTSZK-001",
          ProductId: "5901234123457",
          ProductName: "Teszt Termék",
          ProductManufacturer: "Teszt Gyártó",
          UnitOfMeasure: "PIECE",
          Vtsz: "12345678",
          CategoryCode: "CAT001",
          CategoryName: "Élelmiszer",
          State: "ACTIVE"
        )
      }
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new DownloadProductListRequest("test-001", "5901234123457");
    var response = await client.DownloadProductListAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.NotNull(response.Products);
    Assert.Single(response.Products);
    Assert.Equal("DTSZK-001", response.Products[0].DtszkId);
    Assert.Equal("5901234123457", response.Products[0].ProductId);
    Assert.Equal("Teszt Termék", response.Products[0].ProductName);
    Assert.Null(response.ErrorMessage);
  }

  /// <summary>
  ///   hu: Sikeres terméktörzs lekérdezés - nincs találat
  ///   <br />
  ///   en: Successful product catalog query - no results
  /// </summary>
  [Fact]
  public async Task DownloadProductListAsync_NoProducts_ReturnsEmptyList()
  {
    var expectedResponse = new DownloadProductListResponse(
      RequestId: "test-002",
      ResultCode: 0
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new DownloadProductListRequest("test-002", "9999999999999");
    var response = await client.DownloadProductListAsync(request);

    Assert.NotNull(response);
    Assert.Equal(0, response.ResultCode);
    Assert.Null(response.Products);
  }

  /// <summary>
  ///   hu: Hibás válasz - NAV kommunikációs hiba
  ///   <br />
  ///   en: Error response - NAV communication error
  /// </summary>
  [Fact]
  public async Task DownloadProductListAsync_NavError_ReturnsErrorResponse()
  {
    var expectedResponse = new DownloadProductListResponse(
      RequestId: "test-003",
      ResultCode: 100,
      ErrorMessage: "NAV-I kommunikációs hiba"
    );

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new DownloadProductListRequest("test-003", "11111111111");
    var response = await client.DownloadProductListAsync(request);

    Assert.NotNull(response);
    Assert.Equal(100, response.ResultCode);
    Assert.Equal("NAV-I kommunikációs hiba", response.ErrorMessage);
    Assert.Null(response.Products);
  }

  /// <summary>
  ///   hu: HTTP hiba esetén kivétel keletkezik
  ///   <br />
  ///   en: HTTP error throws exception
  /// </summary>
  [Fact]
  public async Task DownloadProductListAsync_HttpError_ThrowsException()
  {
    using var handler = new TMockHttpHandler(HttpStatusCode.InternalServerError);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new DownloadProductListRequest("test-004", "22222222222");

    await Assert.ThrowsAsync<HttpRequestException>(
      () => client.DownloadProductListAsync(request));
  }

  /// <summary>
  ///   hu: A kérés a helyes URL-re megy
  ///   <br />
  ///   en: Request goes to the correct URL
  /// </summary>
  [Fact]
  public async Task DownloadProductListAsync_SendsToCorrectUrl()
  {
    var expectedResponse = new DownloadProductListResponse("test-005", 0);

    using var handler = new TMockHttpHandler(expectedResponse, CJsonOptions);
    using var client = new TQuantumAeApiClient("http://localhost:9090", AHandler: handler);

    var request = new DownloadProductListRequest("test-005", "33333333333");
    await client.DownloadProductListAsync(request);

    Assert.Equal("http://localhost:9090/navi/downloadProductList", handler.LastRequestUrl);
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
