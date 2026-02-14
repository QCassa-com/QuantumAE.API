using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: CommMgr heartbeat kényszerítési kérés - diagnosztikai célú, azonnali CommMgr kommunikáció indítása.
///   <br />
///   en: Force CommMgr heartbeat request - trigger immediate CommMgr communication for diagnostics.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record ForceCommMgrRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: CommMgr heartbeat kényszerítési válasz - visszaigazolás a művelet elindításáról.
///   <br />
///   en: Force CommMgr heartbeat response - confirmation that the operation has been triggered.
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
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record ForceCommMgrResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;
