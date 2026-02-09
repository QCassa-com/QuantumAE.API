using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Sessions;

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
[PublicAPI]
public record DisconnectRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : ISessionsRequest;


/// <summary>
///  hu: Kapcsolat bontási válasz rekord
///  <br />
///  en: Disconnect response record
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker), ha nem, akkor hiba kód
///   <br />
///   en: Result code (0 = success), otherwise error code
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha van)
///   <br />
///   en: Error message (if any)
/// </param>
[PublicAPI]
public record DisconnectResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : ISessionsResponse;
