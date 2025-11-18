using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés zárása összesítő bizonylatként
///   <br />
///   en: Closing an order as a summary document
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
/// <param name="CloseMethod">
///   hu: Zárás módja
///   <br />
///   en: Close method
/// </param>
/// <param name="DocumentGeneral">
///   hu: Dokumentum általános adatai
///   <br />
///   en: Document general info
/// </param>
/// <param name="NonFiscalRows">
///   hu: Nemfiskális (információs) sorok
///   <br />
///   en: Non-fiscal (informational) rows
/// </param>
[PublicAPI]
public sealed record OrderCloseToSummaryRequest(
  string RequestId,
  int ResultCode,
  string OrderId,
  string DocumentId,
  TCloseMethod CloseMethod,
  TDocumentGeneral DocumentGeneral,
  TNonFiscalRows NonFiscalRows
) : IOrderRequest;

/// <summary>
///   hu: Rendelés zárása összesítő bizonylatként válasz
///   <br />
///   en: Closing an order as a summary document response
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
public sealed record OrderCloseToSummaryResponse(
  string RequestId,
  int ResultCode
) : IOrderResponse;