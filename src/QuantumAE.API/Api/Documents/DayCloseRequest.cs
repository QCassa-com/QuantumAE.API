using JetBrains.Annotations;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Napzárás (NFN) kérés. Lezárja az adóügyi napot, Z-reportot generál,
///   és elküldi a napi forgalmi jelentést (DailyCashFlow) a NAV-nak.
///   Csak RequestId szükséges, a rendszer generálja az összes adatot.
///   <br />
///   en: Day close (NFN) request. Closes the taxation day, generates a Z-report,
///   and sends the daily cash flow report (DailyCashFlow) to NAV.
///   Only RequestId is required, the system generates all data.
/// </summary>
/// <param name="RequestId">
///   hu: Kérés azonosítója.
///   <br />
///   en: Request identifier.
/// </param>
[PublicAPI]
public sealed record DayCloseRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId
) : IDocumentRequest;

/// <summary>
///   hu: Napzárás válasz. Tartalmazza a Z-report adatait és az NFN küldés státuszát.
///   <br />
///   en: Day close response. Contains Z-report data and NFN submission status.
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
///   hu: Dokumentumszám NAV-I formátumban (pl. NZ-B12345678/00000001/0142).
///   <br />
///   en: Document number in NAV-I format (e.g., NZ-B12345678/00000001/0142).
/// </param>
/// <param name="ClosureNumber">
///   hu: Zárás sorszáma (ZSZ, 0001-9999).
///   <br />
///   en: Closure sequence number (ZSZ, 0001-9999).
/// </param>
/// <param name="TaxationDayNumber">
///   hu: Adóügyi nap sorszáma (DNO).
///   <br />
///   en: Taxation day number (DNO).
/// </param>
/// <param name="TotalDocumentCount">
///   hu: Összes bizonylat száma az adóügyi napon.
///   <br />
///   en: Total document count during the taxation day.
/// </param>
/// <param name="TotalGrossAmount">
///   hu: Összes bruttó összeg az adóügyi napon.
///   <br />
///   en: Total gross amount during the taxation day.
/// </param>
/// <param name="SentToNav">
///   hu: Jelzi, hogy az NFN jelentés elküldésre került-e a NAV felé.
///   <br />
///   en: Indicates whether the NFN report was sent to NAV.
/// </param>
/// <param name="ErrorMessage">
///   hu: Hibaüzenet, ha a művelet nem volt sikeres.
///   <br />
///   en: Error message if the operation was not successful.
/// </param>
[PublicAPI]
public sealed record DayCloseResponse(
  string RequestId,
  int ResultCode,
  string? DocNumber = null,
  string? ClosureNumber = null,
  long? TaxationDayNumber = null,
  int? TotalDocumentCount = null,
  int? TotalGrossAmount = null,
  bool SentToNav = false,
  string? ErrorMessage = null
) : IDocumentResponse;
