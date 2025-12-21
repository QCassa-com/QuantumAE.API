using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: PositiveDecimalAttribute unit tesztek
/// <br />
/// en: PositiveDecimalAttribute unit tests
/// </summary>
public class TPositiveDecimalAttributeTests
{
  #region Positive Value Tests (AllowZero = false)

  /// <summary>
  /// hu: Teszt: Pozitív érték - sikeres
  /// <br />
  /// en: Test: Positive value - passes
  /// </summary>
  [Theory]
  [InlineData(1)]
  [InlineData(100)]
  [InlineData(0.01)]
  [InlineData(999999.99)]
  public void WhenValueIsPositive_ShouldPass(double AValue)
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult((decimal)AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Nulla érték - sikertelen (AllowZero = false)
  /// <br />
  /// en: Test: Zero value - fails (AllowZero = false)
  /// </summary>
  [Fact]
  public void WhenValueIsZero_AndAllowZeroFalse_ShouldFail()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(0m);

    var result = attribute.GetValidationResult(0m, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Negatív érték - sikertelen
  /// <br />
  /// en: Test: Negative value - fails
  /// </summary>
  [Theory]
  [InlineData(-1)]
  [InlineData(-0.01)]
  [InlineData(-100)]
  public void WhenValueIsNegative_ShouldFail(double AValue)
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext((decimal)AValue);

    var result = attribute.GetValidationResult((decimal)AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region AllowZero = true Tests

  /// <summary>
  /// hu: Teszt: Nulla érték - sikeres (AllowZero = true)
  /// <br />
  /// en: Test: Zero value - passes (AllowZero = true)
  /// </summary>
  [Fact]
  public void WhenValueIsZero_AndAllowZeroTrue_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute(AAllowZero: true);
    var context = CreateValidationContext(0m);

    var result = attribute.GetValidationResult(0m, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Pozitív érték - sikeres (AllowZero = true)
  /// <br />
  /// en: Test: Positive value - passes (AllowZero = true)
  /// </summary>
  [Fact]
  public void WhenValueIsPositive_AndAllowZeroTrue_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute(AAllowZero: true);
    var context = CreateValidationContext(100m);

    var result = attribute.GetValidationResult(100m, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Negatív érték - sikertelen (AllowZero = true)
  /// <br />
  /// en: Test: Negative value - fails (AllowZero = true)
  /// </summary>
  [Fact]
  public void WhenValueIsNegative_AndAllowZeroTrue_ShouldFail()
  {
    var attribute = new PositiveDecimalAttribute(AAllowZero: true);
    var context = CreateValidationContext(-1m);

    var result = attribute.GetValidationResult(-1m, context);

    Assert.NotEqual(ValidationResult.Success, result);
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
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(null);

    var result = attribute.GetValidationResult(null, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Different Numeric Types Tests

  /// <summary>
  /// hu: Teszt: Double típusú pozitív érték - sikeres
  /// <br />
  /// en: Test: Double positive value - passes
  /// </summary>
  [Fact]
  public void WhenDoubleValueIsPositive_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(10.5);

    var result = attribute.GetValidationResult(10.5, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Float típusú pozitív érték - sikeres
  /// <br />
  /// en: Test: Float positive value - passes
  /// </summary>
  [Fact]
  public void WhenFloatValueIsPositive_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(10.5f);

    var result = attribute.GetValidationResult(10.5f, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Int típusú pozitív érték - sikeres
  /// <br />
  /// en: Test: Int positive value - passes
  /// </summary>
  [Fact]
  public void WhenIntValueIsPositive_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(42);

    var result = attribute.GetValidationResult(42, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Long típusú pozitív érték - sikeres
  /// <br />
  /// en: Test: Long positive value - passes
  /// </summary>
  [Fact]
  public void WhenLongValueIsPositive_ShouldPass()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(1000000L);

    var result = attribute.GetValidationResult(1000000L, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Non-Numeric Type Tests

  /// <summary>
  /// hu: Teszt: String típusú érték - sikertelen
  /// <br />
  /// en: Test: String value - fails
  /// </summary>
  [Fact]
  public void WhenValueIsString_ShouldFail()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext("not a number");

    var result = attribute.GetValidationResult("not a number", context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Boolean típusú érték - sikertelen
  /// <br />
  /// en: Test: Boolean value - fails
  /// </summary>
  [Fact]
  public void WhenValueIsBoolean_ShouldFail()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(true);

    var result = attribute.GetValidationResult(true, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Constructor Tests

  /// <summary>
  /// hu: Teszt: AllowZero alapértelmezetten false
  /// <br />
  /// en: Test: AllowZero defaults to false
  /// </summary>
  [Fact]
  public void AllowZero_ShouldDefaultToFalse()
  {
    var attribute = new PositiveDecimalAttribute();

    Assert.False(attribute.AllowZero);
  }

  /// <summary>
  /// hu: Teszt: AllowZero beállítható konstruktorban
  /// <br />
  /// en: Test: AllowZero can be set in constructor
  /// </summary>
  [Fact]
  public void AllowZero_CanBeSetInConstructor()
  {
    var attribute = new PositiveDecimalAttribute(AAllowZero: true);

    Assert.True(attribute.AllowZero);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Hibaüzenet negatív értéknél (AllowZero = false)
  /// <br />
  /// en: Test: Error message for negative value (AllowZero = false)
  /// </summary>
  [Fact]
  public void ErrorMessage_ForNegative_ShouldContainFieldName()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(-1m, "Price");

    var result = attribute.GetValidationResult(-1m, context);

    Assert.NotNull(result);
    Assert.Contains("Price", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Hibaüzenet nulla értéknél (AllowZero = false)
  /// <br />
  /// en: Test: Error message for zero value (AllowZero = false)
  /// </summary>
  [Fact]
  public void ErrorMessage_ForZero_WhenNotAllowed_ShouldContainFieldName()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext(0m, "Amount");

    var result = attribute.GetValidationResult(0m, context);

    Assert.NotNull(result);
    Assert.Contains("Amount", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Hibaüzenet nem-numerikus értéknél
  /// <br />
  /// en: Test: Error message for non-numeric value
  /// </summary>
  [Fact]
  public void ErrorMessage_ForNonNumeric_ShouldContainFieldName()
  {
    var attribute = new PositiveDecimalAttribute();
    var context = CreateValidationContext("text", "Value");

    var result = attribute.GetValidationResult("text", context);

    Assert.NotNull(result);
    Assert.Contains("Value", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new PositiveDecimalAttribute { ErrorMessage = "Custom: {0} must be positive" };
    var context = CreateValidationContext(-1m, "MyField");

    var result = attribute.GetValidationResult(-1m, context);

    Assert.NotNull(result);
    Assert.Equal("Custom: MyField must be positive", result.ErrorMessage);
  }

  #endregion

  #region Helper Methods

  private static ValidationContext CreateValidationContext(object? AValue, string AMemberName = "TestProperty")
  {
    var instance = new TestModel { Value = AValue };
    return new ValidationContext(instance)
    {
      MemberName = AMemberName,
      DisplayName = AMemberName
    };
  }

  private class TestModel
  {
    public object? Value { get; set; }
  }

  #endregion
}
