namespace QuantumAE.Models;

/// <summary>
///   hu: LED állapot információ
///   <br />
///   en: LED status information
/// </summary>
public enum TLedState
{
  /// <summary>
  ///   0 = Ismeretlen
  /// </summary>
  Unknown,

  /// <summary>
  ///   1 = Ki
  /// </summary>
  Off,

  /// <summary>
  ///   2 = Be
  /// </summary>
  On,

  /// <summary>
  ///   3 = Lassú villogás
  /// </summary>
  SlowBlink,

  /// <summary>
  ///   4 = Gyors villogás
  /// </summary>
  FastBlink
}