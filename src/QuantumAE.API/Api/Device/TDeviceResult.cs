namespace QuantumAE.Api;

/// <summary>
///   hu: Eszköz műveletek eredménykódjai.
///   <br />
///   en: Device operation result codes.
/// </summary>
public enum TDeviceResult
{
  /// <summary>
  ///   hu: Sikeres művelet.
  ///   <br />
  ///   en: Successful operation.
  /// </summary>
  Success,

  /// <summary>
  ///   hu: Eszköz nem található.
  ///   <br />
  ///   en: Device not found.
  /// </summary>
  DeviceNotFound = TResultCodes.DeviceResult,

  /// <summary>
  ///   hu: Belső hiba.
  ///   <br />
  ///   en: Internal error.
  /// </summary>
  InternalError
}