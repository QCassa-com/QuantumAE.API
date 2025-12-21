using JetBrains.Annotations;
using QuantumAE.Validation.Resources;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Lokalizált kötelező mező validációs attribútum.
/// <br />
/// en: Localized required field validation attribute.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
{
  /// <inheritdoc />
  public override string FormatErrorMessage(string AName)
  {
    // Ha explicit ErrorMessage van megadva, azt használjuk
    if (!string.IsNullOrEmpty(ErrorMessage))
    {
      return string.Format(ErrorMessage, AName);
    }

    // Egyébként a lokalizált üzenetet használjuk
    var template = ValidationMessages.Required;
    return string.Format(template, AName);
  }
}
