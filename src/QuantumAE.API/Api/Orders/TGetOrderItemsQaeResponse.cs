using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: GetItems válasz
///   <br />
///   en: GetItems response
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
public sealed record TGetOrderItemsQaeResponse(string RequestId, int ResultCode, TSellItems? SellItems) : IQaeResponse;