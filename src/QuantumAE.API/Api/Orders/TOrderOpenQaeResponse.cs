namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés nyitási válasz
///   <br />
///   en: Order opening response
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
/// <param name="OrderId">
///   hu: Rendelés egyedi azonosítója
///   <br />
///   en: Unique identifier of the order
/// </param>
public sealed record TOrderOpenQaeResponse(string RequestId, int ResultCode, string OrderId) : IQaeResponse;