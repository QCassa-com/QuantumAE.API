namespace QuantumAE.Api;

/// <summary>
///   hu: NAV-I műveletek eredménykódjai.
///   <br />
///   en: NAV-I operation result codes.
/// </summary>
public enum TNavIResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Már regisztrálva van.
  ///   <br />
  ///   en: Already registered.
  /// </summary>
  Registered = TResultCodes.NavIResult,

  /// <summary>
  ///   hu: NAV-I kommunikációs hiba.
  ///   <br />
  ///   en: NAV-I communication error.
  /// </summary>
  NavIError,

  /// <summary>
  ///   hu: Művelet már folyamatban van.
  ///   <br />
  ///   en: Operation already in progress.
  /// </summary>
  AlreadyInProgress,

  /// <summary>
  ///   hu: Sikertelen művelet.
  ///   <br />
  ///   en: Operation failed.
  /// </summary>
  Failure
}