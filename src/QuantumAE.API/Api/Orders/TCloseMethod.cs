using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Zárási mód
///   <br />
///   en: Close method
/// </summary>
[PublicAPI]
public enum TCloseMethod
{
  /// <summary>
  ///   hu: Ismeretlen zárási mód
  ///   <br />
  ///   en: Unknown close method
  /// </summary>
  Unknown = 0,

  /// <summary>
  ///   hu: Nyomtatás zárási módként
  ///   <br />
  ///   en: Print as close method
  /// </summary>
  Print = 1
}