using JetBrains.Annotations;

namespace QuantumAE.Api.Reports;

/// <summary>
///   hu: Riport műveletek eredménykódjai.
///   <br />
///   en: Report operation result codes.
/// </summary>
[PublicAPI]
public enum TReportResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Az eszköz nincs regisztrálva a NAV-nál.
  ///   <br />
  ///   en: Device is not registered with NAV.
  /// </summary>
  DeviceNotRegistered = TResultCodes.ReportResult,

  /// <summary>
  ///   hu: A mai adóügyi nap már meg lett nyitva.
  ///   <br />
  ///   en: Taxation day is already open for today.
  /// </summary>
  DayAlreadyOpen,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError
}
