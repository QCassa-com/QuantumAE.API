using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Tanúsítvány ellenőrzés kényszerítési kérés - diagnosztikai célú, azonnali tanúsítvány ellenőrzés indítása.
///   <br />
///   en: Force certificate check request - trigger immediate certificate check for diagnostics.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record ForceCertificateCheckRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Tanúsítvány ellenőrzés kényszerítési válasz - visszaigazolás a művelet elindításáról.
///   <br />
///   en: Force certificate check response - confirmation that the operation has been triggered.
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
public sealed record ForceCertificateCheckResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;
