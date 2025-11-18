using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés zárása sztornó bizonylatként
///   <br />
///   en: Closing an order as a storno document
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
/// <param name="Pay">
///   hu: Fizetés adatai
///   <br />
///   en: Payment info
/// </param>
/// <param name="Customer">
///   hu: Vevő adatai
///   <br />
///   en: Customer data
/// </param>
/// <param name="StornoInfo">
///   hu: Sztornó adatai
///   <br />
///   en: Storno information
/// </param>
/// <param name="Cut">
///   hu: Vágás jelzése
///   <br />
///   en: Indication of cutting
/// </param>
/// <param name="Retraction">
///   hu: Visszahúzás sorok száma
///   <br />
///   en: Number of retraction lines
/// </param>
[PublicAPI]
public sealed record OrderCloseToStornoRequest(
  string RequestId,
  int ResultCode,
  string OrderId,
  string DocumentId,
  TCloseMethod CloseMethod,
  TDocumentGeneral DocumentGeneral,
  TPay Pay,
  TCustomer Customer,
  TStornoInfo StornoInfo,
  bool Cut,
  int Retraction
) : IOrderRequest;


/// <summary>
///   hu: Rendelés zárása sztornó bizonylatként válasz
///   <br />
///   en: Closing an order as a storno document response
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
[PublicAPI]
public sealed record OrderCloseToStornoResponse(
  string RequestId,
  int ResultCode
) : IOrderResponse;