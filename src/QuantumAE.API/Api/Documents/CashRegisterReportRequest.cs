using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Pénztárjelentés (PJN) kérés. Bármikor lekérdezhető összesítő bizonylat
///   az adóügyi nap folyamán. A rendszer generálja az összesítő adatokat,
///   csak RequestId szükséges.
///   <br />
///   en: Cash register report (PJN) request. Summary report that can be generated
///   at any time during the taxation day. The system generates all summary data,
///   only RequestId is required.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés azonosítója.
///   <br />
///   en: Request identifier.
/// </param>
[PublicAPI]
public sealed record CashRegisterReportRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : IDocumentRequest;

/// <summary>
///   hu: Pénztárjelentés válasz. Tartalmazza a nap összesítő adatait.
///   <br />
///   en: Cash register report response. Contains summary data for the day.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés azonosítója, megegyezik a kérésben szereplővel.
///   <br />
///   en: Request identifier, matches the one in the request.
/// </param>
/// <param name="ResultCode">
///   hu: Eredménykód (0 = siker, egyéb = hiba).
///   <br />
///   en: Result code (0 = success, non-zero = error).
/// </param>
/// <param name="DocNumber">
///   hu: Dokumentumszám NAV-I formátumban (pl. PJ-B12345678/00000258/0142/00001).
///   <br />
///   en: Document number in NAV-I format (e.g., PJ-B12345678/00000258/0142/00001).
/// </param>
/// <param name="DailyRevenueReceipt">
///   hu: Napi nyugtás bevétel összege.
///   <br />
///   en: Daily receipt revenue amount.
/// </param>
/// <param name="DailyRevenueInvoice">
///   hu: Napi számlás bevétel összege.
///   <br />
///   en: Daily invoice revenue amount.
/// </param>
/// <param name="DailyRevenueSum">
///   hu: Napi bevétel összesen.
///   <br />
///   en: Daily revenue total.
/// </param>
/// <param name="BalanceAmountWithRounding">
///   hu: Kerekített fiókegyenleg összesen.
///   <br />
///   en: Balance amount with rounding.
/// </param>
/// <param name="SentToNav">
///   hu: Jelzi, hogy a jelentés elküldésre került-e a NAV felé.
///   <br />
///   en: Indicates whether the report was sent to NAV.
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet, ha a művelet nem volt sikeres.
///   <br />
///   en: Error message if the operation was not successful.
/// </param>
[PublicAPI]
public sealed record CashRegisterReportResponse(
  string RequestId,
  int ResultCode,
  string? DocNumber = null,
  int? DailyRevenueReceipt = null,
  int? DailyRevenueInvoice = null,
  int? DailyRevenueSum = null,
  int? BalanceAmountWithRounding = null,
  bool SentToNav = false,
  string? ErrorMessage = null
) : IDocumentResponse;
