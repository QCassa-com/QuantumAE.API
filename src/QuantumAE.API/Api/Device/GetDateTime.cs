namespace QuantumAE.Api.Device;

/// <summary>
///   hu: Dátum és idő lekérdezése
///   <br />
///   en: Date and time query
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique request identifier
/// </param>
/// <remarks>
///   GET /device/getDateTime?RequestId={ARequestId}
/// </remarks>
public record GetDateTimeRequest(string RequestId) : IDeviceRequest;

/// <summary>
///   hu: Dátum és idő lekérdezése válasz
///   <br />
///   en: Date and time query response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique request identifier
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres)
///   <br />
///   en: Result code (0 = success)
/// </param>
/// <param name="DateTime">
///   hu: Aktuális dátum és idő
///   <br />
///   en: Current date and time
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
public record GetDateTimeResponse(string RequestId, int ResultCode, DateTime DateTime, string? ErrorMessage = null) : IDeviceResponse;