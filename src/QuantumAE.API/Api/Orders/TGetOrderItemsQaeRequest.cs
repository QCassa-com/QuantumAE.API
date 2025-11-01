using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: GetItems kérés
///   <br />
///   en: GetItems request
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///   hu: A rendelés egyedi azonosítója
///   <br />
///   en: Unique identifier of the order
/// </param>
[PublicAPI]
public sealed record TGetOrderItemsQaeRequest(string RequestId, int OrderId) : IQaeRequest;