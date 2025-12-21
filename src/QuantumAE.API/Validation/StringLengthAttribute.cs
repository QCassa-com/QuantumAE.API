using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Lokalizált string hossz validációs attribútum.
/// <br />
/// en: Localized string length validation attribute.
/// </summary>
/// <param name="AMaximumLength">
/// hu: Maximális karakterszám
/// <br />
/// en: Maximum character count
/// </param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class StringLengthAttribute(int AMaximumLength) : System.ComponentModel.DataAnnotations.StringLengthAttribute(AMaximumLength)
{
  /// <inheritdoc />
  public override string FormatErrorMessage(string AName)
  {
    // Ha explicit ErrorMessage van megadva, azt használjuk
    if (!string.IsNullOrEmpty(ErrorMessage))
    {
      return string.Format(ErrorMessage, AName, MaximumLength, MinimumLength);
    }

    // Egyébként a lokalizált üzenetet használjuk
    if (MinimumLength > 0)
    {
      var template = ValidationMessages.StringLength_Range;
      return string.Format(template, AName, MinimumLength, MaximumLength);
    }
    else
    {
      var template = ValidationMessages.StringLength_Max;
      return string.Format(template, AName, MaximumLength);
    }
  }
}
