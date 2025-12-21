using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Egy tétel hozzáadása a rendeléshez
///   <br />
///   en: Add an item to the order
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
/// <param name="Item">
///   hu: Hozzáadandó tétel adatai
///   <br />
///   en: Data of the item to be added
/// </param>
[PublicAPI]
public sealed record OrderItemsAddRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,
  [property: Required]
  [property: NotEmptyString]
  string OrderId,
  [property: Required]
  TOrderItem Item
) : IOrderRequest;

/// <summary>
///   hu: Tétel hozzáadás válasz
///   <br />
///   en: Item addition response
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
[PublicAPI]
public sealed record OrderItemsAddResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : IOrderResponse;