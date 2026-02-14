using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Offline szinkronizálás kényszerítési kérés - diagnosztikai célú, azonnali offline szinkronizálás indítása.
///   <br />
///   en: Force offline sync request - trigger immediate offline synchronization for diagnostics.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record ForceOfflineSyncRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Offline szinkronizálás kényszerítési válasz - visszaigazolás a művelet elindításáról.
///   <br />
///   en: Force offline sync response - confirmation that the operation has been triggered.
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
public sealed record ForceOfflineSyncResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;
