using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Sessions;

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
public sealed record ConnectRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  [property: ApNumber]
  string ApNumber
) : ISessionsRequest;

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
/// <param name="ResultCode">
///   hu: Eredménykód (0 = sikeres)
///   <br />
///   en: Result code (0 = success)
/// </param>
/// <param name="SessionId">
///   hu: Munkamenet azonosító
///   <br />
///   en: Session identifier
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public record ConnectResponse(string RequestId, int ResultCode, string SessionId, string? ErrorMessage = null) : ISessionsResponse;
