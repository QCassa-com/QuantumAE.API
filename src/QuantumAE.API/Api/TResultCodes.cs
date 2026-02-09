using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///   hu: API válaszkódok konstansokat tartalmazó osztály.
///   <br />
///   en: API response codes constants class.
/// </summary>
[PublicAPI]
public class TResultCodes
{
  /// <summary>
  ///   hu: Rendeléssel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Order related error codes.
  /// </summary>
  public const int OrderResult = 0x1000;
  
  /// <summary>
  ///   hu: Rendelési tételekkel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Order item related error codes.
  /// </summary>
  public const int OrderItemResult = 0x2000;
  
  /// <summary>
  ///   hu: Vezérléssel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Control related error codes.
  /// </summary>
  public const int ControlResult = 0x3000;
  
  /// <summary>
  ///   hu: Az eszközzel kapcsolatos hibakódok.
  ///   <br />
  ///   en: Device related error codes.
  /// </summary>
  public const int DeviceResult = 0x4000;

  /// <summary>
  ///   hu: NAV-I műveletekkel kapcsolatos hibakódok.
  ///   <br />
  ///   en: NAV-I operation related error codes.
  /// </summary>
  public const int NavIResult = 0x5000;
}

/// <summary>
///   hu: Vezérlési műveletek eredménykódjai.
///   <br />
///   en: Control operation result codes.
/// </summary>
public enum TControlResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Munkamenet nem található.
  ///   <br />
  ///   en: Session not found.
  /// </summary>
  SessionNotFound = TResultCodes.ControlResult,

  /// <summary>
  ///   hu: Érvénytelen munkamenet.
  ///   <br />
  ///   en: Invalid session.
  /// </summary>
  SessionInvalid,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError,

  /// <summary>
  ///   hu: Érvénytelen kérés.
  ///   <br />
  ///   en: Invalid request.
  /// </summary>
  InvalidRequest,

  /// <summary>
  ///   hu: Validáció sikertelen.
  ///   <br />
  ///   en: Validation failed.
  /// </summary>
  ValidationFailed
}

/// <summary>
///   hu: Rendelés műveletek eredménykódjai.
///   <br />
///   en: Order operation result codes.
/// </summary>
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
  DocumentError,

  /// <summary>
  ///   hu: Számla esetén kötelező a vevő adatok megadása.
  ///   <br />
  ///   en: Customer information is required for invoices.
  /// </summary>
  CustomerRequired
}

/// <summary>
///   hu: Rendelés tétel műveletek eredménykódjai.
///   <br />
///   en: Order item operation result codes.
/// </summary>
public enum TOrderItemResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Név és vonalkód is üres.
  ///   <br />
  ///   en: Name and barcode are both empty.
  /// </summary>
  NameAndBarCodeEmpty = TResultCodes.OrderItemResult,

  /// <summary>
  ///   hu: Termék nem található.
  ///   <br />
  ///   en: Product not found.
  /// </summary>
  ProductNotFound,

  /// <summary>
  ///   hu: Érvénytelen mennyiség.
  ///   <br />
  ///   en: Invalid quantity.
  /// </summary>
  InvalidQuantity,

  /// <summary>
  ///   hu: Érvénytelen ár.
  ///   <br />
  ///   en: Invalid price.
  /// </summary>
  InvalidPrice,

  /// <summary>
  ///   hu: Sor nem található.
  ///   <br />
  ///   en: Line not found.
  /// </summary>
  LineNotFound,

  /// <summary>
  ///   hu: Tétel már sztornózva van.
  ///   <br />
  ///   en: Item is already cancelled.
  /// </summary>
  AlreadyStornoed,

  /// <summary>
  ///   hu: Sztornózott tétel nem módosítható.
  ///   <br />
  ///   en: Cannot update cancelled item.
  /// </summary>
  CannotUpdateStornoed
}

/// <summary>
///   hu: Eszköz műveletek eredménykódjai.
///   <br />
///   en: Device operation result codes.
/// </summary>
public enum TDeviceResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Eszköz nem található.
  ///   <br />
  ///   en: Device not found.
  /// </summary>
  DeviceNotFound = TResultCodes.DeviceResult,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError
}

/// <summary>
///   hu: NAV-I műveletek eredménykódjai.
///   <br />
///   en: NAV-I operation result codes.
/// </summary>
public enum TNavIResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Már regisztrálva van.
  ///   <br />
  ///   en: Already registered.
  /// </summary>
  Registered = TResultCodes.NavIResult,

  /// <summary>
  ///   hu: NAV-I kommunikációs hiba.
  ///   <br />
  ///   en: NAV-I communication error.
  /// </summary>
  NavIError,

  /// <summary>
  ///   hu: Művelet már folyamatban van.
  ///   <br />
  ///   en: Operation already in progress.
  /// </summary>
  AlreadyInProgress,

  /// <summary>
  ///   hu: Sikertelen művelet.
  ///   <br />
  ///   en: Operation failed.
  /// </summary>
  Failure
}
