using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: MinAttribute unit tesztek
/// <br />
/// en: MinAttribute unit tests
/// </summary>
public class TMinAttributeTests
{
  #region Inclusive (>=) Tests

  /// <summary>
  /// hu: Teszt: Az érték egyenlő a minimummal - inkluzív módban sikeres
  /// <br />
  /// en: Test: Value equals minimum - passes in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 3)]
  [InlineData(0, 0)]
  [InlineData(-5, -5)]
  public void Inclusive_WhenValueEqualsMinimum_ShouldPass(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték nagyobb mint a minimum - inkluzív módban sikeres
  /// <br />
  /// en: Test: Value greater than minimum - passes in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 4)]
  [InlineData(3, 100)]
  [InlineData(-10, -5)]
  [InlineData(0, 1)]
  public void Inclusive_WhenValueGreaterThanMinimum_ShouldPass(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték kisebb mint a minimum - inkluzív módban sikertelen
  /// <br />
  /// en: Test: Value less than minimum - fails in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 2)]
  [InlineData(3, 0)]
  [InlineData(3, -1)]
  [InlineData(0, -1)]
  [InlineData(-5, -10)]
  public void Inclusive_WhenValueLessThanMinimum_ShouldFail(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
    Assert.Contains("TestProperty", result.MemberNames);
  }

  #endregion

  #region Exclusive (>) Tests

  /// <summary>
  /// hu: Teszt: Az érték egyenlő a minimummal - exkluzív módban sikertelen
  /// <br />
  /// en: Test: Value equals minimum - fails in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 3)]
  [InlineData(0, 0)]
  [InlineData(-5, -5)]
  public void Exclusive_WhenValueEqualsMinimum_ShouldFail(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum) { Exclusive = true };
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
  }

  /// <summary>
  /// hu: Teszt: Az érték nagyobb mint a minimum - exkluzív módban sikeres
  /// <br />
  /// en: Test: Value greater than minimum - passes in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 4)]
  [InlineData(3, 100)]
  [InlineData(-10, -9)]
  [InlineData(0, 1)]
  public void Exclusive_WhenValueGreaterThanMinimum_ShouldPass(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum) { Exclusive = true };
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték kisebb mint a minimum - exkluzív módban sikertelen
  /// <br />
  /// en: Test: Value less than minimum - fails in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(3, 2)]
  [InlineData(0, -1)]
  public void Exclusive_WhenValueLessThanMinimum_ShouldFail(int AMinimum, int AValue)
  {
    var attribute = new MinAttribute(AMinimum) { Exclusive = true };
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Null Value Tests

  /// <summary>
  /// hu: Teszt: Null érték mindig sikeres (opcionális mező)
  /// <br />
  /// en: Test: Null value always passes (optional field)
  /// </summary>
  [Fact]
  public void WhenValueIsNull_ShouldPass()
  {
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext(null);

    var result = attribute.GetValidationResult(null, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Null érték exkluzív módban is sikeres
  /// <br />
  /// en: Test: Null value passes in exclusive mode too
  /// </summary>
  [Fact]
  public void Exclusive_WhenValueIsNull_ShouldPass()
  {
    var attribute = new MinAttribute(3) { Exclusive = true };
    var context = CreateValidationContext(null);

    var result = attribute.GetValidationResult(null, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Different Numeric Types Tests

  /// <summary>
  /// hu: Teszt: Long típusú érték validálása
  /// <br />
  /// en: Test: Validation of long type value
  /// </summary>
  [Theory]
  [InlineData(100L, true)]
  [InlineData(3L, true)]
  [InlineData(2L, false)]
  public void WithLongValue_ShouldValidateCorrectly(long AValue, bool AExpectedValid)
  {
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    if (AExpectedValid)
      Assert.Equal(ValidationResult.Success, result);
    else
      Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Decimal típusú érték validálása
  /// <br />
  /// en: Test: Validation of decimal type value
  /// </summary>
  [Theory]
  [InlineData(3.5, true)]
  [InlineData(3.0, true)]
  [InlineData(2.99, false)]
  public void WithDecimalValue_ShouldValidateCorrectly(double ADoubleValue, bool AExpectedValid)
  {
    var value = (decimal)ADoubleValue;
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext(value);

    var result = attribute.GetValidationResult(value, context);

    if (AExpectedValid)
      Assert.Equal(ValidationResult.Success, result);
    else
      Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Double típusú érték validálása
  /// <br />
  /// en: Test: Validation of double type value
  /// </summary>
  [Theory]
  [InlineData(3.5, true)]
  [InlineData(3.0, true)]
  [InlineData(2.999, false)]
  public void WithDoubleValue_ShouldValidateCorrectly(double AValue, bool AExpectedValid)
  {
    var attribute = new MinAttribute(3.0);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    if (AExpectedValid)
      Assert.Equal(ValidationResult.Success, result);
    else
      Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Float típusú érték validálása
  /// <br />
  /// en: Test: Validation of float type value
  /// </summary>
  [Theory]
  [InlineData(3.5f, true)]
  [InlineData(3.0f, true)]
  [InlineData(2.9f, false)]
  public void WithFloatValue_ShouldValidateCorrectly(float AValue, bool AExpectedValid)
  {
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    if (AExpectedValid)
      Assert.Equal(ValidationResult.Success, result);
    else
      Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Non-Numeric Type Tests

  /// <summary>
  /// hu: Teszt: String típusú értéknél hibát ad vissza
  /// <br />
  /// en: Test: Returns error for string type value
  /// </summary>
  [Fact]
  public void WithStringValue_ShouldFail()
  {
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext("hello");

    var result = attribute.GetValidationResult("hello", context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Boolean típusú értéknél hibát ad vissza
  /// <br />
  /// en: Test: Returns error for boolean type value
  /// </summary>
  [Fact]
  public void WithBooleanValue_ShouldFail()
  {
    var attribute = new MinAttribute(3);
    var context = CreateValidationContext(true);

    var result = attribute.GetValidationResult(true, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Constructor Tests

  /// <summary>
  /// hu: Teszt: Int konstruktor helyesen állítja be a Minimum értéket
  /// <br />
  /// en: Test: Int constructor correctly sets the Minimum value
  /// </summary>
  [Fact]
  public void IntConstructor_ShouldSetMinimum()
  {
    var attribute = new MinAttribute(42);

    Assert.Equal(42.0, attribute.Minimum);
  }

  /// <summary>
  /// hu: Teszt: Double konstruktor helyesen állítja be a Minimum értéket
  /// <br />
  /// en: Test: Double constructor correctly sets the Minimum value
  /// </summary>
  [Fact]
  public void DoubleConstructor_ShouldSetMinimum()
  {
    var attribute = new MinAttribute(3.14);

    Assert.Equal(3.14, attribute.Minimum);
  }

  /// <summary>
  /// hu: Teszt: Exclusive alapértelmezetten false
  /// <br />
  /// en: Test: Exclusive defaults to false
  /// </summary>
  [Fact]
  public void Exclusive_ShouldDefaultToFalse()
  {
    var attribute = new MinAttribute(3);

    Assert.False(attribute.Exclusive);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Lokalizált hibaüzenet inkluzív módban
  /// <br />
  /// en: Test: Localized error message in inclusive mode
  /// </summary>
  [Fact]
  public void Inclusive_ErrorMessage_ShouldContainFieldNameAndMinimum()
  {
    var attribute = new MinAttribute(5);
    var context = CreateValidationContext(3, "TestField");

    var result = attribute.GetValidationResult(3, context);

    Assert.NotNull(result);
    Assert.Contains("TestField", result.ErrorMessage);
    Assert.Contains("5", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Lokalizált hibaüzenet exkluzív módban
  /// <br />
  /// en: Test: Localized error message in exclusive mode
  /// </summary>
  [Fact]
  public void Exclusive_ErrorMessage_ShouldContainFieldNameAndMinimum()
  {
    var attribute = new MinAttribute(5) { Exclusive = true };
    var context = CreateValidationContext(5, "TestField");

    var result = attribute.GetValidationResult(5, context);

    Assert.NotNull(result);
    Assert.Contains("TestField", result.ErrorMessage);
    Assert.Contains("5", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new MinAttribute(5) { ErrorMessage = "Custom: {0} must be >= {1}" };
    var context = CreateValidationContext(3, "MyField");

    var result = attribute.GetValidationResult(3, context);

    Assert.NotNull(result);
    Assert.Equal("Custom: MyField must be >= 5", result.ErrorMessage);
  }

  #endregion

  #region Helper Methods

  private static ValidationContext CreateValidationContext(object? AValue, string AMemberName = "TestProperty")
  {
    var instance = new TestModel { TestProperty = AValue };
    return new ValidationContext(instance)
    {
      MemberName = AMemberName,
      DisplayName = AMemberName
    };
  }

  private class TestModel
  {
    public object? TestProperty { get; set; }
  }

  #endregion
}
