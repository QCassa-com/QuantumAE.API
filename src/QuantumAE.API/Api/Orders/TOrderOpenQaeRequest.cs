namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés nyitási kérés
///   <br />
///   en: Order opening request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///   hu: Rendelés egyedi azonosítója
///   <br />
///   en: Unique identifier of the order
/// </param>
public sealed record TOrderOpenQaeRequest(string RequestId, string OrderId) : IQaeRequest;