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
  ///   hu: Az adóügyi nap nincs megnyitva.
  ///   <br />
  ///   en: The fiscal day has not been opened yet.
  /// </summary>
  DayNotOpen,

  /// <summary>
  ///   hu: Érvénytelen pénzmozgás jogcím.
  ///   <br />
  ///   en: Invalid payment title.
  /// </summary>
  InvalidPaymentTitle,

  /// <summary>
  ///   hu: Üres fizetőeszköz lista.
  ///   <br />
  ///   en: At least one payment instrument is required.
  /// </summary>
  EmptyInstruments,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError
}
