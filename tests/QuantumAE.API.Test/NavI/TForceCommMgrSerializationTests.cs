using System.Text.Json;
using MessagePack;
using MessagePack.Resolvers;
using QuantumAE.Api.NavI;

namespace QuantumAE.API.Test.NavI;

/// <summary>
///   hu: ForceCommMgrRequest és ForceCommMgrResponse szerializációs tesztek (JSON + MessagePack)
///   <br />
///   en: ForceCommMgrRequest and ForceCommMgrResponse serialization tests (JSON + MessagePack)
/// </summary>
public class TForceCommMgrSerializationTests
{
  private static readonly JsonSerializerOptions CJsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
  };

  private static readonly MessagePackSerializerOptions CMsgpackOptions =
    MessagePackSerializerOptions.Standard.WithResolver(ContractlessStandardResolver.Instance);

  #region ForceCommMgrRequest - JSON..

  /// <summary>
  ///   hu: Request JSON szerializáció round-trip
  ///   <br />
  ///   en: Request JSON serialization round-trip
  /// </summary>
  [Fact]
  public void Request_Json_RoundTrip()
  {
    var original = new ForceCommMgrRequest("req-001");

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<ForceCommMgrRequest>(json, CJsonOptions);

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
    var request = new ForceCommMgrRequest("req-002");

    var json = JsonSerializer.Serialize(request, CJsonOptions);

    Assert.Contains("requestId", json);
    Assert.Contains("req-002", json);
  }

  #endregion

  #region ForceCommMgrRequest - MessagePack..

  /// <summary>
  ///   hu: Request MessagePack szerializáció round-trip
  ///   <br />
  ///   en: Request MessagePack serialization round-trip
  /// </summary>
  [Fact]
  public void Request_MessagePack_RoundTrip()
  {
    var original = new ForceCommMgrRequest("req-003");

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<ForceCommMgrRequest>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
  }

  #endregion

  #region ForceCommMgrResponse - JSON..

  /// <summary>
  ///   hu: Response JSON szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response JSON serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_Json_RoundTrip_Success()
  {
    var original = new ForceCommMgrResponse(
      RequestId: "req-004",
      ResultCode: 0
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<ForceCommMgrResponse>(json, CJsonOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
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
    var original = new ForceCommMgrResponse(
      RequestId: "req-005",
      ResultCode: 100,
      ErrorMessage: "CommMgr kommunikációs hiba"
    );

    var json = JsonSerializer.Serialize(original, CJsonOptions);
    var deserialized = JsonSerializer.Deserialize<ForceCommMgrResponse>(json, CJsonOptions);

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
    var response = new ForceCommMgrResponse(
      RequestId: "req-006",
      ResultCode: 0
    );

    var json = JsonSerializer.Serialize(response, CJsonOptions);

    Assert.DoesNotContain("errorMessage", json);
  }

  #endregion

  #region ForceCommMgrResponse - MessagePack..

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (sikeres válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (successful response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Success()
  {
    var original = new ForceCommMgrResponse(
      RequestId: "req-007",
      ResultCode: 0
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<ForceCommMgrResponse>(bytes, CMsgpackOptions);

    Assert.NotNull(deserialized);
    Assert.Equal(original.RequestId, deserialized.RequestId);
    Assert.Equal(original.ResultCode, deserialized.ResultCode);
    Assert.Null(deserialized.ErrorMessage);
  }

  /// <summary>
  ///   hu: Response MessagePack szerializáció round-trip (hibás válasz)
  ///   <br />
  ///   en: Response MessagePack serialization round-trip (error response)
  /// </summary>
  [Fact]
  public void Response_MessagePack_RoundTrip_Error()
  {
    var original = new ForceCommMgrResponse(
      RequestId: "req-008",
      ResultCode: 100,
      ErrorMessage: "Timeout"
    );

    var bytes = MessagePackSerializer.Serialize(original, CMsgpackOptions);
    var deserialized = MessagePackSerializer.Deserialize<ForceCommMgrResponse>(bytes, CMsgpackOptions);

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
    var request = new ForceCommMgrRequest("req-009");

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
    var response = new ForceCommMgrResponse("req-010", 0);

    Assert.IsAssignableFrom<INavIResponse>(response);
    Assert.IsAssignableFrom<QuantumAE.Api.IQaeResponse>(response);
  }

  #endregion
}
