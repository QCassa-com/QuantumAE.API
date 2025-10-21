using JetBrains.Annotations;

namespace QuantumAE.Api;

/// <summary>
/// hu: A kérések közös mezőit leíró interfész (mintában szereplő stílus).
/// <br/>
/// en: Interface describing common request fields (style per sample).
/// </summary>
[PublicAPI]
public interface IRequest
{
  /// <summary>
  /// hu: Kérés egyedi azonosítója
  /// <br/>
  /// en: Unique identifier of the request
  /// </summary>
  string RequestId { get; init; }
}