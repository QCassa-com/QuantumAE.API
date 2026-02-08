using RequiredAttribute = QuantumAE.Validation.RequiredAttribute;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: RequiredAttribute unit tesztek
/// <br />
/// en: RequiredAttribute unit tests
/// </summary>
public class TRequiredAttributeTests
{
  #region Validation Tests

  /// <summary>
  /// hu: Teszt: Nem-null és nem-üres érték sikeres validáció
  /// <br />
  /// en: Test: Non-null and non-empty value passes validation
  /// </summary>
  [Theory]
  [InlineData("hello")]
  [InlineData("a")]
  [InlineData("Hello World")]
  public void WhenValueIsNotEmpty_ShouldPass(string AValue)
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Whitespace string sikertelen validáció (alapértelmezett viselkedés)
  /// <br />
  /// en: Test: Whitespace string fails validation (default behavior)
  /// </summary>
  [Theory]
  [InlineData(" ")]
  [InlineData("   ")]
  [InlineData("\t")]
  public void WhenValueIsWhitespace_ShouldFail(string AValue)
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(AValue);

    Assert.False(isValid);
  }

  /// <summary>
  /// hu: Teszt: Null érték sikertelen validáció
  /// <br />
  /// en: Test: Null value fails validation
  /// </summary>
  [Fact]
  public void WhenValueIsNull_ShouldFail()
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(null);

    Assert.False(isValid);
  }

  /// <summary>
  /// hu: Teszt: Üres string sikertelen validáció (alapértelmezett viselkedés)
  /// <br />
  /// en: Test: Empty string fails validation (default behavior)
  /// </summary>
  [Fact]
  public void WhenValueIsEmptyString_ShouldFail()
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(string.Empty);

    Assert.False(isValid);
  }

  /// <summary>
  /// hu: Teszt: AllowEmptyStrings = true esetén üres string elfogadott
  /// <br />
  /// en: Test: Empty string accepted when AllowEmptyStrings = true
  /// </summary>
  [Fact]
  public void WhenAllowEmptyStringsTrue_AndValueIsEmpty_ShouldPass()
  {
    var attribute = new RequiredAttribute { AllowEmptyStrings = true };

    var isValid = attribute.IsValid(string.Empty);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Szám típusú értékek elfogadása
  /// <br />
  /// en: Test: Numeric values are accepted
  /// </summary>
  [Theory]
  [InlineData(0)]
  [InlineData(42)]
  [InlineData(-1)]
  public void WhenValueIsNumber_ShouldPass(int AValue)
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Objektum típusú értékek elfogadása
  /// <br />
  /// en: Test: Object values are accepted
  /// </summary>
  [Fact]
  public void WhenValueIsObject_ShouldPass()
  {
    var attribute = new RequiredAttribute();

    var isValid = attribute.IsValid(new object());

    Assert.True(isValid);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Lokalizált hibaüzenet tartalmazza a mező nevét
  /// <br />
  /// en: Test: Localized error message contains field name
  /// </summary>
  [Fact]
  public void FormatErrorMessage_ShouldContainFieldName()
  {
    var attribute = new RequiredAttribute();

    var message = attribute.FormatErrorMessage("TestField");

    Assert.Contains("TestField", message);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new RequiredAttribute { ErrorMessage = "Custom: {0} is mandatory" };

    var message = attribute.FormatErrorMessage("MyField");

    Assert.Equal("Custom: MyField is mandatory", message);
  }

  /// <summary>
  /// hu: Teszt: Alapértelmezett üzenet ha nincs egyedi megadva
  /// <br />
  /// en: Test: Default message when no custom is specified
  /// </summary>
  [Fact]
  public void DefaultErrorMessage_ShouldBeLocalized()
  {
    var attribute = new RequiredAttribute();

    var message = attribute.FormatErrorMessage("TestField");

    // A lokalizált üzenet tartalmazza a mező nevét
    Assert.NotEmpty(message);
    Assert.Contains("TestField", message);
  }

  #endregion
}
