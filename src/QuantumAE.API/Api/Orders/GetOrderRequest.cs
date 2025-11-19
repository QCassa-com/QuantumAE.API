using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders
{
  /// <summary>
  ///   hu: Rendelés adatinak lekérdezése
  ///   <br />
  ///   en: Get order request
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
  public sealed record GetOrderRequest(string RequestId, string OrderId) : IOrderRequest;

  /// <summary>
  ///   hu: Rendelés adatainak lekérdezésére adott válasz
  ///   <br />
  ///   en: Response to get order request
  /// </summary>
  /// <param name="RequestId">
  ///   hu: Kérés egyedi azonosítója
  ///   <br />
  ///   en: Unique identifier of the request
  /// </param>
  /// <param name="ResultCode">
  ///   hu: Eredménykód (0 = siker), különben hiba kód
  ///   <br />
  ///   en: Result code (0 = success), otherwise error code
  /// </param>
  /// <param name="Order">
  ///   hu: Rendelés adatai (siker esetén)
  ///   <br />
  ///   en: Order data (in case of success)
  /// </param>
  [PublicAPI]
  public sealed record GetOrderResponse(string RequestId, int ResultCode, TOrder? Order = null) : IOrderResponse;
}