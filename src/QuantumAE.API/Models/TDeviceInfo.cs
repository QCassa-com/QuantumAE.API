using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Az Adóügyi Egység összefoglaló adatai.
///   <br />
///   en: The summary data of the Tax Device.
/// </summary>
/// <param name="DeviceName">
///   hu: Az adóügyi egység megnevezése.
///   <br />
///   en: The name of the Tax Device.
/// </param>
/// <param name="HardwareVersion">
///   hu: Hardver verzió.
///   <br />
///   en: Hardware version.
/// </param>
/// <param name="SoftwareVersion">
///   hu: Szoftver verzió.
///   <br />
///   en: Software version.
/// </param>
/// <param name="SerialNumber">
///   hu: Sorozatszám.
///   <br />
///   en: Serial number.
/// </param>
/// <param name="ApNumber">
///   hu: Az Adóügyi Egység azonosító száma.
///   <br />
///   en: The identification number of the Tax Device.
/// </param>
[PublicAPI]
public record TDeviceInfo(string DeviceName, string HardwareVersion, string SoftwareVersion, string SerialNumber, string ApNumber);