using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: GetNavDataRequest és GetNavDataResponse szerializációs tesztek (JSON + MessagePack)
///   <br />
///   en: GetNavDataRequest and GetNavDataResponse serialization tests (JSON + MessagePack)
/// </summary>
public class TGetNavDataSerializationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  private static readonly MessagePackSerializerOptions CMsgpackOptions =
    MessagePackSerializerOptions.Standard.WithResolver(ContractlessStandardResolver.Instance);

  #region GetNavDataRequest - JSON..

  /// <summary>
  ///   hu: Request JSON szerializáció round-trip
  ///   <br />
  ///   en: Request JSON serialization round-trip
  /// </summary>
  [Fact]
  public void Request_Json_RoundTrip()
  {
    var original = new GetNavDataRequest("req-001");

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<GetNavDataRequest>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
  }

  /// <summary>
  ///   hu: Request JSON tartalmazza a kötelező mezőket
  ///   <br />
  ///   en: Request JSON contains required fields
  /// </summary>
  [Fact]
  public void Request_Json_ContainsRequiredFields()
  {
    var request = new GetNavDataRequest("req-002");

    var json = JsonSerializer.Serialize(request, CJsonOptions);

    Assert.Contains("requestId", json);
    Assert.Contains("req-002", json);
  }

  #endregion

  #region GetNavDataRequest - MessagePack..

  /// <summary>
  ///   hu: Request MessagePack szerializáció round-trip
  ///   <br />
  ///   en: Request MessagePack serialization round-trip
  /// </summary>
  [Fact]
  public void Request_MessagePack_RoundTrip()
  {
    var original = new GetNavDataRequest("req-003");

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<GetNavDataRequest>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
  }

  #endregion

  #region GetNavDataResponse - JSON..

  /// <summary>
  ///   hu: Response JSON szerializáció round-trip (sikeres válasz ÁFA adatokkal)
  ///   <br />
  ///   en: Response JSON serialization round-trip (successful response with VAT data)
  /// </summary>
  [Fact]
  public void Response_Json_RoundTrip_Success()
  {
    var original = new GetNavDataResponse(
      RequestId: "req-004",
      ResultCode: 0,
      VatGroups: [
        new TVatGroupDto("A  ", 27m, 21.26m, "27%"),
        new TVatGroupDto("B  ", 18m, 15.25m, "18%")
      ],
      OperatorName: "Teszt Kft.",
      OperatingSiteName: "Teszt Üzlet"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<GetNavDataResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.NotNull(deserialized.VatGroups);
    Assert.Equal(2, deserialized.VatGroups.Length);
    Assert.Equal("A  ", deserialized.VatGroups[0].CollectorCode);
    Assert.Equal(27m, deserialized.VatGroups[0].VatPercentage);
    Assert.Equal("Teszt Kft.", deserialized.OperatorName);
    Assert.Equal("Teszt Üzlet", deserialized.OperatingSiteName);
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
    var original = new GetNavDataResponse(
      RequestId: "req-005",
      ResultCode: 100,
      ErrorMessage: "Nincs elérhető NAV adat"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<GetNavDataResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
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
    var response = new GetNavDataResponse(
      RequestId: "req-006",
      ResultCode: 0
    );

    var json = JsonSerializer.Serialize(response, CJsonOptions);

    Assert.DoesNotContain("errorMessage", json);
    Assert.DoesNotContain("vatGroups", json);
    Assert.DoesNotContain("operatorName", json);
    Assert.DoesNotContain("operatingSiteName", json);
  }

  #endregion

  #region GetNavDataResponse - MessagePack..

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Success()
  {
    var original = new GetNavDataResponse(
      RequestId: "req-007",
      ResultCode: 0,
      VatGroups: [new TVatGroupDto("C  ", 5m, 4.76m, "5%")],
      OperatorName: "Üzemeltető Zrt."
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<GetNavDataResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.NotNull(deserialized.VatGroups);
    Assert.Single(deserialized.VatGroups);
    Assert.Equal("C  ", deserialized.VatGroups[0].CollectorCode);
    Assert.Equal("Üzemeltető Zrt.", deserialized.OperatorName);
  }

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (hibás válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (error response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Error()
  {
    var original = new GetNavDataResponse(
      RequestId: "req-008",
      ResultCode: 100,
      ErrorMessage: "Timeout"
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<GetNavDataResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
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
    var request = new GetNavDataRequest("req-009");

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
    var response = new GetNavDataResponse("req-010", 0);

    Assert.IsAssignableFrom<INavIResponse>(response);
    Assert.IsAssignableFrom<QuantumAE.Api.IQaeResponse>(response);
  }

  #endregion
}
