using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;
using RangeAttribute = QuantumAE.Validation.RangeAttribute;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: RangeAttribute unit tesztek
/// <br />
/// en: RangeAttribute unit tests
/// </summary>
public class TRangeAttributeTests
{
  #region Int Range Tests

  /// <summary>
  /// hu: Teszt: Egész szám a tartományon belül - sikeres
  /// <br />
  /// en: Test: Integer within range - passes
  /// </summary>
  [Theory]
  [InlineData(1, 1, 10)]
  [InlineData(5, 1, 10)]
  [InlineData(10, 1, 10)]
  [InlineData(0, -5, 5)]
  [InlineData(-3, -5, 5)]
  public void IntRange_WhenValueInRange_ShouldPass(int AValue, int AMin, int AMax)
  {
    var attribute = new RangeAttribute(AMin, AMax);

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Egész szám a tartományon kívül - sikertelen
  /// <br />
  /// en: Test: Integer outside range - fails
  /// </summary>
  [Theory]
  [InlineData(0, 1, 10)]
  [InlineData(11, 1, 10)]
  [InlineData(-6, -5, 5)]
  [InlineData(100, 1, 10)]
  public void IntRange_WhenValueOutOfRange_ShouldFail(int AValue, int AMin, int AMax)
  {
    var attribute = new RangeAttribute(AMin, AMax);

    var isValid = attribute.IsValid(AValue);

    Assert.False(isValid);
  }

  #endregion

  #region Double Range Tests

  /// <summary>
  /// hu: Teszt: Double érték a tartományon belül - sikeres
  /// <br />
  /// en: Test: Double value within range - passes
  /// </summary>
  [Theory]
  [InlineData(0.0, 0.0, 1.0)]
  [InlineData(0.5, 0.0, 1.0)]
  [InlineData(1.0, 0.0, 1.0)]
  [InlineData(-0.5, -1.0, 1.0)]
  public void DoubleRange_WhenValueInRange_ShouldPass(double AValue, double AMin, double AMax)
  {
    var attribute = new RangeAttribute(AMin, AMax);

    var isValid = attribute.IsValid(AValue);

    Assert.True(isValid);
  }

  /// <summary>
  /// hu: Teszt: Double érték a tartományon kívül - sikertelen
  /// <br />
  /// en: Test: Double value outside range - fails
  /// </summary>
  [Theory]
  [InlineData(-0.1, 0.0, 1.0)]
  [InlineData(1.1, 0.0, 1.0)]
  [InlineData(2.0, 0.0, 1.0)]
  public void DoubleRange_WhenValueOutOfRange_ShouldFail(double AValue, double AMin, double AMax)
  {
    var attribute = new RangeAttribute(AMin, AMax);

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
    var attribute = new RangeAttribute(1, 100);

    var isValid = attribute.IsValid(null);

    Assert.True(isValid);
  }

  #endregion

  #region Constructor Tests

  /// <summary>
  /// hu: Teszt: Int konstruktor beállítja a Minimum és Maximum értékeket
  /// <br />
  /// en: Test: Int constructor sets Minimum and Maximum values
  /// </summary>
  [Fact]
  public void IntConstructor_ShouldSetMinAndMax()
  {
    var attribute = new RangeAttribute(1, 100);

    Assert.Equal(1, attribute.Minimum);
    Assert.Equal(100, attribute.Maximum);
  }

  /// <summary>
  /// hu: Teszt: Double konstruktor beállítja a Minimum és Maximum értékeket
  /// <br />
  /// en: Test: Double constructor sets Minimum and Maximum values
  /// </summary>
  [Fact]
  public void DoubleConstructor_ShouldSetMinAndMax()
  {
    var attribute = new RangeAttribute(0.5, 99.5);

    Assert.Equal(0.5, attribute.Minimum);
    Assert.Equal(99.5, attribute.Maximum);
  }

  /// <summary>
  /// hu: Teszt: Type konstruktor string értékekkel
  /// <br />
  /// en: Test: Type constructor with string values
  /// </summary>
  [Fact]
  public void TypeConstructor_ShouldSetMinAndMax()
  {
    var attribute = new RangeAttribute(typeof(decimal), "0.01", "999.99");

    Assert.Equal("0.01", attribute.Minimum);
    Assert.Equal("999.99", attribute.Maximum);
  }

  #endregion

  #region Different Types Tests

  /// <summary>
  /// hu: Teszt: Long típusú érték validálása int range-gel
  /// <br />
  /// en: Test: Long value validation with int range
  /// </summary>
  [Theory]
  [InlineData(50L, true)]
  [InlineData(1L, true)]
  [InlineData(100L, true)]
  [InlineData(0L, false)]
  [InlineData(101L, false)]
  public void WithLongValue_ShouldValidateCorrectly(long AValue, bool AExpectedValid)
  {
    var attribute = new RangeAttribute(1, 100);

    var isValid = attribute.IsValid(AValue);

    Assert.Equal(AExpectedValid, isValid);
  }

  /// <summary>
  /// hu: Teszt: Decimal típusú érték validálása double range-gel
  /// <br />
  /// en: Test: Decimal value validation with double range
  /// </summary>
  [Fact]
  public void WithDecimalValue_InRange_ShouldPass()
  {
    var attribute = new RangeAttribute(0.01, 999.99);

    var isValid = attribute.IsValid(100.50m);

    Assert.True(isValid);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Hibaüzenet tartalmazza a mező nevét és a tartományt
  /// <br />
  /// en: Test: Error message contains field name and range
  /// </summary>
  [Fact]
  public void FormatErrorMessage_ShouldContainFieldNameAndRange()
  {
    var attribute = new RangeAttribute(1, 100);

    var message = attribute.FormatErrorMessage("Quantity");

    Assert.Contains("Quantity", message);
    Assert.Contains("1", message);
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
    var attribute = new RangeAttribute(1, 100)
    {
      ErrorMessage = "Custom: {0} must be {1} to {2}"
    };

    var message = attribute.FormatErrorMessage("Amount");

    Assert.Equal("Custom: Amount must be 1 to 100", message);
  }

  #endregion
}
