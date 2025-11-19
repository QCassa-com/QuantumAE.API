using JetBrains.Annotations;
using QuantumAE.Models;

namespace QuantumAE.Api
{
  /// <summary>
  ///  hu: Rendelés tétel validátor osztály
  ///  <br />
  ///  en: Order item validator class
  /// </summary>
  [PublicAPI]
  public class TOrderItemValidator
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
    ///  hu: 
    ///  <br />
    ///  en: True if the item is valid, otherwise false
    /// </returns>
    public TOrderItemResultCode Validate(TOrderItem AOrderItem)
    {
      if (string.IsNullOrWhiteSpace(AOrderItem.Name) && string.IsNullOrEmpty(AOrderItem.Barcode))
        return TOrderItemResultCode.NameAndBarCodeEmpty;
        
      return TOrderItemResultCode.Success;
    }
  }
}