using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
///   hu: A válaszok közös mezőit leíró rekord (mintában szereplő stílus).
///   <br />
///   en: Record describing common response fields (style per sample).
/// </summary>
[PublicAPI]
public interface IQaeResponse
{
  /// <summary>
  ///   hu: Kérés egyedi azonosítója
  ///   <br />
  ///   en: Unique identifier of the request
  /// </summary>
  string RequestId { get; init; }

  /// <summary>
  ///   hu: Eredménykód (0 = siker), különben hiba kód
  ///   <br />
  ///   en: Result code (0 = success), otherwise error code
  /// </summary>
  int ResultCode { get; init; }
}