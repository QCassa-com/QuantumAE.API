using QuantumAE.Models;

namespace QuantumAE.Api.Device;

/// <summary>
///   hu: LED-ek állapoatának lekérdezése
///   <br />
///   en: LED status query
/// </summary>
/// <param name="RequestId">
///   hu: A kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <remarks>
///   GET /device/getDateTime?RequestId={ARequestId}
/// </remarks>
public record GetLedStatusRequest(string RequestId) : IDeviceRequest;

/// <summary>
///   hu: LED-ek állapotának lekérdezése válasz
///   <br />
///   en: LED status query response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), ha nem, akkor hiba kód
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="LedStatus">
///   hu: LED-ek állapotainak listája
///   <br />
///   en: List of LED states
/// </param>
public record GetLedStatusResponse(string RequestId, int ResultCode, TLedState[] LedStatus) : IDeviceResponse;