using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Nemfiskális sorok
///   <br />
///   en: Non-fiscal rows
/// </summary>
/// <param name="Rows">
///   hu: Nemfiskális (információs) sorok listája
///   <br />
///   en: List of non-fiscal (informational) rows
/// </param>
[PublicAPI]
public sealed record TNonFiscalRows(IReadOnlyList<string> Rows);