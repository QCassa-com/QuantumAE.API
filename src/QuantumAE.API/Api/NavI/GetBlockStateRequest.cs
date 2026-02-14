using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Blokkolási állapot lekérdezési kérés - a pénztárgép aktuális blokkolási állapotának lekérdezése.
///   <br />
///   en: Block state query request - query the current block state of the cash register.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record GetBlockStateRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Blokkolási állapot lekérdezési válasz - a pénztárgép aktuális blokkolási állapota.
///   <br />
///   en: Block state query response - the current block state of the cash register.
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
/// <param name="IsBlocked">
///   hu: Az eszköz blokkolva van-e (true = BLOCKED, false = UNBLOCKED vagy nincs adat)
///   <br />
///   en: Whether the device is blocked (true = BLOCKED, false = UNBLOCKED or no data)
/// </param>
/// <param name="BlockedSince">
///   hu: Blokkolás kezdete (ha blokkolva van)
///   <br />
///   en: Block start time (if blocked)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record GetBlockStateResponse(
  string RequestId,
  int ResultCode,
  bool IsBlocked = false,
  string? BlockedSince = null,
  string? ErrorMessage = null
) : INavIResponse;
