using JetBrains.Annotations;
using QuantumAE.Api;

namespace QuantumAE.Models;

/// <summary>
///   hu: A válaszok közös mezőit leíró rekord (mintában szereplő stílus).
///   <br />
///   en: Record describing common response fields (style per sample).
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
public sealed record TApiResult(string RequestId, int ResultCode, string Message): IResponse;