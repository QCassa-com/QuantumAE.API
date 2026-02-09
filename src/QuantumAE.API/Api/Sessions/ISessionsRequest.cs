using JetBrains.Annotations;

namespace QuantumAE.Api.Sessions;

/// <summary>
///   hu: Munkamenet kérés alap interfész
///   <br />
///   en: Session request base interface
/// </summary>
[PublicAPI]
public interface ISessionsRequest : IQaeRequest
{
}

/// <summary>
///   hu: Munkamenet válasz alap interfész
///   <br />
///   en: Session response base interface
/// </summary>
[PublicAPI]
public interface ISessionsResponse : IQaeResponse
{
}
