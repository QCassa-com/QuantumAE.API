
using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;


/// <summary>
///   hu: Rendelés zárása egészségségkártya bizonylatként.
///   <br />
///   en: Order close as health card receipt.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés egyedi azonosítója
///   <br />
///   en: Unique identifier of the request
/// </param>
/// <param name="OrderId">
///   hu: A lezárandó rendelés egyedi azonosítója az Adóügyi Egységben
///   <br />
///   en: Unique identifier of the order to be closed in the Tax Unit
/// </param>
[PublicAPI]
public record OrderCloseToHealthCardRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string OrderId
) : IOrderRequest;


/// <summary>
///   hu: Rendelés zárása egészségségkártya bizonylatként válasz.
///   <br />
///   en: Order close as health card receipt response.
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
///   hu: Hibaüzenet (ha hiba történt)
///   <br />
///   en: Error message (if error occurred)
/// </param>
[PublicAPI]
public record OrderCloseToHealthCardResponse(string RequestId, int ResultCode, string? ErrorMessage = null) : IOrderResponse;