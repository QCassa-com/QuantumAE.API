using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Mértékegység típusok (NAV UnitOfMeasureType alapján)
///   <br />
///   en: Unit of measure types (based on NAV UnitOfMeasureType)
/// </summary>
[PublicAPI]
public enum TUnitType
{
  /// <summary>
  ///   hu: Darab
  ///   <br />
  ///   en: Piece
  /// </summary>
  Piece = 0,

  /// <summary>
  ///   hu: Kilogramm
  ///   <br />
  ///   en: Kilogram
  /// </summary>
  Kilogram,

  /// <summary>
  ///   hu: Tonna
  ///   <br />
  ///   en: Ton
  /// </summary>
  Ton,

  /// <summary>
  ///   hu: Kilowattóra
  ///   <br />
  ///   en: Kilowatt hour
  /// </summary>
  KilowattHour,

  /// <summary>
  ///   hu: Nap
  ///   <br />
  ///   en: Day
  /// </summary>
  Day,

  /// <summary>
  ///   hu: Óra
  ///   <br />
  ///   en: Hour
  /// </summary>
  Hour,

  /// <summary>
  ///   hu: Perc
  ///   <br />
  ///   en: Minute
  /// </summary>
  Minute,

  /// <summary>
  ///   hu: Hónap
  ///   <br />
  ///   en: Month
  /// </summary>
  Month,

  /// <summary>
  ///   hu: Liter
  ///   <br />
  ///   en: Liter
  /// </summary>
  Liter,

  /// <summary>
  ///   hu: Kilométer
  ///   <br />
  ///   en: Kilometer
  /// </summary>
  Kilometer,

  /// <summary>
  ///   hu: Köbméter
  ///   <br />
  ///   en: Cubic meter
  /// </summary>
  CubicMeter,

  /// <summary>
  ///   hu: Négyzetméter
  ///   <br />
  ///   en: Square meter
  /// </summary>
  SquareMeter,

  /// <summary>
  ///   hu: Méter
  ///   <br />
  ///   en: Meter
  /// </summary>
  Meter,

  /// <summary>
  ///   hu: Folyóméter
  ///   <br />
  ///   en: Linear meter
  /// </summary>
  LinearMeter,

  /// <summary>
  ///   hu: Karton
  ///   <br />
  ///   en: Carton
  /// </summary>
  Carton,

  /// <summary>
  ///   hu: Csomag
  ///   <br />
  ///   en: Pack
  /// </summary>
  Pack,

  /// <summary>
  ///   hu: Egyéb (UnitName mezőben megadva)
  ///   <br />
  ///   en: Other (specified in UnitName field)
  /// </summary>
  Other
}

/// <summary>
///   hu: TUnitType konverziós kiterjesztések
///   <br />
///   en: TUnitType conversion extensions
/// </summary>
[PublicAPI]
public static class TUnitTypeExtensions
{
  /// <summary>
  ///   hu: Konvertálás NAV UnitOfMeasureType értékre
  ///   <br />
  ///   en: Convert to NAV UnitOfMeasureType value
  /// </summary>
  /// <param name="AUnitType">
  ///   hu: A konvertálandó mértékegység típus
  ///   <br />
  ///   en: The unit type to convert
  /// </param>
  /// <returns>
  ///   hu: NAV kompatibilis mértékegység string
  ///   <br />
  ///   en: NAV compatible unit of measure string
  /// </returns>
  public static string ToNavUnitOfMeasure(this TUnitType AUnitType) => AUnitType switch
  {
    TUnitType.Piece => "PIECE",
    TUnitType.Kilogram => "KILOGRAM",
    TUnitType.Ton => "TON",
    TUnitType.KilowattHour => "KWH",
    TUnitType.Day => "DAY",
    TUnitType.Hour => "HOUR",
    TUnitType.Minute => "MINUTE",
    TUnitType.Month => "MONTH",
    TUnitType.Liter => "LITER",
    TUnitType.Kilometer => "KILOMETER",
    TUnitType.CubicMeter => "CUBIC_METER",
    TUnitType.SquareMeter => "SQUARE_METER",
    TUnitType.Meter => "METER",
    TUnitType.LinearMeter => "LINEAR_METER",
    TUnitType.Carton => "CARTON",
    TUnitType.Pack => "PACK",
    TUnitType.Other => "OWN",
    _ => "PIECE"
  };
}
