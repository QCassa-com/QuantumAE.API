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
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: A rendelés nem található.
  ///   <br />
  ///   en: Order not found.
  /// </summary>
  OrderNotFound = TResultCodes.OrderResult,

  /// <summary>
  ///   hu: A művelet nem támogatott.
  ///   <br />
  ///   en: Operation not supported.
  /// </summary>
  NotSupported,

  /// <summary>
  ///   hu: A rendelés már nyitva van.
  ///   <br />
  ///   en: Order is already open.
  /// </summary>
  Opened,

  /// <summary>
  ///   hu: A rendelés nincs nyitva vagy le van zárva.
  ///   <br />
  ///   en: Order is not open or is closed.
  /// </summary>
  NotOpenOrClosed,

  /// <summary>
  ///   hu: A rendelés üres.
  ///   <br />
  ///   en: Order is empty.
  /// </summary>
  Empty,

  /// <summary>
  ///   hu: A rendelés le van zárva.
  ///   <br />
  ///   en: Order is closed.
  /// </summary>
  Closed,

  /// <summary>
  ///   hu: A bizonylat offline módban lett mentve, később kerül beküldésre a NAV-nak.
  ///   <br />
  ///   en: Document was saved in offline mode, will be sent to NAV later.
  /// </summary>
  NavOffline,

  /// <summary>
  ///   hu: Bizonylat feldolgozási hiba.
  ///   <br />
  ///   en: Document processing error.
  /// </summary>
  DocumentError
}

//[PublicAPI]
public enum TOrderItemResult
{
  Success,
  NameAndBarCodeEmpty = TResultCodes.OrderItemResult,
  ProductNotFound,
  InvalidQuantity,
  InvalidPrice,
  LineNotFound,
  AlreadyStornoed,
  CannotUpdateStornoed
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
