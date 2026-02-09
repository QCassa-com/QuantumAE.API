using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
///   hu: AP szám (Adóügyi Pénztárgép szám) formátum validáció.
///   Formátum: 9 karakter, az első betű B-X (A, Y, Z kizárva), a többi 8 számjegy (0-9).
///   Regex: ^[B-X][0-9]{8}$
///   <br />
///   en: AP number (Tax Cash Register number) format validation.
///   Format: 9 characters, first letter B-X (A, Y, Z excluded), remaining 8 digits (0-9).
///   Regex: ^[B-X][0-9]{8}$
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed partial class ApNumberAttribute : ValidationAttribute
{
  [GeneratedRegex(@"^[B-X][0-9]{8}$")]
  private static partial Regex ApNumberRegex();

  /// <inheritdoc />
  protected override ValidationResult? IsValid(object? AValue, ValidationContext AValidationContext)
  {
    if (AValue == null)
      return ValidationResult.Success;

    if (AValue is not string stringValue)
    {
      return new ValidationResult(
        $"The field '{AValidationContext.DisplayName}' must be a string.",
        [AValidationContext.MemberName!]);
    }

    if (string.IsNullOrWhiteSpace(stringValue))
      return ValidationResult.Success;

    if (!ApNumberRegex().IsMatch(stringValue))
    {
      var message = this.FormatMessage(ValidationMessages.ApNumber, AValidationContext.DisplayName);
      return new ValidationResult(message, [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }
}
