using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Zárási kérés – Göngyöleg
///   <br />
///   en: Close request – Empties
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
public sealed record TOrderCloseToEmptiesRequest(
  string RequestId,
  int ResultCode,
  string OrderId,
  string DocumentId,
  TCloseMethod CloseMethod,
  TDocumentGeneral DocumentGeneral,
  TPay Pay,
  bool Cut,
  int Retraction
) : IQaeResponse;