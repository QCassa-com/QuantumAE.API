using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: GetIds válasz
///   <br />
///   en: GetIds response
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
/// <param name="Count">
///   hu: Azonosítók száma
///   <br />
///   en: Number of identifiers
/// </param>
/// <param name="Ids">
///   hu: Azonosítók listája
///   <br />
///   en: List of identifiers
/// </param>
[PublicAPI]
public sealed record TGetIdsQaeResponse(string RequestId, int ResultCode, int Count, IReadOnlyList<string> OrderIds)
  : IQaeResponse;