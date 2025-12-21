using JetBrains.Annotations;

namespace QuantumAE.Models;

/// <summary>
///   hu: Tétel jelleg típusok (NAV ItemNatureType alapján)
///   <br />
///   en: Item nature types (based on NAV ItemNatureType)
/// </summary>
[PublicAPI]
public static class TItemNature
{
  /// <summary>
  ///   hu: Értékesítés
  ///   <br />
  ///   en: Sale
  /// </summary>
  public const string Sale = "n";

  /// <summary>
  ///   hu: Sztornózott értékesítés
  ///   <br />
  ///   en: Cancelled sale
  /// </summary>
  public const string SaleStorno = "ns";

  /// <summary>
  ///   hu: Engedmény
  ///   <br />
  ///   en: Discount
  /// </summary>
  public const string Discount = "e";

  /// <summary>
  ///   hu: Sztornózott engedmény
  ///   <br />
  ///   en: Cancelled discount
  /// </summary>
  public const string DiscountStorno = "es";

  /// <summary>
  ///   hu: Kedvezmény
  ///   <br />
  ///   en: Allowance
  /// </summary>
  public const string Allowance = "k";

  /// <summary>
  ///   hu: Sztornózott kedvezmény
  ///   <br />
  ///   en: Cancelled allowance
  /// </summary>
  public const string AllowanceStorno = "ks";

  /// <summary>
  ///   hu: Felár
  ///   <br />
  ///   en: Surcharge
  /// </summary>
  public const string Surcharge = "f";

  /// <summary>
  ///   hu: Sztornózott felár
  ///   <br />
  ///   en: Cancelled surcharge
  /// </summary>
  public const string SurchargeStorno = "fs";

  /// <summary>
  ///   hu: Göngyöleg kiadás
  ///   <br />
  ///   en: Packaging out
  /// </summary>
  public const string PackagingOut = "g";

  /// <summary>
  ///   hu: Göngyöleg visszavétel
  ///   <br />
  ///   en: Packaging return
  /// </summary>
  public const string PackagingReturn = "gs";

  /// <summary>
  ///   hu: Visszáru
  ///   <br />
  ///   en: Return
  /// </summary>
  public const string Return = "v";

  /// <summary>
  ///   hu: Sztornózott visszáru
  ///   <br />
  ///   en: Cancelled return
  /// </summary>
  public const string ReturnStorno = "vs";

  /// <summary>
  ///   hu: Díjbeszedés
  ///   <br />
  ///   en: Fee collection
  /// </summary>
  public const string FeeCollection = "x";

  /// <summary>
  ///   hu: Díjbeszedés visszafizetés
  ///   <br />
  ///   en: Fee refund
  /// </summary>
  public const string FeeRefund = "xs";

  /// <summary>
  ///   hu: Letét
  ///   <br />
  ///   en: Deposit
  /// </summary>
  public const string Deposit = "p";

  /// <summary>
  ///   hu: Alapértelmezett tétel jelleg (értékesítés)
  ///   <br />
  ///   en: Default item nature (sale)
  /// </summary>
  public const string Default = Sale;

  /// <summary>
  ///   hu: Sztornó jelleg hozzáadása az eredeti jelleghez
  ///   <br />
  ///   en: Add storno suffix to the original nature
  /// </summary>
  /// <param name="ANature">
  ///   hu: Az eredeti tétel jelleg
  ///   <br />
  ///   en: The original item nature
  /// </param>
  /// <returns>
  ///   hu: A sztornózott tétel jelleg
  ///   <br />
  ///   en: The stornoed item nature
  /// </returns>
  public static string ToStorno(string ANature)
  {
    if (string.IsNullOrEmpty(ANature))
      return SaleStorno;

    // Ha már sztornó, akkor visszaadjuk
    if (ANature.EndsWith("s"))
      return ANature;

    return ANature + "s";
  }

  /// <summary>
  ///   hu: Ellenőrzi, hogy a tétel jelleg sztornó-e
  ///   <br />
  ///   en: Checks if the item nature is a storno
  /// </summary>
  /// <param name="ANature">
  ///   hu: A tétel jelleg
  ///   <br />
  ///   en: The item nature
  /// </param>
  /// <returns>
  ///   hu: Igaz, ha sztornó jellegű
  ///   <br />
  ///   en: True if it's a storno nature
  /// </returns>
  public static bool IsStorno(string? ANature) =>
    !string.IsNullOrEmpty(ANature) && ANature.EndsWith("s");
}
