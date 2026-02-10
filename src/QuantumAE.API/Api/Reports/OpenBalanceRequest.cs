using JetBrains.Annotations;
using QuantumAE.Api.Payments;
using QuantumAE.Validation;

namespace QuantumAE.Api.Reports;

/// <summary>
///   hu: Napnyitás (pénztárnyitás) kérés. Az adóügyi nap elején küldi a pénztárgép a NAV felé.
///   Az Instruments lista tartalmazza a nyitó egyenleget fizetőeszközönként.
///   <br />
///   en: Cash register open balance request. Sent at the beginning of each taxation day.
///   The Instruments list contains the opening balance per payment instrument.
/// </summary>
[PublicAPI]
public sealed record OpenBalanceRequest(
  [property: Required]
  [property: NotEmptyString]
  string RequestId,

  List<TInstrumentItem>? Instruments
) : IReportRequest;

/// <summary>
///   hu: Napnyitás válasz. Tartalmazza az adóügyi nap sorszámát és a bizonylatszámot.
///   <br />
///   en: Open balance response. Contains the taxation day number and the report number.
/// </summary>
[PublicAPI]
public sealed record OpenBalanceResponse(
  string RequestId,
  int ResultCode,
  long? TaxationDayNumber = null,
  string? ReportNumber = null,
  bool SentToNav = false,
  string? ErrorMessage = null
) : IReportResponse;
