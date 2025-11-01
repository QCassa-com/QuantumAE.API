namespace QuantumAE.Api.Controls;

/// <summary>
///   hu: Kapcsolat bontási kérés rekord
///   <br />
///   en: Disconnect request record
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <remarks>
///   hu: Token = SessionId
///   <br />
///   en: Token = SessionId
/// </remarks>
public record TDisconnectRequest(string RequestId);