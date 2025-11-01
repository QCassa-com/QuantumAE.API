using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Üzemanyagkártya zárási adatok
///   <br />
///   en: Fuel card close information
/// </summary>
/// <param name="CardNumber">
///   hu: Kártyaszám
///   <br />
///   en: Card number
/// </param>
/// <param name="AuthCode">
///   hu: Engedélyezési kód (ha van)
///   <br />
///   en: Authorization code (if any)
/// </param>
[PublicAPI]
public sealed record TFuelCardClose(string CardNumber, string? AuthCode);