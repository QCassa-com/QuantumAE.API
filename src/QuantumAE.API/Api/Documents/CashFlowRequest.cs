using JetBrains.Annotations;
using QuantumAE.Api.Payments;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Pénzmozgás bizonylat (PMN) kérés. Pénzbefizetés, pénzkivét és fizetőeszköz csere
///   egyaránt ezt a kérést használja. A CashFlowTypeId határozza meg a jogcímet
///   (a CashFlowTypes törzsadat tábla Id mezőjére hivatkozik).
///   <br />
///   en: Cash flow document (PMN) request. Used for cash-in, cash-out and instrument exchange.
///   The CashFlowTypeId determines the payment title
///   (references the Id field of the CashFlowTypes master data table).
/// </summary>
/// <param name="RequestId">
///   hu: Kérés azonosítója.
///   <br />
///   en: Request identifier.
/// </param>
/// <param name="CashFlowTypeId">
///   hu: Pénzmozgás jogcím típus azonosítója (a CashFlowTypes tábla Id mezője).
///   <br />
///   en: Cash flow type identifier (references CashFlowTypes table Id field).
/// </param>
/// <param name="Instruments">
///   hu: Fizetőeszközök listája.
///   <br />
///   en: List of payment instruments.
/// </param>
/// <param name="ChangeAmount">
///   hu: Visszajáró összege egész forintban (opcionális).
///   <br />
///   en: Change amount in whole HUF (optional).
/// </param>
/// <param name="RoundingDifference">
///   hu: Kerekítési különbözet összege forintban (opcionális).
///   <br />
///   en: Rounding difference amount in HUF (optional).
/// </param>
[PublicAPI]
public sealed record CashFlowRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  [property: Required]
  int CashFlowTypeId,

  [property: Required]
  List<TInstrumentItem> Instruments,

  long? ChangeAmount = null,

  int? RoundingDifference = null
) : IDocumentRequest;

/// <summary>
///   hu: Pénzmozgás bizonylat válasz.
///   <br />
///   en: Cash flow document response.
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
///   hu: Dokumentumszám NAV-I formátumban (pl. PM-B12345678/00000257/0142/00001).
///   <br />
///   en: Document number in NAV-I format (e.g., PM-B12345678/00000257/0142/00001).
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
public sealed record CashFlowResponse(
  string RequestId,
  int ResultCode,
  string? DocNumber = null,
  bool SentToNav = false,
  string? ErrorMessage = null
) : IDocumentResponse;
