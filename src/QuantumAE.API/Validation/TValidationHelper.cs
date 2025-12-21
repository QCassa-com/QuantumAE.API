using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace QuantumAE.Validation;

/// <summary>
/// hu: Validációs segédosztály, amely egységes validációt biztosít kliens és szerver oldalon.
/// <br />
/// en: Validation helper class that provides unified validation for client and server side.
/// </summary>
[PublicAPI]
public static class TValidationHelper
{
  /// <summary>
  /// hu: Objektum validálása DataAnnotations attribútumok alapján.
  /// <br />
  /// en: Validate an object based on DataAnnotations attributes.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <param name="AResults">
  /// hu: A validációs eredmények listája (kimeneti paraméter)
  /// <br />
  /// en: The list of validation results (output parameter)
  /// </param>
  /// <param name="AValidateAllProperties">
  /// hu: Ha igaz, minden property-t validál, nem csak a Required mezőket
  /// <br />
  /// en: If true, validates all properties, not just Required fields
  /// </param>
  /// <returns>
  /// hu: Igaz, ha a validáció sikeres (nincs hiba)
  /// <br />
  /// en: True if validation is successful (no errors)
  /// </returns>
  public static bool TryValidate<T>(
    T AInstance,
    out List<ValidationResult> AResults,
    bool AValidateAllProperties = true) where T : class
  {
    AResults = [];
    var context = new ValidationContext(AInstance);
    return Validator.TryValidateObject(AInstance, context, AResults, AValidateAllProperties);
  }

  /// <summary>
  /// hu: Objektum validálása, kivételt dob hiba esetén.
  /// <br />
  /// en: Validate an object, throws exception on error.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <param name="AValidateAllProperties">
  /// hu: Ha igaz, minden property-t validál
  /// <br />
  /// en: If true, validates all properties
  /// </param>
  /// <exception cref="ValidationException">
  /// hu: Ha a validáció sikertelen
  /// <br />
  /// en: If validation fails
  /// </exception>
  public static void Validate<T>(T AInstance, bool AValidateAllProperties = true) where T : class
  {
    var context = new ValidationContext(AInstance);
    Validator.ValidateObject(AInstance, context, AValidateAllProperties);
  }

  /// <summary>
  /// hu: Objektum validálása és az első hiba üzenet visszaadása.
  /// <br />
  /// en: Validate an object and return the first error message.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <returns>
  /// hu: Az első hiba üzenet, vagy null ha nincs hiba
  /// <br />
  /// en: The first error message, or null if no error
  /// </returns>
  public static string? GetFirstError<T>(T AInstance) where T : class
  {
    if (TryValidate(AInstance, out var results))
      return null;

    return results.FirstOrDefault()?.ErrorMessage;
  }

  /// <summary>
  /// hu: Objektum validálása és az összes hiba üzenet visszaadása.
  /// <br />
  /// en: Validate an object and return all error messages.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <returns>
  /// hu: A hiba üzenetek listája (üres lista ha nincs hiba)
  /// <br />
  /// en: The list of error messages (empty list if no error)
  /// </returns>
  public static IReadOnlyList<string> GetAllErrors<T>(T AInstance) where T : class
  {
    if (TryValidate(AInstance, out var results))
      return [];

    return results
      .Where(AResult => AResult.ErrorMessage != null)
      .Select(AResult => AResult.ErrorMessage!)
      .ToList();
  }

  /// <summary>
  /// hu: Ellenőrzi, hogy az objektum érvényes-e.
  /// <br />
  /// en: Check if the object is valid.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <returns>
  /// hu: Igaz, ha az objektum érvényes
  /// <br />
  /// en: True if the object is valid
  /// </returns>
  public static bool IsValid<T>(T AInstance) where T : class
  {
    return TryValidate(AInstance, out _);
  }

  /// <summary>
  /// hu: Rekurzív validáció beágyazott objektumokkal.
  /// <br />
  /// en: Recursive validation with nested objects.
  /// </summary>
  /// <typeparam name="T">
  /// hu: A validálandó objektum típusa
  /// <br />
  /// en: The type of object to validate
  /// </typeparam>
  /// <param name="AInstance">
  /// hu: A validálandó objektum példány
  /// <br />
  /// en: The object instance to validate
  /// </param>
  /// <param name="AResults">
  /// hu: A validációs eredmények listája (kimeneti paraméter)
  /// <br />
  /// en: The list of validation results (output parameter)
  /// </param>
  /// <returns>
  /// hu: Igaz, ha a validáció sikeres
  /// <br />
  /// en: True if validation is successful
  /// </returns>
  public static bool TryValidateRecursive<T>(T AInstance, out List<ValidationResult> AResults) where T : class
  {
    AResults = [];
    return ValidateRecursiveInternal(AInstance, AResults, string.Empty);
  }

  private static bool ValidateRecursiveInternal(object AInstance, List<ValidationResult> AResults, string APrefix)
  {
    var context = new ValidationContext(AInstance);
    var localResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(AInstance, context, localResults, true);

    foreach (var result in localResults)
    {
      var memberNames = result.MemberNames
        .Select(AName => string.IsNullOrEmpty(APrefix) ? AName : $"{APrefix}.{AName}")
        .ToList();

      AResults.Add(new ValidationResult(result.ErrorMessage, memberNames));
    }

    // Rekurzív validáció beágyazott objektumokhoz
    foreach (var property in AInstance.GetType().GetProperties())
    {
      if (!property.CanRead)
        continue;

      var value = property.GetValue(AInstance);

      if (value == null)
        continue;

      var propertyType = property.PropertyType;

      // Skip primitívek, stringek, értéktípusok
      if (propertyType.IsPrimitive || propertyType == typeof(string) || propertyType.IsValueType)
        continue;

      // Skip gyűjtemények (külön kezelendők ha szükséges)
      if (propertyType.IsArray || (propertyType.IsGenericType &&
          propertyType.GetGenericTypeDefinition() == typeof(List<>)))
        continue;

      var nestedPrefix = string.IsNullOrEmpty(APrefix) ? property.Name : $"{APrefix}.{property.Name}";
      isValid &= ValidateRecursiveInternal(value, AResults, nestedPrefix);
    }

    return isValid;
  }
}
