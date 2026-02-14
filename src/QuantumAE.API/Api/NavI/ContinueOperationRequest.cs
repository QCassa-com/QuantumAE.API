using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Üzemeltetés folytatási kérés - a pénztárgép üzembe visszahelyezése a NAV-I felé.
///   <br />
///   en: Continue operation request - reactivate the cash register towards NAV-I.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record ContinueOperationRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Üzemeltetés folytatási válasz - a NAV-I visszaigazolása az üzembe visszahelyezésről.
///   <br />
///   en: Continue operation response - NAV-I confirmation of reactivation.
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
/// <param name="BlockUnblock">
///   hu: Blokkolási állapot (BLOCK/UNBLOCK), ha a NAV visszaküldte
///   <br />
///   en: Block/unblock state (BLOCK/UNBLOCK), if NAV returned it
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record ContinueOperationResponse(
  string RequestId,
  int ResultCode,
  string? BlockUnblock = null,
  string? ErrorMessage = null
) : INavIResponse;
