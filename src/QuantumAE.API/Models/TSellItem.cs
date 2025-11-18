using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Tétel
///   <br />
///   en: Item
/// </summary>
/// <param name="LineNo">
///   hu: Sorszám
///   <br />
///   en: Line number
/// </param>
/// <param name="Name">
///   hu: Megnevezés
///   <br />
///   en: Name
/// </param>
/// <param name="Qty">
///   hu: Mennyiség
///   <br />
///   en: Quantity
/// </param>
/// <param name="Unit">
///   hu: Mértékegység
///   <br />
///   en: Unit of measure
/// </param>
/// <param name="UnitPrice">
///   hu: Egységár
///   <br />
///   en: Unit price
/// </param>
/// <param name="LineTotal">
///   hu: Sorösszeg
///   <br />
///   en: Line total
/// </param>
[PublicAPI]
public sealed record TSellItem(int LineNo, string Name, decimal Qty, string Unit, decimal UnitPrice, decimal LineTotal);