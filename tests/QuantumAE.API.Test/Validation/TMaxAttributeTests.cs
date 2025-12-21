using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: MaxAttribute unit tesztek
/// <br />
/// en: MaxAttribute unit tests
/// </summary>
public class TMaxAttributeTests
{
  #region Inclusive (<=) Tests

  /// <summary>
  /// hu: Teszt: Az érték egyenlő a maximummal - inkluzív módban sikeres
  /// <br />
  /// en: Test: Value equals maximum - passes in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 100)]
  [InlineData(0, 0)]
  [InlineData(-5, -5)]
  public void Inclusive_WhenValueEqualsMaximum_ShouldPass(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték kisebb mint a maximum - inkluzív módban sikeres
  /// <br />
  /// en: Test: Value less than maximum - passes in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 99)]
  [InlineData(100, 0)]
  [InlineData(100, -50)]
  [InlineData(0, -1)]
  [InlineData(-5, -10)]
  public void Inclusive_WhenValueLessThanMaximum_ShouldPass(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték nagyobb mint a maximum - inkluzív módban sikertelen
  /// <br />
  /// en: Test: Value greater than maximum - fails in inclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 101)]
  [InlineData(100, 200)]
  [InlineData(0, 1)]
  [InlineData(-5, -4)]
  [InlineData(-5, 0)]
  public void Inclusive_WhenValueGreaterThanMaximum_ShouldFail(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum);
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
    Assert.Contains("TestProperty", result.MemberNames);
  }

  #endregion

  #region Exclusive (<) Tests

  /// <summary>
  /// hu: Teszt: Az érték egyenlő a maximummal - exkluzív módban sikertelen
  /// <br />
  /// en: Test: Value equals maximum - fails in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 100)]
  [InlineData(0, 0)]
  [InlineData(-5, -5)]
  public void Exclusive_WhenValueEqualsMaximum_ShouldFail(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum) { Exclusive = true };
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
  }

  /// <summary>
  /// hu: Teszt: Az érték kisebb mint a maximum - exkluzív módban sikeres
  /// <br />
  /// en: Test: Value less than maximum - passes in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 99)]
  [InlineData(100, 0)]
  [InlineData(0, -1)]
  [InlineData(-5, -6)]
  public void Exclusive_WhenValueLessThanMaximum_ShouldPass(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum) { Exclusive = true };
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Az érték nagyobb mint a maximum - exkluzív módban sikertelen
  /// <br />
  /// en: Test: Value greater than maximum - fails in exclusive mode
  /// </summary>
  [Theory]
  [InlineData(100, 101)]
  [InlineData(0, 1)]
  public void Exclusive_WhenValueGreaterThanMaximum_ShouldFail(int AMaximum, int AValue)
  {
    var attribute = new MaxAttribute(AMaximum) { Exclusive = true };
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
    var attribute = new MaxAttribute(100);
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
    var attribute = new MaxAttribute(100) { Exclusive = true };
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
  [InlineData(50L, true)]
  [InlineData(100L, true)]
  [InlineData(101L, false)]
  public void WithLongValue_ShouldValidateCorrectly(long AValue, bool AExpectedValid)
  {
    var attribute = new MaxAttribute(100);
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
  [InlineData(99.99, true)]
  [InlineData(100.0, true)]
  [InlineData(100.01, false)]
  public void WithDecimalValue_ShouldValidateCorrectly(double ADoubleValue, bool AExpectedValid)
  {
    var value = (decimal)ADoubleValue;
    var attribute = new MaxAttribute(100);
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
  [InlineData(99.5, true)]
  [InlineData(100.0, true)]
  [InlineData(100.001, false)]
  public void WithDoubleValue_ShouldValidateCorrectly(double AValue, bool AExpectedValid)
  {
    var attribute = new MaxAttribute(100.0);
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
  [InlineData(99.5f, true)]
  [InlineData(100.0f, true)]
  [InlineData(100.1f, false)]
  public void WithFloatValue_ShouldValidateCorrectly(float AValue, bool AExpectedValid)
  {
    var attribute = new MaxAttribute(100);
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
    var attribute = new MaxAttribute(100);
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
    var attribute = new MaxAttribute(100);
    var context = CreateValidationContext(true);

    var result = attribute.GetValidationResult(true, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Constructor Tests

  /// <summary>
  /// hu: Teszt: Int konstruktor helyesen állítja be a Maximum értéket
  /// <br />
  /// en: Test: Int constructor correctly sets the Maximum value
  /// </summary>
  [Fact]
  public void IntConstructor_ShouldSetMaximum()
  {
    var attribute = new MaxAttribute(42);

    Assert.Equal(42.0, attribute.Maximum);
  }

  /// <summary>
  /// hu: Teszt: Double konstruktor helyesen állítja be a Maximum értéket
  /// <br />
  /// en: Test: Double constructor correctly sets the Maximum value
  /// </summary>
  [Fact]
  public void DoubleConstructor_ShouldSetMaximum()
  {
    var attribute = new MaxAttribute(99.99);

    Assert.Equal(99.99, attribute.Maximum);
  }

  /// <summary>
  /// hu: Teszt: Exclusive alapértelmezetten false
  /// <br />
  /// en: Test: Exclusive defaults to false
  /// </summary>
  [Fact]
  public void Exclusive_ShouldDefaultToFalse()
  {
    var attribute = new MaxAttribute(100);

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
  public void Inclusive_ErrorMessage_ShouldContainFieldNameAndMaximum()
  {
    var attribute = new MaxAttribute(100);
    var context = CreateValidationContext(150, "TestField");

    var result = attribute.GetValidationResult(150, context);

    Assert.NotNull(result);
    Assert.Contains("TestField", result.ErrorMessage);
    Assert.Contains("100", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Lokalizált hibaüzenet exkluzív módban
  /// <br />
  /// en: Test: Localized error message in exclusive mode
  /// </summary>
  [Fact]
  public void Exclusive_ErrorMessage_ShouldContainFieldNameAndMaximum()
  {
    var attribute = new MaxAttribute(100) { Exclusive = true };
    var context = CreateValidationContext(100, "TestField");

    var result = attribute.GetValidationResult(100, context);

    Assert.NotNull(result);
    Assert.Contains("TestField", result.ErrorMessage);
    Assert.Contains("100", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new MaxAttribute(100) { ErrorMessage = "Custom: {0} must be <= {1}" };
    var context = CreateValidationContext(150, "MyField");

    var result = attribute.GetValidationResult(150, context);

    Assert.NotNull(result);
    Assert.Equal("Custom: MyField must be <= 100", result.ErrorMessage);
  }

  #endregion

  #region Combined Min/Max Tests

  /// <summary>
  /// hu: Teszt: Min és Max attribútumok együttes használata
  /// <br />
  /// en: Test: Using Min and Max attributes together
  /// </summary>
  [Theory]
  [InlineData(0, false)]   // below min
  [InlineData(1, true)]    // at min (inclusive)
  [InlineData(50, true)]   // in range
  [InlineData(100, true)]  // at max (inclusive)
  [InlineData(101, false)] // above max
  public void MinAndMax_CombinedValidation_ShouldWorkCorrectly(int AValue, bool AExpectedValid)
  {
    var minAttribute = new MinAttribute(1);
    var maxAttribute = new MaxAttribute(100);
    var context = CreateValidationContext(AValue);

    var minResult = minAttribute.GetValidationResult(AValue, context);
    var maxResult = maxAttribute.GetValidationResult(AValue, context);

    var isValid = minResult == ValidationResult.Success && maxResult == ValidationResult.Success;

    Assert.Equal(AExpectedValid, isValid);
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
