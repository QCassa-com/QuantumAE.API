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
/// <param name="RegistrationNumber">
///   hu: NAV regisztrációs szám (AP szám)
///   <br />
///   en: NAV registration number (AP number)
/// </param>
[PublicAPI]
public sealed record RegistrationRequest(string RequestId, string RegistrationNumber): INavIRequest;

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
[PublicAPI]
public sealed record RegistrationResponse(string RequestId, int ResultCode) : INavIResponse;
