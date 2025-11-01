using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Fizetési adatok
///   <br />
///   en: Payment information
/// </summary>
/// <param name="Amount">
///   hu: Fizetett összeg
///   <br />
///   en: Paid amount
/// </param>
/// <param name="Method">
///   hu: Fizetési mód
///   <br />
///   en: Payment method
/// </param>
[PublicAPI]
public sealed record TPay(decimal Amount, string Method);