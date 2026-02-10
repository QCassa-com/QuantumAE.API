using JetBrains.Annotations;

namespace QuantumAE.Api.Payments;

/// <summary>
///   hu: Fizetőeszköz típus törzsadat. Az első néhány rekord fix (rendszer szintű),
///   az üzlet további típusokat adhat hozzá (pl. "A utalvány", "B utalvány"),
///   amelyek mind egy NAV TPaymentCode-ra képeződnek.
///   <br />
///   en: Payment type master data. The first few records are fixed (system level),
///   shops can add additional types (e.g. "Voucher A", "Voucher B"),
///   each mapped to a NAV TPaymentCode.
/// </summary>
[PublicAPI]
public sealed record TPaymentType(
  int Id,
  string Name,
  TPaymentCode PaymentCode,
  bool Enabled
);
