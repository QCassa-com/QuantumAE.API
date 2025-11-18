namespace QuantumAE.Models;

public class TOrder
{
  /// <summary>
  ///   hu: Rendelés nyitásának időpontja
  ///   <br />
  ///   en: Order creation time
  /// </summary>
  public DateTime CreatedAt { get; set; }
  
  /// <summary>
  ///   hu: Rendelés zárásának időpontja
  ///   <br />
  ///   en: Order closing time
  /// </summary>
  public DateTime? ClosedAt { get; set; }
  
  /// <summary>
  ///  hu: Tételek
  ///  <br />
  ///  en: Items
  /// </summary>
  public List<TOrderItem> Items { get; set; } = new List<TOrderItem>();
}

/// <summary>
///   hu: Rendelés tétele
///   <br />
///   en: Order item
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
/// <param name="Quantity">
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
public sealed record TOrderItem(string Name, decimal Quantity, string Unit, decimal UnitPrice, decimal LineTotal);