using JetBrains.Annotations;

namespace QuantumAE.Api.Payments;

/// <summary>
///   hu: Fizetőeszköz tétel. Az Amount mindig az alapértelmezett pénznemben (jelenleg HUF) értendő.
///   Ha CurrencyCode megadva, akkor deviza fizetés: az ExchangeRate és CurrencyAmount is kötelező.
///   Ha CurrencyCode null, az alapértelmezett pénznemet jelenti (árfolyam = 1).
///   <br />
///   en: Payment instrument item. Amount is always in the default currency (currently HUF).
///   If CurrencyCode is set, it's a foreign currency payment: ExchangeRate and CurrencyAmount are required.
///   If CurrencyCode is null, it means the default currency (exchange rate = 1).
/// </summary>
[PublicAPI]
public sealed record TInstrumentItem(
  int PaymentTypeId,
  int Amount,
  string? CurrencyCode = null,
  decimal? ExchangeRate = null,
  decimal? CurrencyAmount = null
);
