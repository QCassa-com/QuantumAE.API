using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Device;

/// <summary>
///   hu: LED-ek állapotának lekérdezése
///   <br />
///   en: LED status query
/// </summary>
/// <param name="RequestId">
///   hu: A kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <remarks>
///   GET /device/getLedStatus?RequestId={ARequestId}
/// </remarks>
public sealed record GetLedStatusRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : IDeviceRequest;

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
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
public record GetLedStatusResponse(string RequestId, int ResultCode, TLedState[] LedStatus, string? ErrorMessage = null) : IDeviceResponse;