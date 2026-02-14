using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: DownloadProductListRequest és DownloadProductListResponse szerializációs tesztek (JSON + MessagePack)
///   <br />
///   en: DownloadProductListRequest and DownloadProductListResponse serialization tests (JSON + MessagePack)
/// </summary>
public class TDownloadProductListSerializationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  private static readonly MessagePackSerializerOptions CMsgpackOptions =
    MessagePackSerializerOptions.Standard.WithResolver(ContractlessStandardResolver.Instance);

  #region DownloadProductListRequest - JSON..

  /// <summary>
  ///   hu: Request JSON szerializáció round-trip
  ///   <br />
  ///   en: Request JSON serialization round-trip
  /// </summary>
  [Fact]
  public void Request_Json_RoundTrip()
  {
    var original = new DownloadProductListRequest("req-001", "5901234123457");

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<DownloadProductListRequest>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ProductCode, deserialized.ProductCode);
  }

  /// <summary>
  ///   hu: Request JSON tartalmazza a kötelező mezőket
  ///   <br />
  ///   en: Request JSON contains required fields
  /// </summary>
  [Fact]
  public void Request_Json_ContainsRequiredFields()
  {
    var request = new DownloadProductListRequest("req-002", "5901234123457");

    var json = JsonSerializer.Serialize(request, CJsonOptions);

    Assert.Contains("requestId", json);
    Assert.Contains("req-002", json);
    Assert.Contains("productCode", json);
    Assert.Contains("5901234123457", json);
  }

  #endregion

  #region DownloadProductListRequest - MessagePack..

  /// <summary>
  ///   hu: Request MessagePack szerializáció round-trip
  ///   <br />
  ///   en: Request MessagePack serialization round-trip
  /// </summary>
  [Fact]
  public void Request_MessagePack_RoundTrip()
  {
    var original = new DownloadProductListRequest("req-003", "5901234123457");

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<DownloadProductListRequest>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ProductCode, deserialized.ProductCode);
  }

  #endregion

  #region DownloadProductListResponse - JSON..

  /// <summary>
  ///   hu: Response JSON szerializáció round-trip (sikeres válasz termékekkel)
  ///   <br />
  ///   en: Response JSON serialization round-trip (successful response with products)
  /// </summary>
  [Fact]
  public void Response_Json_RoundTrip_Success()
  {
    var original = new DownloadProductListResponse(
      RequestId: "req-004",
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

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<DownloadProductListResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.NotNull(deserialized.Products);
    Assert.Single(deserialized.Products);
    Assert.Equal("DTSZK-001", deserialized.Products[0].DtszkId);
    Assert.Equal("5901234123457", deserialized.Products[0].ProductId);
    Assert.Equal("Teszt Termék", deserialized.Products[0].ProductName);
    Assert.Equal("Teszt Gyártó", deserialized.Products[0].ProductManufacturer);
    Assert.Equal("PIECE", deserialized.Products[0].UnitOfMeasure);
    Assert.Null(deserialized.ErrorMessage);
  }

  /// <summary>
  ///   hu: Response JSON szerializáció round-trip (hibás válasz)
  ///   <br />
  ///   en: Response JSON serialization round-trip (error response)
  /// </summary>
  [Fact]
  public void Response_Json_RoundTrip_Error()
  {
    var original = new DownloadProductListResponse(
      RequestId: "req-005",
      ResultCode: 100,
      ErrorMessage: "NAV-I kommunikációs hiba"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<DownloadProductListResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.Null(deserialized.Products);
    Assert.Equal(original.ErrorMessage, deserialized.ErrorMessage);
  }

  /// <summary>
  ///   hu: Response JSON null mezők nem jelennek meg a kimenetben
  ///   <br />
  ///   en: Response JSON null fields are omitted from output
  /// </summary>
  [Fact]
  public void Response_Json_NullFieldsOmitted()
  {
    var response = new DownloadProductListResponse(
      RequestId: "req-006",
      ResultCode: 0
    );

    var json = JsonSerializer.Serialize(response, CJsonOptions);

    Assert.DoesNotContain("products", json);
    Assert.DoesNotContain("errorMessage", json);
  }

  #endregion

  #region DownloadProductListResponse - MessagePack..

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Success()
  {
    var original = new DownloadProductListResponse(
      RequestId: "req-007",
      ResultCode: 0,
      Products: new List<TProductDto>
      {
        new(
          DtszkId: "DTSZK-002",
          ProductId: "5901234123457",
          ProductName: "Másik Termék",
          UnitOfMeasure: "KILOGRAM"
        )
      }
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<DownloadProductListResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.NotNull(deserialized.Products);
    Assert.Single(deserialized.Products);
    Assert.Equal("DTSZK-002", deserialized.Products[0].DtszkId);
  }

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (hibás válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (error response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Error()
  {
    var original = new DownloadProductListResponse(
      RequestId: "req-008",
      ResultCode: 100,
      ErrorMessage: "Timeout"
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<DownloadProductListResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.Null(deserialized.Products);
    Assert.Equal(original.ErrorMessage, deserialized.ErrorMessage);
  }

  #endregion

  #region Interface Implementation..

  /// <summary>
  ///   hu: Request implementálja az INavIRequest interfészt
  ///   <br />
  ///   en: Request implements INavIRequest interface
  /// </summary>
  [Fact]
  public void Request_ImplementsINavIRequest()
  {
    var request = new DownloadProductListRequest("req-009", "12345");

    Assert.IsAssignableFrom<INavIRequest>(request);
    Assert.IsAssignableFrom<QuantumAE.Api.IQaeRequest>(request);
  }

  /// <summary>
  ///   hu: Response implementálja az INavIResponse interfészt
  ///   <br />
  ///   en: Response implements INavIResponse interface
  /// </summary>
  [Fact]
  public void Response_ImplementsINavIResponse()
  {
    var response = new DownloadProductListResponse("req-010", 0);

    Assert.IsAssignableFrom<INavIResponse>(response);
    Assert.IsAssignableFrom<QuantumAE.Api.IQaeResponse>(response);
  }

  #endregion
}
