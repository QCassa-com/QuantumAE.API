using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Validációs attribútum, amely ellenőrzi, hogy a decimális érték pozitív-e.
/// <br />
/// en: Validation attribute that checks if a decimal value is positive.
/// </summary>
/// <param name="AAllowZero">
/// hu: Ha igaz, a nulla is érvényes érték
/// <br />
/// en: If true, zero is also a valid value
/// </param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class PositiveDecimalAttribute(bool AAllowZero = false) : ValidationAttribute
{
  /// <summary>
  /// hu: Nulla érték engedélyezése
  /// <br />
  /// en: Allow zero value
  /// </summary>
  public bool AllowZero { get; } = AAllowZero;

  /// <inheritdoc />
  protected override ValidationResult? IsValid(object? AValue, ValidationContext AValidationContext)
  {
    // Null értékek elfogadása (használj [Required]-et ha kötelező)
    if (AValue == null)
      return ValidationResult.Success;

    var decimalValue = AValue switch
    {
      decimal d => d,
      double dbl => (decimal)dbl,
      float f => (decimal)f,
      int i => i,
      long l => l,
      _ => (decimal?)null
    };

    if (decimalValue == null)
    {
      var notNumericMessage = GetFormattedErrorMessage(AValidationContext.DisplayName, "NotNumeric");
      return new ValidationResult(notNumericMessage, [AValidationContext.MemberName!]);
    }

    var isValid = AllowZero ? decimalValue >= 0 : decimalValue > 0;

    if (!isValid)
    {
      var messageKey = AllowZero ? "AllowZero" : "Default";
      var message = GetFormattedErrorMessage(AValidationContext.DisplayName, messageKey);
      return new ValidationResult(message, [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }

  /// <summary>
  /// hu: Formázott hibaüzenet generálása
  /// <br />
  /// en: Generate formatted error message
  /// </summary>
  private string GetFormattedErrorMessage(string ADisplayName, string AMessageKey)
  {
    // Ha explicit ErrorMessage van megadva, azt használjuk
    if (!string.IsNullOrEmpty(ErrorMessage))
    {
      return string.Format(ErrorMessage, ADisplayName);
    }

    // Egyébként a lokalizált üzenetet használjuk
    var template = AMessageKey switch
    {
      "NotNumeric" => ValidationMessages.PositiveDecimal_NotNumeric,
      "AllowZero" => ValidationMessages.PositiveDecimal_AllowZero,
      _ => ValidationMessages.PositiveDecimal
    };

    return string.Format(template, ADisplayName);
  }
}
