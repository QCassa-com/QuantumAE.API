using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;


/// <summary>
///   hu: Nyitott rendelések azonosítóinak lekérdezési kérése
///   <br />
///   en: Get open orders identifiers request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
public record GetOpenIdsRequest(string RequestId): IOrderRequest;


/// <summary>
///   hu: Nyitott rendelés azonosítók lekérdezésének válasza
///   <br />
///   en: Response of open order identifiers query
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
/// <param name="OrderIds">
///   hu: Nyitott rendelések egyedi azonosítóinak listája
///   <br />
///   en: List of unique identifiers of open orders
/// </param>
[PublicAPI]
public sealed record GetOpenIdsResponse(string RequestId, int ResultCode, IReadOnlyList<string>? OrderIds): IQaeResponse;