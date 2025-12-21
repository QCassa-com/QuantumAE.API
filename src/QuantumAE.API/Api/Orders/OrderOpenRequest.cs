using JetBrains.Annotations;
using QuantumAE.Validation;

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
[PublicAPI]
public sealed record OrderOpenRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,
  [property: Required]
  [property: NotEmptyString]
  string OrderId
) : IOrderRequest;

/// <summary>
///   hu: Rendelés nyitási válasz
///   <br />
///   en: Order opening response
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
[PublicAPI]
public sealed record OrderOpenResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : IOrderResponse;