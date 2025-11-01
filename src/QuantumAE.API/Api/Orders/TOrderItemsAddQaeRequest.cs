using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Tétel hozzáadási kérés
///   <br />
///   en: Item addition request
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
/// <param name="Item">
///   hu: Hozzáadandó tétel adatai
///   <br />
///   en: Data of the item to be added
/// </param>
[PublicAPI]
public sealed record TOrderItemsAddQaeRequest(string RequestId, string OrderId, TOrderItem Item) : IQaeRequest;