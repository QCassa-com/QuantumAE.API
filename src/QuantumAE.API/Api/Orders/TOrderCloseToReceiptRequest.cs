using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Zárási kérés – Nyugta
///   <br />
///   en: Close request – Receipt
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
public sealed record TOrderCloseToReceiptRequest(
  string RequestId,
  int ResultCode,
  string OrderId,
  string DocumentId,
  TCloseMethod CloseMethod,
  TDocumentGeneral DocumentGeneral,
  TPay Pay,
  bool? Cut,
  int? Retraction,
  TReceiptType? ReceiptType
) : IQaeResponse;