using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: QueryTaxpayerRequest és QueryTaxpayerResponse szerializációs tesztek (JSON + MessagePack)
///   <br />
///   en: QueryTaxpayerRequest and QueryTaxpayerResponse serialization tests (JSON + MessagePack)
/// </summary>
public class TQueryTaxpayerSerializationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  private static readonly MessagePackSerializerOptions CMsgpackOptions =
    MessagePackSerializerOptions.Standard.WithResolver(ContractlessStandardResolver.Instance);

  #region QueryTaxpayerRequest - JSON..

  /// <summary>
  ///   hu: Request JSON szerializáció round-trip
  ///   <br />
  ///   en: Request JSON serialization round-trip
  /// </summary>
  [Fact]
  public void Request_Json_RoundTrip()
  {
    var original = new QueryTaxpayerRequest("req-001", "12345678");

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<QueryTaxpayerRequest>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.TaxNumber, deserialized.TaxNumber);
  }

  /// <summary>
  ///   hu: Request JSON tartalmazza a kötelező mezőket
  ///   <br />
  ///   en: Request JSON contains required fields
  /// </summary>
  [Fact]
  public void Request_Json_ContainsRequiredFields()
  {
    var request = new QueryTaxpayerRequest("req-002", "87654321");

    var json = JsonSerializer.Serialize(request, CJsonOptions);

    Assert.Contains("requestId", json);
    Assert.Contains("req-002", json);
    Assert.Contains("taxNumber", json);
    Assert.Contains("87654321", json);
  }

  #endregion

  #region QueryTaxpayerRequest - MessagePack..

  /// <summary>
  ///   hu: Request MessagePack szerializáció round-trip
  ///   <br />
  ///   en: Request MessagePack serialization round-trip
  /// </summary>
  [Fact]
  public void Request_MessagePack_RoundTrip()
  {
    var original = new QueryTaxpayerRequest("req-003", "11223344");

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<QueryTaxpayerRequest>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.TaxNumber, deserialized.TaxNumber);
  }

  #endregion

  #region QueryTaxpayerResponse - JSON..

  /// <summary>
  ///   hu: Response JSON szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response JSON serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_Json_RoundTrip_Success()
  {
    var original = new QueryTaxpayerResponse(
      RequestId: "req-004",
      ResultCode: 0,
      TaxpayerValidity: true,
      TaxpayerName: "Teszt Kft.",
      TaxpayerAddress: "1111 Budapest, Példa utca 1",
      InfoDate: "2025-01-15T10:30:00.000Z"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<QueryTaxpayerResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.Equal(original.TaxpayerValidity, deserialized.TaxpayerValidity);
    Assert.Equal(original.TaxpayerName, deserialized.TaxpayerName);
    Assert.Equal(original.TaxpayerAddress, deserialized.TaxpayerAddress);
    Assert.Equal(original.InfoDate, deserialized.InfoDate);
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
    var original = new QueryTaxpayerResponse(
      RequestId: "req-005",
      ResultCode: 100,
      ErrorMessage: "NAV-I kommunikációs hiba"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<QueryTaxpayerResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.False(deserialized.TaxpayerValidity);
    Assert.Null(deserialized.TaxpayerName);
    Assert.Null(deserialized.TaxpayerAddress);
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
    var response = new QueryTaxpayerResponse(
      RequestId: "req-006",
      ResultCode: 0,
      TaxpayerValidity: false
    );

    var json = JsonSerializer.Serialize(response, CJsonOptions);

    Assert.DoesNotContain("taxpayerName", json);
    Assert.DoesNotContain("taxpayerAddress", json);
    Assert.DoesNotContain("infoDate", json);
    Assert.DoesNotContain("errorMessage", json);
  }

  #endregion

  #region QueryTaxpayerResponse - MessagePack..

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Success()
  {
    var original = new QueryTaxpayerResponse(
      RequestId: "req-007",
      ResultCode: 0,
      TaxpayerValidity: true,
      TaxpayerName: "Példa Bt.",
      TaxpayerAddress: "2000 Szentendre, Fő tér 5",
      InfoDate: "2025-02-01T08:00:00.000Z"
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<QueryTaxpayerResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.Equal(original.TaxpayerValidity, deserialized.TaxpayerValidity);
    Assert.Equal(original.TaxpayerName, deserialized.TaxpayerName);
    Assert.Equal(original.TaxpayerAddress, deserialized.TaxpayerAddress);
    Assert.Equal(original.InfoDate, deserialized.InfoDate);
  }

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (hibás válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (error response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Error()
  {
    var original = new QueryTaxpayerResponse(
      RequestId: "req-008",
      ResultCode: 100,
      ErrorMessage: "Timeout"
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<QueryTaxpayerResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.False(deserialized.TaxpayerValidity);
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
    var request = new QueryTaxpayerRequest("req-009", "99887766");

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
    var response = new QueryTaxpayerResponse("req-010", 0);

    Assert.IsAssignableFrom<INavIResponse>(response);
    Assert.IsAssignableFrom<QuantumAE.Api.IQaeResponse>(response);
  }

  #endregion
}
