using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Regisztrációs státusz lekérdezés kérés
///   <br />
///   en: Registration status query request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója.
///   <br />
///   en: Unique identifier of the request.
/// </param>
[PublicAPI]
public sealed record GetRegistrationStatusRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Regisztrációs státusz lekérdezés válasz
///   <br />
///   en: Registration status query response
/// </summary>
[PublicAPI]
public sealed record GetRegistrationStatusResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;