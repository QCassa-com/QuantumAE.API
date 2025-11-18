using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés adatinak lekérdezése
///   <br />
///   en: Get order items request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///   hu: A rendelés egyedi azonosítója
///   <br />
///   en: Unique identifier of the order
/// </param>
[PublicAPI]
public sealed record GetOrderRequest(string RequestId, int OrderId) : IOrderRequest;

/// <summary>
///   hu: Rendelés adatainak lekérdezésére adott válasz
///   <br />
///   en: Get order items response
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
/// <param name="SellItems">
///   hu: Eladási tételek (siker esetén)
///   <br />
///   en: Sell items (on success)
/// </param>
[PublicAPI]
public sealed record GetOrderItemsResponse(string RequestId, int ResultCode, TOrder Order) : IOrderResponse;