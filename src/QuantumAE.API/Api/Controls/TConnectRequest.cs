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
public record TConnectRequest(string RequestId, string ApNumber);