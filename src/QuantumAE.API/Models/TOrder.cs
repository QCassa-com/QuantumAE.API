using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Rendelés
///   <br />
///   en: Order
/// </summary>
[PublicAPI]
public class TOrder
{
  /// <summary>
  ///   hu: Rendelés egyedi azonosítója
  ///   <br />
  ///   en: Unique identifier of the order
  /// </summary>
  public string Id { get; set; } = string.Empty;
  
  /// <summary>
  ///   hu: Rendelés nyitásának időpontja
  ///   <br />
  ///   en: Order creation time
  /// </summary>
  public DateTime CreatedAtUtc { get; set; }
  
  /// <summary>
  ///   hu: Rendelés zárásának időpontja
  ///   <br />
  ///   en: Order closing time
  /// </summary>
  public DateTime? ClosedAtUtc { get; set; }
  
  /// <summary>
  ///  hu: Tételek
  ///  <br />
  ///  en: Items
  /// </summary>
  public List<TOrderItem> Items { get; set; } = new ();
}

/// <summary>
///   hu: Rendelés tétele
///   <br />
///   en: Order item
/// </summary>
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
/// <param name="Total">
///   hu: Sorösszeg
///   <br />
///   en: Line total
/// </param>
[PublicAPI]
public class TOrderItem
{
  /// <summary>
  ///   hu: Megnevezés
  ///   <br />
  ///   en: Name
  /// </summary>
  public string? Name { get; set; }
  
  /// <summary>
  ///   hu: Vonalkód
  ///   <br />
  ///   en: Barcode
  /// </summary>
  public string? Barcode { get; set; }
  
  /// <summary>
  ///   hu: Mennyiség
  ///   <br />
  ///   en: Quantity
  /// </summary>
  public decimal? Quantity { get; set; }
  
  /// <summary>
  ///   hu: Mértékegység
  ///   <br />
  ///   en: Unit of measure
  /// </summary>
  public string? Unit { get; set; }
  
  /// <summary>
  ///   hu: Egységár
  ///   <br />
  ///   en: Unit price
  /// </summary>
  public decimal? UnitPrice { get; set; }
  
  /// <summary>
  ///   hu: Sorösszeg
  ///   <br />
  ///   en: Line total
  /// </summary>
  public decimal? Total { get; set; }
}