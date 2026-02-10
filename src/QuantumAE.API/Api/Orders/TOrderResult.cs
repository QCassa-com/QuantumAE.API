namespace QuantumAE.Api;

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