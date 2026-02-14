using JetBrains.Annotations;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Bizonylat műveletek eredménykódjai.
///   <br />
///   en: Document operation result codes.
/// </summary>
[PublicAPI]
public enum TDocumentResult
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
  ///   hu: Nem létező vagy letiltott pénzmozgás típus.
  ///   <br />
  ///   en: Cash flow type not found or disabled.
  /// </summary>
  InvalidCashFlowType,

  /// <summary>
  ///   hu: Üres fizetőeszköz lista.
  ///   <br />
  ///   en: At least one payment instrument is required.
  /// </summary>
  EmptyInstruments,

  /// <summary>
  ///   hu: Az adóügyi nap már le lett zárva.
  ///   <br />
  ///   en: The taxation day has already been closed.
  /// </summary>
  DayAlreadyClosed,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError
}
