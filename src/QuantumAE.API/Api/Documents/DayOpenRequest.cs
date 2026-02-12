using JetBrains.Annotations;
using QuantumAE.Api.Payments;
using QuantumAE.Validation;

namespace QuantumAE.Api.Documents;

/// <summary>
///   hu: Napnyitás (pénztárnyitás) kérés. Az adóügyi nap elején küldi a pénztárgép a NAV felé.
///   Az Instruments lista tartalmazza a nyitó egyenleget fizetőeszközönként.
///   <br />
///   en: Day open request. Sent at the beginning of each taxation day.
///   The Instruments list contains the opening balance per payment instrument.
/// </summary>
[PublicAPI]
public sealed record DayOpenRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  List<TInstrumentItem>? Instruments
) : IDocumentRequest;

/// <summary>
///   hu: Napnyitás válasz. Tartalmazza az adóügyi nap sorszámát és a dokumentumszámot
///   NAV-I formátumban (pl. NN-B12345678/00000256/0142).
///   <br />
///   en: Day open response. Contains the taxation day number and the document number
///   in NAV-I format (e.g., NN-B12345678/00000256/0142).
/// </summary>
[PublicAPI]
public sealed record DayOpenResponse(
  string RequestId,
  int ResultCode,
  long? TaxationDayNumber = null,
  string? DocNumber = null,
  bool SentToNav = false,
  string? ErrorMessage = null
) : IDocumentResponse;
