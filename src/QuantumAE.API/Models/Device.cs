using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Az Adóügyi Egység összefoglaló adatai.
///   <br />
///   en: The summary data of the Tax Device.
/// </summary>
/// <param name="ApNumber">
///   hu: Az Adóügyi Egység azonosító száma.
///   <br />
///   en: The identification number of the Tax Device.
/// </param>
[PublicAPI]
public record TDeviceInfo(string ApNumber);