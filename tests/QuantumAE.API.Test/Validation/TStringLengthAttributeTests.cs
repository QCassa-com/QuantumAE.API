using StringLengthAttribute = QuantumAE.Validation.StringLengthAttribute;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: StringLengthAttribute unit tesztek
/// <br />
/// en: StringLengthAttribute unit tests
/// </summary>
public class TStringLengthAttributeTests
{
  #region Maximum Length Tests

  /// <summary>
  /// hu: Teszt: String rövidebb mint a maximum - sikeres
  /// <br />
  /// en: Test: String shorter than maximum - passes
  /// </summary>
  [Theory]
  [InlineData("abc", 10)]
  [InlineData("", 5)]
  [InlineData("a", 1)]
  public void WhenStringIsShorterThanMax_ShouldPass(string AValue, int AMaxLength)
  {
    var attribute = new StringLengthAttribute(AMaxLength);

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: String hossza pontosan a maximum - sikeres
  /// <br />
  /// en: Test: String length exactly at maximum - passes
  /// </summary>
  [Theory]
  [InlineData("abcde", 5)]
  [InlineData("a", 1)]
  [InlineData("", 0)]
  public void WhenStringLengthEqualsMax_ShouldPass(string AValue, int AMaxLength)
  {
    var attribute = new StringLengthAttribute(AMaxLength);

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: String hosszabb mint a maximum - sikertelen
  /// <br />
  /// en: Test: String longer than maximum - fails
  /// </summary>
  [Theory]
  [InlineData("abcdef", 5)]
  [InlineData("ab", 1)]
  [InlineData("a", 0)]
  public void WhenStringIsLongerThanMax_ShouldFail(string AValue, int AMaxLength)
  {
    var attribute = new StringLengthAttribute(AMaxLength);

    var isValid = attribute.IsValid(AValue);

    Assert.False(isValid);
  }

  #endregion

  #region Minimum Length Tests

  /// <summary>
  /// hu: Teszt: String hossza a minimum és maximum között - sikeres
  /// <br />
  /// en: Test: String length between minimum and maximum - passes
  /// </summary>
  [Theory]
  [InlineData("abc", 2, 5)]
  [InlineData("ab", 2, 5)]
  [InlineData("abcde", 2, 5)]
  public void WhenStringLengthInRange_ShouldPass(string AValue, int AMinLength, int AMaxLength)
  {
    var attribute = new StringLengthAttribute(AMaxLength) { MinimumLength = AMinLength };

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: String rövidebb mint a minimum - sikertelen
  /// <br />
  /// en: Test: String shorter than minimum - fails
  /// </summary>
  [Theory]
  [InlineData("a", 2, 5)]
  [InlineData("", 1, 10)]
  public void WhenStringIsShorterThanMin_ShouldFail(string AValue, int AMinLength, int AMaxLength)
  {
    var attribute = new StringLengthAttribute(AMaxLength) { MinimumLength = AMinLength };

    var isValid = attribute.IsValid(AValue);

    Assert.False(isValid);
  }

  #endregion

  #region Null Value Tests

  /// <summary>
  /// hu: Teszt: Null érték elfogadott (opcionális mező)
  /// <br />
  /// en: Test: Null value accepted (optional field)
  /// </summary>
  [Fact]
  public void WhenValueIsNull_ShouldPass()
  {
    var attribute = new StringLengthAttribute(10);

    var isValid = attribute.IsValid(null);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Null érték elfogadott minimum hosszal is
  /// <br />
  /// en: Test: Null value accepted even with minimum length
  /// </summary>
  [Fact]
  public void WhenValueIsNull_WithMinLength_ShouldPass()
  {
    var attribute = new StringLengthAttribute(10) { MinimumLength = 5 };

    var isValid = attribute.IsValid(null);

    Assert.True(isValid);
  }

  #endregion

  #region Constructor Tests

  /// <summary>
  /// hu: Teszt: Konstruktor beállítja a MaximumLength értéket
  /// <br />
  /// en: Test: Constructor sets MaximumLength value
  /// </summary>
  [Fact]
  public void Constructor_ShouldSetMaximumLength()
  {
    var attribute = new StringLengthAttribute(100);

    Assert.Equal(100, attribute.MaximumLength);
  }

  /// <summary>
  /// hu: Teszt: MinimumLength alapértelmezetten 0
  /// <br />
  /// en: Test: MinimumLength defaults to 0
  /// </summary>
  [Fact]
  public void MinimumLength_ShouldDefaultToZero()
  {
    var attribute = new StringLengthAttribute(100);

    Assert.Equal(0, attribute.MinimumLength);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Hibaüzenet csak maximum hossznál
  /// <br />
  /// en: Test: Error message with maximum length only
  /// </summary>
  [Fact]
  public void FormatErrorMessage_MaxOnly_ShouldContainFieldAndMax()
  {
    var attribute = new StringLengthAttribute(50);

    var message = attribute.FormatErrorMessage("Description");

    Assert.Contains("Description", message);
    Assert.Contains("50", message);
  }

  /// <summary>
  /// hu: Teszt: Hibaüzenet minimum és maximum hossznál
  /// <br />
  /// en: Test: Error message with minimum and maximum length
  /// </summary>
  [Fact]
  public void FormatErrorMessage_WithMinAndMax_ShouldContainBoth()
  {
    var attribute = new StringLengthAttribute(100) { MinimumLength = 10 };

    var message = attribute.FormatErrorMessage("Name");

    Assert.Contains("Name", message);
    Assert.Contains("10", message);
    Assert.Contains("100", message);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new StringLengthAttribute(50)
    {
      MinimumLength = 5,
      ErrorMessage = "Custom: {0} length must be {2}-{1}"
    };

    var message = attribute.FormatErrorMessage("Field");

    Assert.Equal("Custom: Field length must be 5-50", message);
  }

  #endregion
}
