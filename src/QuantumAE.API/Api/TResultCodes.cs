using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///  hu: API válaszkódok konstansokat tartalmazó osztály
/// <br />
///  en: API response codes constants class
/// </summary>
[PublicAPI]
public class TResultCodes
{
  /// <summary>
  ///  hu: Rendeléssel kapcsolatos hibakódok
  ///  <br />
  ///  en: Order related error codes
  /// </summary>
  public const int OrderResult = 0x1000;
  
  /// <summary>
  /// 
  /// </summary>
  public const int OrderItemResult = 0x2000;
  
  /// <summary>
  ///  hu: Vezérléssel kapcsolatos hiba kódok.
  ///  <br />
  ///  en: Control related error codes.
  /// </summary>
  public const int ControlResult = 0x3000;
  
  /// <summary>
  ///  hu: Az eszközzel kapcsolatos hibakódok.
  ///  <br />
  ///  en: Device related error codes.
  /// </summary>
  public const int DeviceResult = 0x4000;

  /// <summary>
  ///  hu: NAV-I műveletekkel kapcsolatos hibakódok.
  ///  <br />
  ///  en: NAV-I operation related error codes.
  /// </summary>
  public const int NavIResult = 0x5000;
}

//[PublicAPI]
public enum TControlResult
{
  Success,
  SessionNotFound = TResultCodes.ControlResult,
  SessionInvalid,
  InternalError
}

//[PublicAPI]
public enum TOrderResult
{
  Success,
  OrderNotFound = TResultCodes.OrderResult,
  NotSupported,
  Opened,
  NotOpenOrClosed,
  Empty,
  Closed
}

//[PublicAPI]
public enum TOrderItemResult
{
  Success,
  NameAndBarCodeEmpty = TResultCodes.OrderItemResult,
  ProductNotFound,
  InvalidQuantity,
  InvalidPrice,
}

//[PublicAPI]
public enum TDeviceResult
{
  Success,
  DeviceNotFound = TResultCodes.DeviceResult,
  InternalError
}

//[PublicAPI]
public enum TNavIResult
{
  Success,
  Registered = TResultCodes.NavIResult,
  NavIError,
  AlreadyInProgress,
  Failure
}
