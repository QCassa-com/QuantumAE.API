using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Validációs attribútum, amely ellenőrzi, hogy a string nem üres és nem csak whitespace.
/// Különbözik a [Required]-tól: null értéket elfogad, de üres stringet nem.
/// <br />
/// en: Validation attribute that checks if a string is not empty and not whitespace only.
/// Differs from [Required]: accepts null but not empty string.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class NotEmptyStringAttribute : ValidationAttribute
{
  /// <inheritdoc />
  protected override ValidationResult? IsValid(object? AValue, ValidationContext AValidationContext)
  {
    // Null értékek elfogadása (használj [Required]-et ha kötelező)
    if (AValue == null)
      return ValidationResult.Success;

    if (AValue is not string stringValue)
    {
      return new ValidationResult(
        $"The field '{AValidationContext.DisplayName}' must be a string.",
        [AValidationContext.MemberName!]);
    }

    if (string.IsNullOrWhiteSpace(stringValue))
    {
      var message = GetFormattedErrorMessage(AValidationContext.DisplayName);
      return new ValidationResult(message, [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }

  /// <summary>
  /// hu: Formázott hibaüzenet generálása
  /// <br />
  /// en: Generate formatted error message
  /// </summary>
  private string GetFormattedErrorMessage(string ADisplayName)
  {
    // Ha explicit ErrorMessage van megadva, azt használjuk
    if (!string.IsNullOrEmpty(ErrorMessage))
    {
      return string.Format(ErrorMessage, ADisplayName);
    }

    // Egyébként a lokalizált üzenetet használjuk
    var template = ValidationMessages.NotEmptyString;
    return string.Format(template, ADisplayName);
  }
}
