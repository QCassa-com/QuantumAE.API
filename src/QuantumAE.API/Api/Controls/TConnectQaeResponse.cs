namespace QuantumAE.Api.Controls;

/// <summary>
///  hu: Csatlakozás válasz
///  <br />
///  en: Connect response
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="SessionId">
///   hu: Munkamenet azonosító
///   <br />
///   en: Session identifier
/// </param>
public record TConnectQaeResponse(string RequestId, int ResultCode, string SessionId) : IQaeResponse;