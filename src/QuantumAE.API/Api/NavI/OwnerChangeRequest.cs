using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Tulajdonosváltás (átszemélyesítés) kérés - az e-pénztárgép új üzemeltetőhöz történő átadása.
///   <br />
///   en: Owner change (re-personalization) request - transfer the e-cash register to a new operator.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ApNumber">
///   hu: AP szám (Adóügyi Pénztárgép szám) - a NAV által kiadott azonosító
///   <br />
///   en: AP number (Tax Cash Register number) - identifier issued by NAV
/// </param>
/// <param name="RegistrationNumber">
///   hu: Átszemélyesítési kód (ÜH kód) - 16 számjegyből álló kód (REQ-075)
///   <br />
///   en: Re-personalization code (registration number) - 16-digit code (REQ-075)
/// </param>
[PublicAPI]
public sealed record OwnerChangeRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  [property: ApNumber]
  string ApNumber,

  [property: Required]
  [property: NotEmptyString]
  [property: StringLength(16, MinimumLength = 16)]
  string RegistrationNumber
) : INavIRequest;

/// <summary>
///   hu: Tulajdonosváltás (átszemélyesítés) válasz - a NAV-I visszaigazolása a tulajdonosváltásról.
///   <br />
///   en: Owner change (re-personalization) response - NAV-I confirmation of the owner change.
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
/// <param name="NewOperatorName">
///   hu: Az új üzemeltető neve (ha a NAV visszaküldte)
///   <br />
///   en: New operator name (if NAV returned it)
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
public sealed record OwnerChangeResponse(
  string RequestId,
  int ResultCode,
  string? NewOperatorName = null,
  string? BlockUnblock = null,
  string? ErrorMessage = null
) : INavIResponse;
