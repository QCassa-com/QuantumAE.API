using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.NavI;

/// <summary>
///   hu: Szoftverfrissítés állapot lekérdezési kérés - a firmware frissítés állapotának és határidejének lekérdezése.
///   <br />
///   en: Software update status query request - query the firmware update status and installation deadline.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
[PublicAPI]
public sealed record GetSoftwareUpdateStatusRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : INavIRequest;

/// <summary>
///   hu: Szoftverfrissítés állapot lekérdezési válasz - a firmware frissítés állapota és telepítési határideje.
///   <br />
///   en: Software update status query response - firmware update status and installation deadline.
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
/// <param name="UpdateAvailable">
///   hu: Van-e elérhető frissítés
///   <br />
///   en: Whether an update is available
/// </param>
/// <param name="FirmwareInstallDueDate">
///   hu: A firmware telepítési határideje (DocumentDateType formátum)
///   <br />
///   en: Firmware installation due date (DocumentDateType format)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public sealed record GetSoftwareUpdateStatusResponse(
  string RequestId,
  int ResultCode,
  bool UpdateAvailable = false,
  string? FirmwareInstallDueDate = null,
  string? ErrorMessage = null
) : INavIResponse;
