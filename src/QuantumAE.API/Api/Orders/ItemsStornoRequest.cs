using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Tétel sztornózása a rendelésben
///   <br />
///   en: Storno an item in the order
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
/// <param name="LineNumber">
///   hu: Sztornózandó tétel sorszáma
///   <br />
///   en: Line number of the item to storno
/// </param>
/// <param name="Reason">
///   hu: Sztornó indoka (opcionális)
///   <br />
///   en: Reason for storno (optional)
/// </param>
[PublicAPI]
public sealed record ItemsStornoRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string OrderId,

  [property: Required]
  [property: Range(1, int.MaxValue)]
  int LineNumber,

  [property: StringLength(200)]
  string? Reason = null
) : IOrderRequest;

/// <summary>
///   hu: Tétel sztornó válasz
///   <br />
///   en: Item storno response
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
/// <param name="StornoLineNumber">
///   hu: A létrejött sztornó tétel sorszáma
///   <br />
///   en: Line number of the created storno item
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (opcionális)
///   <br />
///   en: Error message (optional)
/// </param>
[PublicAPI]
public sealed record ItemsStornoResponse(
  string RequestId,
  int ResultCode,
  int? StornoLineNumber = null,
  string? ErrorMessage = null
) : IOrderResponse;
