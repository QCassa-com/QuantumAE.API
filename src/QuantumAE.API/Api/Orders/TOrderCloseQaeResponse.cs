using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Zárási válasz. Siker esetén ResultCode = 0 és a DocumentClose ki van töltve; hiba esetén ResultCode != 0 és a
///   DocumentClose null.
///   <br />
///   en: Close response. On success, ResultCode = 0 and DocumentClose is populated; on error, ResultCode != 0 and
///   DocumentClose is null.
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
/// <param name="DocumentClose">
///   hu: Zárási adatok (ha sikeres a művelet)
///   <br />
///   en: Close data (if the operation is successful)
/// </param>
[PublicAPI]
public sealed record TOrderCloseQaeResponse(string RequestId, int ResultCode, TDocumentClose? DocumentClose) : IQaeResponse;