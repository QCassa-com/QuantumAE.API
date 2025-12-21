using JetBrains.Annotations;
using QuantumAE.Validation;

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
  [Required]
  [NotEmptyString]
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
[PublicAPI]
public class TOrderItem
{
  /// <summary>
  ///   hu: Tétel sorszáma (1-től induló, automatikusan generált ha nincs megadva)
  ///   <br />
  ///   en: Line number (starting from 1, auto-generated if not specified)
  /// </summary>
  public int? LineNumber { get; set; }

  /// <summary>
  ///   hu: Tétel jellege (n=értékesítés, ns=sztornó, e=engedmény, stb.)
  ///   <br />
  ///   en: Item nature (n=sale, ns=storno, e=discount, etc.)
  /// </summary>
  [StringLength(3)]
  public string ItemNature { get; set; } = TItemNature.Default;

  /// <summary>
  ///   hu: Megnevezés (kötelező items/add esetén)
  ///   <br />
  ///   en: Name (required for items/add)
  /// </summary>
  [StringLength(512)]
  public string? Name { get; set; }

  /// <summary>
  ///   hu: Vonalkód (EAN/GTIN)
  ///   <br />
  ///   en: Barcode (EAN/GTIN)
  /// </summary>
  [StringLength(50)]
  public string? Barcode { get; set; }

  /// <summary>
  ///   hu: Mennyiség (alapértelmezett: 1)
  ///   <br />
  ///   en: Quantity (default: 1)
  /// </summary>
  [PositiveDecimal]
  public decimal Quantity { get; set; } = 1;

  /// <summary>
  ///   hu: Mértékegység típusa (alapértelmezett: darab)
  ///   <br />
  ///   en: Unit of measure type (default: piece)
  /// </summary>
  public TUnitType UnitType { get; set; } = TUnitType.Piece;

  /// <summary>
  ///   hu: Egyedi mértékegység neve (csak ha UnitType = Other)
  ///   <br />
  ///   en: Custom unit name (only if UnitType = Other)
  /// </summary>
  [StringLength(50)]
  public string? UnitName { get; set; }

  /// <summary>
  ///   hu: Egységár
  ///   <br />
  ///   en: Unit price
  /// </summary>
  [PositiveDecimal(AAllowZero: true)]
  public decimal? UnitPrice { get; set; }

  /// <summary>
  ///   hu: Sorösszeg (kötelező items/add esetén)
  ///   <br />
  ///   en: Line total (required for items/add)
  /// </summary>
  [PositiveDecimal(AAllowZero: true)]
  public decimal? Total { get; set; }

  /// <summary>
  ///   hu: ÁFA kód (A=5%, B=18%, C=27%, D=0%, stb.) - kötelező items/add esetén
  ///   <br />
  ///   en: VAT code (A=5%, B=18%, C=27%, D=0%, etc.) - required for items/add
  /// </summary>
  [StringLength(10)]
  public string? VatCode { get; set; }
}

/// <summary>
///   hu: Tétel módosítás adatai (minden mező opcionális)
///   <br />
///   en: Item update data (all fields optional)
/// </summary>
[PublicAPI]
public class TOrderItemUpdate
{
  /// <summary>
  ///   hu: Megnevezés
  ///   <br />
  ///   en: Name
  /// </summary>
  [StringLength(512)]
  public string? Name { get; set; }

  /// <summary>
  ///   hu: Vonalkód (EAN/GTIN)
  ///   <br />
  ///   en: Barcode (EAN/GTIN)
  /// </summary>
  [StringLength(50)]
  public string? Barcode { get; set; }

  /// <summary>
  ///   hu: Mennyiség
  ///   <br />
  ///   en: Quantity
  /// </summary>
  [PositiveDecimal]
  public decimal? Quantity { get; set; }

  /// <summary>
  ///   hu: Mértékegység típusa
  ///   <br />
  ///   en: Unit of measure type
  /// </summary>
  public TUnitType? UnitType { get; set; }

  /// <summary>
  ///   hu: Egyedi mértékegység neve (csak ha UnitType = Other)
  ///   <br />
  ///   en: Custom unit name (only if UnitType = Other)
  /// </summary>
  [StringLength(50)]
  public string? UnitName { get; set; }

  /// <summary>
  ///   hu: Egységár
  ///   <br />
  ///   en: Unit price
  /// </summary>
  [PositiveDecimal(AAllowZero: true)]
  public decimal? UnitPrice { get; set; }

  /// <summary>
  ///   hu: Sorösszeg
  ///   <br />
  ///   en: Line total
  /// </summary>
  [PositiveDecimal(AAllowZero: true)]
  public decimal? Total { get; set; }

  /// <summary>
  ///   hu: ÁFA kód (A=5%, B=18%, C=27%, D=0%, stb.)
  ///   <br />
  ///   en: VAT code (A=5%, B=18%, C=27%, D=0%, etc.)
  /// </summary>
  [StringLength(10)]
  public string? VatCode { get; set; }
}
