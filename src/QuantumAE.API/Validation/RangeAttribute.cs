using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Lokalizált tartomány validációs attribútum.
/// <br />
/// en: Localized range validation attribute.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class RangeAttribute : System.ComponentModel.DataAnnotations.RangeAttribute
{
  /// <summary>
  /// hu: Tartomány validáció int értékekhez.
  /// <br />
  /// en: Range validation for int values.
  /// </summary>
  /// <param name="AMinimum">
  /// hu: Minimális érték
  /// <br />
  /// en: Minimum value
  /// </param>
  /// <param name="AMaximum">
  /// hu: Maximális érték
  /// <br />
  /// en: Maximum value
  /// </param>
  public RangeAttribute(int AMinimum, int AMaximum) : base(AMinimum, AMaximum)
  {
  }

  /// <summary>
  /// hu: Tartomány validáció double értékekhez.
  /// <br />
  /// en: Range validation for double values.
  /// </summary>
  /// <param name="AMinimum">
  /// hu: Minimális érték
  /// <br />
  /// en: Minimum value
  /// </param>
  /// <param name="AMaximum">
  /// hu: Maximális érték
  /// <br />
  /// en: Maximum value
  /// </param>
  public RangeAttribute(double AMinimum, double AMaximum) : base(AMinimum, AMaximum)
  {
  }

  /// <summary>
  /// hu: Tartomány validáció tetszőleges típushoz string formátumban.
  /// <br />
  /// en: Range validation for any type with string format.
  /// </summary>
  /// <param name="AType">
  /// hu: Érték típusa
  /// <br />
  /// en: Value type
  /// </param>
  /// <param name="AMinimum">
  /// hu: Minimális érték stringként
  /// <br />
  /// en: Minimum value as string
  /// </param>
  /// <param name="AMaximum">
  /// hu: Maximális érték stringként
  /// <br />
  /// en: Maximum value as string
  /// </param>
  public RangeAttribute(Type AType, string AMinimum, string AMaximum) : base(AType, AMinimum, AMaximum)
  {
  }

  /// <inheritdoc />
  public override string FormatErrorMessage(string AName)
  {
    // Ha explicit ErrorMessage van megadva, azt használjuk
    if (!string.IsNullOrEmpty(ErrorMessage))
    {
      return string.Format(ErrorMessage, AName, Minimum, Maximum);
    }

    // Egyébként a lokalizált üzenetet használjuk
    var template = ValidationMessages.Range;
    return string.Format(template, AName, Minimum, Maximum);
  }
}
