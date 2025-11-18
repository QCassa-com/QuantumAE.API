using JetBrains.Annotations;

namespace QuantumAE.Api.Controls;

/// <summary>
///   hu: Pénztárgép csatlakoztatása az Adóügyi Egységhez.
///   <br />
///   en: Connect cash register to the Tax Unit.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ApNumber">
///   hu: AP szám
///   <br />
///   en: AP number
/// </param>
[PublicAPI]
public record ConnectRequest(string RequestId, string ApNumber): IControlsRequest;

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
[PublicAPI]
public record ConnectResponse(string RequestId, int ResultCode, string SessionId) : IControlsResponse;