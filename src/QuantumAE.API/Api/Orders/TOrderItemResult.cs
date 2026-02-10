namespace QuantumAE.Api;

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