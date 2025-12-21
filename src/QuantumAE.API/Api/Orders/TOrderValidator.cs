
using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;

namespace QuantumAE.Api.Orders;

/// <summary>
///  hu: Rendelés tétel validátor osztály
///  <br />
///  en: Order item validator class
/// </summary>
[PublicAPI]
public static class TOrderValidator
{
  /// <summary>
  ///  hu: Rendelés tétel validálása attribútumok alapján
  ///  <br />
  ///  en: Validate order item based on attributes
  /// </summary>
  /// <param name="AOrderItem">
  ///  hu: Rendelés tétel
  ///  <br />
  ///  en: Order item
  /// </param>
  /// <returns>
  ///  hu: Eredménykód (TOrderItemResult)
  ///  <br />
  ///  en: Result code (TOrderItemResult)
  /// </returns>
  public static TOrderItemResult Validate(this TOrderItem AOrderItem)
  {
    if (!TValidationHelper.TryValidate(AOrderItem, out var results))
    {
      // Konvertáljuk a validációs hibákat TOrderItemResult-ra
      foreach (var result in results)
      {
        // Name/Barcode hiba
        if (result.MemberNames.Any(AName => AName is "Name" or "Barcode") &&
            result.ErrorMessage?.Contains("must be provided") == true)
        {
          return TOrderItemResult.NameAndBarCodeEmpty;
        }

        // Quantity hiba
        if (result.MemberNames.Any(AName => AName == "Quantity"))
        {
          return TOrderItemResult.InvalidQuantity;
        }

        // Price hiba
        if (result.MemberNames.Any(AName => AName is "UnitPrice" or "Total"))
        {
          return TOrderItemResult.InvalidPrice;
        }
      }

      // Általános hiba
      return TOrderItemResult.NameAndBarCodeEmpty;
    }

    return TOrderItemResult.Success;
  }

  /// <summary>
  ///  hu: Rendelés tétel validálása részletes hibaüzenetekkel
  ///  <br />
  ///  en: Validate order item with detailed error messages
  /// </summary>
  /// <param name="AOrderItem">
  ///  hu: Rendelés tétel
  ///  <br />
  ///  en: Order item
  /// </param>
  /// <param name="AErrors">
  ///  hu: Hibaüzenetek listája (kimeneti paraméter)
  ///  <br />
  ///  en: List of error messages (output parameter)
  /// </param>
  /// <returns>
  ///  hu: Igaz, ha a validáció sikeres
  ///  <br />
  ///  en: True if validation is successful
  /// </returns>
  public static bool TryValidate(this TOrderItem AOrderItem, out IReadOnlyList<string> AErrors)
  {
    var errors = TValidationHelper.GetAllErrors(AOrderItem);
    AErrors = errors;
    return errors.Count == 0;
  }

  /// <summary>
  ///  hu: Request objektum validálása
  ///  <br />
  ///  en: Validate request object
  /// </summary>
  /// <typeparam name="TRequest">
  ///  hu: A request típusa
  ///  <br />
  ///  en: The request type
  /// </typeparam>
  /// <param name="ARequest">
  ///  hu: A validálandó request
  ///  <br />
  ///  en: The request to validate
  /// </param>
  /// <param name="AErrors">
  ///  hu: Hibaüzenetek listája (kimeneti paraméter)
  ///  <br />
  ///  en: List of error messages (output parameter)
  /// </param>
  /// <returns>
  ///  hu: Igaz, ha a validáció sikeres
  ///  <br />
  ///  en: True if validation is successful
  /// </returns>
  public static bool TryValidateRequest<TRequest>(this TRequest ARequest, out IReadOnlyList<string> AErrors)
    where TRequest : class, IOrderRequest
  {
    var errors = TValidationHelper.GetAllErrors(ARequest);
    AErrors = errors;
    return errors.Count == 0;
  }
}