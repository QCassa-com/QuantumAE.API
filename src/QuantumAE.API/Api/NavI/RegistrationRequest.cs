using JetBrains.Annotations;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Regisztrációs folyamat indítása.
///   <br />
///   en: Start registration process.
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
///   hu: Üzembe helyezési kód (ÜH kód) - 16 számjegyből álló kód (REQ-075)
///   <br />
///   en: Registration number (commissioning code) - 16-digit code (REQ-075)
/// </param>
[PublicAPI]
public sealed record RegistrationRequest(string RequestId, string ApNumber, string RegistrationNumber): INavIRequest;

/// <summary>
///   hu: Regisztrációs folyamat indítása válasz.
///   <br />
///   en: Start registration process response.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), különben hiba kód a TNavIResult
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record RegistrationResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;