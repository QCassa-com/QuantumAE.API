using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///   hu: QuantumAE API általános válasz rekord
///   <br />
///   en: General response record of QuantumAE API
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
[PublicAPI]
public sealed record TApiResult(string RequestId, int ResultCode) : IQaeResponse;