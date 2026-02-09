using System.ComponentModel.DataAnnotations;

namespace QuantumAE.Validation;

/// <summary>
///   hu: Extension metódus validációs attribútumokhoz a hibaüzenet formázás egységesítésére.
///   Ha az attribútumon explicit ErrorMessage van megadva, azt használja; egyébként a lokalizált template-et.
///   <br />
///   en: Extension method for validation attributes to unify error message formatting.
///   Uses explicit ErrorMessage if set on the attribute; otherwise uses the localized template.
/// </summary>
internal static class ValidationAttributeExtensions
{
  /// <summary>
  ///   hu: Formázott hibaüzenet generálása. Ha van explicit ErrorMessage, azt használja;
  ///   egyébként a megadott lokalizált template-et.
  ///   <br />
  ///   en: Generate formatted error message. Uses explicit ErrorMessage if set;
  ///   otherwise uses the provided localized template.
  /// </summary>
  /// <param name="AAttribute">
  ///   hu: A validációs attribútum
  ///   <br />
  ///   en: The validation attribute
  /// </param>
  /// <param name="ALocalizedTemplate">
  ///   hu: A lokalizált üzenet sablon (pl. ValidationMessages.Required)
  ///   <br />
  ///   en: The localized message template (e.g. ValidationMessages.Required)
  /// </param>
  /// <param name="AArgs">
  ///   hu: A string.Format paraméterek
  ///   <br />
  ///   en: The string.Format arguments
  /// </param>
  public static string FormatMessage(this ValidationAttribute AAttribute, string ALocalizedTemplate, params object[] AArgs)
  {
    var template = !string.IsNullOrEmpty(AAttribute.ErrorMessage)
      ? AAttribute.ErrorMessage
      : ALocalizedTemplate;

    return string.Format(template, AArgs);
  }
}
