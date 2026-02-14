using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Tanúsítvány állapot lekérdezési kérés - a hitelesítési és aláíró tanúsítványok lejárati idejének lekérdezése.
///   <br />
///   en: Certificate status query request - query the expiry dates of authentication and signing certificates.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record GetCertificateStatusRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Tanúsítvány állapot lekérdezési válasz - a tanúsítványok lejárati ideje és a hátralévő napok száma.
///   <br />
///   en: Certificate status query response - certificate expiry dates and days until expiry.
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
/// <param name="AuthCertExpiry">
///   hu: Hitelesítési tanúsítvány lejárati ideje (ISO 8601 formátum)
///   <br />
///   en: Authentication certificate expiry date (ISO 8601 format)
/// </param>
/// <param name="SignCertExpiry">
///   hu: Aláíró tanúsítvány lejárati ideje (ISO 8601 formátum)
///   <br />
///   en: Signing certificate expiry date (ISO 8601 format)
/// </param>
/// <param name="DaysUntilExpiry">
///   hu: Napok száma a legkorábbi lejáratig (negatív = már lejárt)
///   <br />
///   en: Days until earliest expiry (negative = already expired)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record GetCertificateStatusResponse(
  string RequestId,
  int ResultCode,
  string? AuthCertExpiry = null,
  string? SignCertExpiry = null,
  int? DaysUntilExpiry = null,
  string? ErrorMessage = null
) : INavIResponse;
