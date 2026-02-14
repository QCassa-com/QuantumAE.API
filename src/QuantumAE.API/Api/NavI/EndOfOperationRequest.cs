using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Üzemeltetés befejezési kérés - a pénztárgép üzemen kívül helyezése a NAV-I felé.
///   <br />
///   en: End of operation request - deactivate the cash register towards NAV-I.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record EndOfOperationRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Üzemeltetés befejezési válasz - a NAV-I visszaigazolása az üzemen kívül helyezésről.
///   <br />
///   en: End of operation response - NAV-I confirmation of deactivation.
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
public sealed record EndOfOperationResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;
