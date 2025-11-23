using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api.Orders
{
  /// <summary>
  ///  hu: Rendelés tétel validátor osztály
  ///  <br />
  ///  en: Order item validator class
  /// </summary>
  [PublicAPI]
  public static class TOrderValidator
  {
    /// <summary>
    ///  hu: Rendelés tétel validálása
    ///  <br />
    ///  en: Validate order item
    /// </summary>
    /// <param name="AOrderItem">
    ///  hu: Rendelés tétel
    ///  <br />
    ///  en: Order item
    /// </param>
    /// <returns>
    ///  hu: Eredménykód
    ///  <br />
    ///  en: 
    /// </returns>
    public static TOrderItemResult Validate(this TOrderItem AOrderItem)
    {
      if (string.IsNullOrWhiteSpace(AOrderItem.Name) && string.IsNullOrEmpty(AOrderItem.Barcode))
        return TOrderItemResult.NameAndBarCodeEmpty;
        
      return TOrderItemResult.Success;
    }
  }
}