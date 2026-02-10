namespace QuantumAE.Api;

/// <summary>
///   hu: Általános eredmény kódok
///   <br />
///   en: General result codes.
/// </summary>
public enum TGeneralResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Munkamenet nem található.
  ///   <br />
  ///   en: Session not found.
  /// </summary>
  SessionNotFound = TResultCodes.GeneralResult,

  /// <summary>
  ///   hu: Érvénytelen munkamenet.
  ///   <br />
  ///   en: Invalid session.
  /// </summary>
  SessionInvalid,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError,

  /// <summary>
  ///   hu: Érvénytelen kérés.
  ///   <br />
  ///   en: Invalid request.
  /// </summary>
  InvalidRequest,

  /// <summary>
  ///   hu: Validáció sikertelen.
  ///   <br />
  ///   en: Validation failed.
  /// </summary>
  ValidationFailed,

  /// <summary>
  ///   hu: Érvénytelen JSON kérés törzs.
  ///   <br />
  ///   en: Invalid JSON request body.
  /// </summary>
  InvalidJsonBody
}