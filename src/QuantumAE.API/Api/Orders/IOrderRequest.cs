using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés kérés alap interfész
///   <br />
///   en: Order request base interface
/// </summary>
[PublicAPI]
public interface IOrderRequest: IQaeRequest
{
}

/// <summary>
///   hu: Rendelés válasz alap interfész
///   <br />
///   en: Order response base interface 
/// </summary>
[PublicAPI]
public interface IOrderResponse : IQaeResponse
{
}