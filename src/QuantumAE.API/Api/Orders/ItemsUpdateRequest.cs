using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Tétel módosítása a rendelésben
///   <br />
///   en: Update an item in the order
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///   hu: Rendelés egyedi azonosítója
///   <br />
///   en: Unique identifier of the order
/// </param>
/// <param name="LineNumber">
///   hu: Módosítandó tétel sorszáma
///   <br />
///   en: Line number of the item to update
/// </param>
/// <param name="Item">
///   hu: Módosítandó adatok (csak a megadott mezők frissülnek)
///   <br />
///   en: Data to update (only specified fields will be updated)
/// </param>
[PublicAPI]
public sealed record ItemsUpdateRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string OrderId,

  [property: Required]
  [property: Range(1, int.MaxValue)]
  int LineNumber,

  [property: Required]
  TOrderItemUpdate Item
) : IOrderRequest;

/// <summary>
///   hu: Tétel módosítás válasz
///   <br />
///   en: Item update response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), különben hiba kód
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (opcionális)
///   <br />
///   en: Error message (optional)
/// </param>
[PublicAPI]
public sealed record ItemsUpdateResponse(
  string RequestId,
  int ResultCode,
  string? ErrorMessage = null
) : IOrderResponse;
