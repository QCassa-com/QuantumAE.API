using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Minimális érték validációs attribútum.
/// <br />
/// en: Minimum value validation attribute.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MinAttribute : ValidationAttribute
{
  /// <summary>
  /// hu: Minimális érték (double).
  /// <br />
  /// en: Minimum value (double).
  /// </summary>
  public double Minimum { get; }

  /// <summary>
  /// hu: Ha igaz, a minimális érték nem megengedett (exkluzív).
  /// <br />
  /// en: If true, the minimum value is not allowed (exclusive).
  /// </summary>
  public bool Exclusive { get; set; }

  /// <summary>
  /// hu: Minimális érték validáció int értékhez.
  /// <br />
  /// en: Minimum value validation for int.
  /// </summary>
  /// <param name="AMinimum">
  /// hu: Minimális érték
  /// <br />
  /// en: Minimum value
  /// </param>
  public MinAttribute(int AMinimum)
  {
    Minimum = AMinimum;
  }

  /// <summary>
  /// hu: Minimális érték validáció double értékhez.
  /// <br />
  /// en: Minimum value validation for double.
  /// </summary>
  /// <param name="AMinimum">
  /// hu: Minimális érték
  /// <br />
  /// en: Minimum value
  /// </param>
  public MinAttribute(double AMinimum)
  {
    Minimum = AMinimum;
  }

  /// <inheritdoc />
  protected override ValidationResult? IsValid(object? AValue, ValidationContext AValidationContext)
  {
    if (AValue == null)
      return ValidationResult.Success;

    var doubleValue = AValue switch
    {
      int i => (double)i,
      long l => (double)l,
      decimal d => (double)d,
      double dbl => dbl,
      float f => (double)f,
      _ => (double?)null
    };

    if (doubleValue == null)
    {
      return new ValidationResult(
        this.FormatMessage(ValidationMessages.PositiveDecimal_NotNumeric, AValidationContext.DisplayName),
        [AValidationContext.MemberName!]);
    }

    var isValid = Exclusive
      ? doubleValue > Minimum
      : doubleValue >= Minimum;

    if (!isValid)
    {
      var template = Exclusive ? ValidationMessages.Min_Exclusive : ValidationMessages.Min;
      return new ValidationResult(
        this.FormatMessage(template, AValidationContext.DisplayName, Minimum),
        [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }
}
