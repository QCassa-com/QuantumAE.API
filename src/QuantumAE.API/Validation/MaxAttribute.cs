using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Maximális érték validációs attribútum.
/// <br />
/// en: Maximum value validation attribute.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MaxAttribute : ValidationAttribute
{
  /// <summary>
  /// hu: Maximális érték (double).
  /// <br />
  /// en: Maximum value (double).
  /// </summary>
  public double Maximum { get; }

  /// <summary>
  /// hu: Ha igaz, a maximális érték nem megengedett (exkluzív).
  /// <br />
  /// en: If true, the maximum value is not allowed (exclusive).
  /// </summary>
  public bool Exclusive { get; set; }

  /// <summary>
  /// hu: Maximális érték validáció int értékhez.
  /// <br />
  /// en: Maximum value validation for int.
  /// </summary>
  /// <param name="AMaximum">
  /// hu: Maximális érték
  /// <br />
  /// en: Maximum value
  /// </param>
  public MaxAttribute(int AMaximum)
  {
    Maximum = AMaximum;
  }

  /// <summary>
  /// hu: Maximális érték validáció double értékhez.
  /// <br />
  /// en: Maximum value validation for double.
  /// </summary>
  /// <param name="AMaximum">
  /// hu: Maximális érték
  /// <br />
  /// en: Maximum value
  /// </param>
  public MaxAttribute(double AMaximum)
  {
    Maximum = AMaximum;
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
      ? doubleValue < Maximum
      : doubleValue <= Maximum;

    if (!isValid)
    {
      var template = Exclusive ? ValidationMessages.Max_Exclusive : ValidationMessages.Max;
      return new ValidationResult(
        this.FormatMessage(template, AValidationContext.DisplayName, Maximum),
        [AValidationContext.MemberName!]);
    }

    return ValidationResult.Success;
  }
}
