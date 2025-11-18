using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Vevő adatai
///   <br />
///   en: Customer information
/// </summary>
/// <param name="Name">
///   hu: Vevő neve
///   <br />
///   en: Customer name
/// </param>
/// <param name="TaxNumber">
///   hu: Vevő adószáma
///   <br />
///   en: Customer tax number
/// </param>
/// <param name="Address">
///   hu: Vevő címe
///   <br />
///   en: Customer address
/// </param>
[PublicAPI]
public sealed record TCustomer(string Name, string TaxNumber, string Address);