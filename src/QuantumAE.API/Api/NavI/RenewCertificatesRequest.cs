using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Tanúsítvány megújítási kérés
///   <br />
///   en: Certificate renewal request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record RenewCertificatesRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
): INavIRequest;

/// <summary>
///   hu: Tanúsítvány megújítási válasz
///   <br />
///   en: Certificate renewal response
/// </summary>
[PublicAPI]
public sealed record RenewCertificatesResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : INavIResponse;