using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Validációs attribútum, amely kötelezővé teszi a mezőt, ha egy másik mező üres.
/// Használat: Ha a Name és Barcode közül legalább az egyiknek kitöltöttnek kell lennie.
/// <br />
/// en: Validation attribute that makes a field required if another field is empty.
/// Usage: When at least one of Name or Barcode must be filled.
/// </summary>
/// <param name="AOtherProperty">
/// hu: A másik property neve, amelyet ellenőrizni kell
/// <br />
/// en: The name of the other property to check
/// </param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class RequiredIfEmptyAttribute(string AOtherProperty) : ValidationAttribute
{
  /// <summary>
  /// hu: A másik property neve
  /// <br />
  /// en: The other property name
  /// </summary>
  public string OtherProperty { get; } = AOtherProperty;

  /// <inheritdoc />
  protected override ValidationResult? IsValid(object? AValue, ValidationContext AValidationContext)
  {
    var otherPropertyInfo = AValidationContext.ObjectType.GetProperty(OtherProperty);

    if (otherPropertyInfo == null)
    {
      return new ValidationResult($"Property '{OtherProperty}' not found.");
    }

    var otherValue = otherPropertyInfo.GetValue(AValidationContext.ObjectInstance);
    var currentIsEmpty = IsEmpty(AValue);
    var otherIsEmpty = IsEmpty(otherValue);

    if (currentIsEmpty && otherIsEmpty)
    {
      var message = this.FormatMessage(ValidationMessages.RequiredIfEmpty, AValidationContext.DisplayName, OtherProperty);
      return new ValidationResult(message, [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }

  private static bool IsEmpty(object? AValue)
  {
    return AValue switch
    {
      null => true,
      string s => string.IsNullOrWhiteSpace(s),
      _ => false
    };
  }
}
