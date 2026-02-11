
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using QuantumAE.Models;
using QuantumAE.Validation;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Api.Orders;

/// <summary>
///  hu: Rendelés tétel validátor osztály
///  <br />
///  en: Order item validator class
/// </summary>
[PublicAPI]
public static partial class TOrderValidator
{
  private static readonly HashSet<string> ValidVatCodes =
  [
    "A", "B", "C", "D", "E", "N",
    "TAM", "AAM", "EAM", "ATK", "TRA", "SEC", "ART", "ANT", "EUE", "HO"
  ];

  private static readonly HashSet<string> ValidVatStatuses =
  [
    "DOMESTIC", "OTHER", "PRIVATE_PERSON"
  ];

  [GeneratedRegex(@"^\d{8}$|^\d{13}$")]
  private static partial Regex EanRegex();

  /// <summary>
  ///  hu: Rendelés tétel validálása attribútumok alapján
  ///  <br />
  ///  en: Validate order item based on attributes
  /// </summary>
  public static TOrderItemResult Validate(this TOrderItem AOrderItem)
  {
    if (!TValidationHelper.TryValidate(AOrderItem, out var results))
    {
      foreach (var result in results)
      {
        if (result.MemberNames.Any(AName => AName is "Name" or "Barcode") &&
            result.ErrorMessage?.Contains("must be provided") == true)
        {
          return TOrderItemResult.NameAndBarCodeEmpty;
        }

        if (result.MemberNames.Any(AName => AName == "Quantity"))
        {
          return TOrderItemResult.InvalidQuantity;
        }

        if (result.MemberNames.Any(AName => AName is "UnitPrice" or "Total"))
        {
          return TOrderItemResult.InvalidPrice;
        }
      }

      return TOrderItemResult.NameAndBarCodeEmpty;
    }

    return TOrderItemResult.Success;
  }

  /// <summary>
  ///  hu: Rendelés tétel validálása részletes hibaüzenetekkel
  ///  <br />
  ///  en: Validate order item with detailed error messages
  /// </summary>
  public static bool TryValidate(this TOrderItem AOrderItem, out IReadOnlyList<string> AErrors)
  {
    var errors = new List<string>();
    errors.AddRange(TValidationHelper.GetAllErrors(AOrderItem));

    if (!string.IsNullOrEmpty(AOrderItem.VatCode) && !ValidVatCodes.Contains(AOrderItem.VatCode))
    {
      errors.Add(string.Format(ApiResponseMessages.Order_InvalidVatCode, AOrderItem.VatCode, string.Join(", ", ValidVatCodes)));
    }

    if (!string.IsNullOrEmpty(AOrderItem.Barcode) && !EanRegex().IsMatch(AOrderItem.Barcode))
    {
      errors.Add(string.Format(ApiResponseMessages.Order_InvalidBarcode, AOrderItem.Barcode));
    }

    AErrors = errors;
    return errors.Count == 0;
  }

  /// <summary>
  ///  hu: Request objektum validálása
  ///  <br />
  ///  en: Validate request object
  /// </summary>
  public static bool TryValidateRequest<TRequest>(this TRequest ARequest, out IReadOnlyList<string> AErrors)
    where TRequest : class, IOrderRequest
  {
    var errors = TValidationHelper.GetAllErrors(ARequest);
    AErrors = errors;
    return errors.Count == 0;
  }

  /// <summary>
  ///  hu: Fizetési adatok validálása
  ///  <br />
  ///  en: Validate payment data
  /// </summary>
  public static bool TryValidatePayment(this TPayment APayment, decimal ATotalAmount, out IReadOnlyList<string> AErrors)
  {
    var errors = new List<string>();

    var hasOldMode = APayment.Amount.HasValue || !string.IsNullOrEmpty(APayment.Method);
    var hasNewMode = APayment.Cash.HasValue || APayment.Card.HasValue || APayment.Szep.HasValue || APayment.Afr.HasValue;

    if (hasOldMode && hasNewMode)
    {
      errors.Add(ApiResponseMessages.Order_PaymentModeMix);
    }

    if (hasNewMode)
    {
      var totalPaid = (APayment.Cash ?? 0) + (APayment.Card ?? 0) + (APayment.Szep ?? 0) + (APayment.Afr ?? 0);

      if (totalPaid < (int)Math.Floor(ATotalAmount))
      {
        errors.Add(string.Format(ApiResponseMessages.Order_InsufficientPayment, totalPaid, ATotalAmount));
      }
    }

    AErrors = errors;
    return errors.Count == 0;
  }

  /// <summary>
  ///  hu: Vevő adatok validálása számla esetén
  ///  <br />
  ///  en: Validate customer data for invoice
  /// </summary>
  public static bool TryValidateCustomerForInvoice(this TCustomer ACustomer, out IReadOnlyList<string> AErrors)
  {
    var errors = new List<string>();

    if (string.IsNullOrEmpty(ACustomer.VatStatus))
    {
      errors.Add(ApiResponseMessages.Order_VatStatusRequired);
    }
    else if (!ValidVatStatuses.Contains(ACustomer.VatStatus))
    {
      errors.Add(string.Format(ApiResponseMessages.Order_InvalidVatStatus, ACustomer.VatStatus, string.Join(", ", ValidVatStatuses)));
    }
    else if (ACustomer.VatStatus == "DOMESTIC" && string.IsNullOrEmpty(ACustomer.TaxNumber))
    {
      errors.Add(ApiResponseMessages.Order_TaxNumberRequired);
    }
    else if (ACustomer.VatStatus == "OTHER" &&
             string.IsNullOrEmpty(ACustomer.CommunityVatNumber) &&
             string.IsNullOrEmpty(ACustomer.ThirdStateTaxId))
    {
      errors.Add(ApiResponseMessages.Order_CommunityOrThirdStateTaxIdRequired);
    }

    AErrors = errors;
    return errors.Count == 0;
  }
}
