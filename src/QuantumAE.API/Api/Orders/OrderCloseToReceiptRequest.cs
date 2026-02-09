using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés zárása nyugtaként
///   <br />
///   en: Closing order as receipt
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
/// <param name="DocumentId">
///   hu: Bizonylat azonosító
///   <br />
///   en: Document identifier
/// </param>
/// <param name="CloseMethod">
///   hu: Zárási módja
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
/// <param name="ReceiptType">
///   hu: Nyugta típusa (ha van)
///   <br />
///   en: Receipt type (if any)
/// </param>
[PublicAPI]
public sealed record OrderCloseToReceiptRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  [property: NotEmptyString]
  string OrderId,

  string? DocumentId,
  TCloseMethod? CloseMethod,
  TDocumentGeneral? DocumentGeneral,
  TPayment? Pay,
  bool? Cut,

  [property: Range(0, 100)]
  int? Retraction,

  TReceiptType? ReceiptType
) : IOrderRequest;


/// <summary>
///   hu: Rendelés zárása nyugtaként válasz
///   <br />
///   en: Closing order as receipt response
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
/// <param name="DocumentId">
///   hu: A generált bizonylat azonosítója (sikeres feldolgozás esetén)
///   <br />
///   en: Generated document ID (on successful processing)
/// </param>
/// <param name="SentToNav">
///   hu: Igaz, ha a bizonylat sikeresen el lett küldve a NAV-nak
///   <br />
///   en: True if document was successfully sent to NAV
/// </param>
/// <param name="SavedOffline">
///   hu: Igaz, ha a bizonylat offline mentésre került (NAV nem elérhető)
///   <br />
///   en: True if document was saved offline (NAV unreachable)
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet (hiba esetén)
///   <br />
///   en: Error message (on error)
/// </param>
[PublicAPI]
public sealed record OrderCloseToReceiptResponse(
  string RequestId,
  int ResultCode,
  string? DocumentId = null,
  bool SentToNav = false,
  bool SavedOffline = false,
  string? ErrorMessage = null
) : IOrderResponse;