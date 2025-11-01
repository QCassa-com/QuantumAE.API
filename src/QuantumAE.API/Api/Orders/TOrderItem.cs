using JetBrains.Annotations;

namespace QuantumAE.Api.Orders;

/// <summary>
///   hu: Rendelés tétel adatai
///   <br />
///   en: Order item data
/// </summary>
/// <param name="Name">
///   hu: Tétel megnevezése
///   <br />
///   en: Item name
/// </param>
/// <param name="ArticleNo">
///   hu: Tétel cikkszáma
///   <br />
///   en: Item article number
/// </param>
/// <param name="Unit">
///   hu: Tétel mértékegysége
///   <br />
///   en: Item unit of measure
/// </param>
/// <param name="UnitPrice">
///   hu: Tétel egységára
///   <br />
///   en: Item unit price
/// </param>
/// <param name="Quantity">
///   hu: Tétel mennyisége
///   <br />
///   en: Item quantity
/// </param>
/// <param name="Cat">
///   hu: Tétel kategóriája
///   <br />
///   en: Item category
/// </param>
/// <param name="Dept">
///   hu: Tétel osztálya
///   <br />
///   en: Item department
/// </param>
[PublicAPI]
public sealed record TOrderItem(
  string Name,
  string ArticleNo,
  string Unit,
  decimal UnitPrice,
  decimal Quantity,
  string Cat,
  string Dept);