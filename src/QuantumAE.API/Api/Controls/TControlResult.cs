namespace QuantumAE.Api;

/// <summary>
///   hu: Eszköz vezérlés
///   <br />
///   en: Device control
/// </summary>
public enum TControlResult
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
  InvalidXsd = TResultCodes.GeneralResult
}