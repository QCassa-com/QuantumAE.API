using JetBrains.Annotations;

namespace QuantumAE.Api.NavI;


/// <summary>
///   hu: Regisztrációs státusz lekérdezés kérés
///   <br />
///   en: Registration status query request
/// </summary>
/// <param name="RequestId"></param>
[PublicAPI]
public sealed record GetRegistrationStatusRequest(string RequestId) : INavIRequest;

/// <summary>
///   hu: Regisztrációs státusz lekérdezés válasz
///   <br />
///   en: Registration status query response
/// </summary>
[PublicAPI]
public sealed record GetRegistrationStatusResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;