using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Egyszerű státusz válasz
///   <br />
///   en: Simple acknowledgment response
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
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record TSimpleAckQaeResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : IQaeResponse;