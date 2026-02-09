using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Regisztrációs státusz lekérdezési kérés
///   <br />
///   en: Registration status request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record RegistrationStatusRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Regisztrációs státusz lekérdezési válasz
///   <br />
///   en: Registration status response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker).
///   <br />
///   en: Result code (0 = success).
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén).
///   <br />
///   en: Error message (on error).
/// </param>
[PublicAPI]
public sealed record RegistrationStatusResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;