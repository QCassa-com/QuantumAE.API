using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: RequiredIfEmptyAttribute unit tesztek
/// <br />
/// en: RequiredIfEmptyAttribute unit tests
/// </summary>
public class TRequiredIfEmptyAttributeTests
{
  #region Both Filled Tests

  /// <summary>
  /// hu: Teszt: Mindkét mező kitöltve - sikeres
  /// <br />
  /// en: Test: Both fields filled - passes
  /// </summary>
  [Fact]
  public void WhenBothFieldsFilled_ShouldPass()
  {
    var model = new TestModel { Name = "Product", Barcode = "12345" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region One Filled Tests

  /// <summary>
  /// hu: Teszt: Csak az első mező kitöltve - sikeres
  /// <br />
  /// en: Test: Only first field filled - passes
  /// </summary>
  [Fact]
  public void WhenOnlyCurrentFieldFilled_ShouldPass()
  {
    var model = new TestModel { Name = "Product", Barcode = null };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Csak a másik mező kitöltve - sikeres
  /// <br />
  /// en: Test: Only other field filled - passes
  /// </summary>
  [Fact]
  public void WhenOnlyOtherFieldFilled_ShouldPass()
  {
    var model = new TestModel { Name = null, Barcode = "12345" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Első mező üres string, másik kitöltve - sikeres
  /// <br />
  /// en: Test: First field empty string, other filled - passes
  /// </summary>
  [Fact]
  public void WhenCurrentIsEmptyString_AndOtherFilled_ShouldPass()
  {
    var model = new TestModel { Name = "", Barcode = "12345" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Both Empty Tests

  /// <summary>
  /// hu: Teszt: Mindkét mező null - sikertelen
  /// <br />
  /// en: Test: Both fields null - fails
  /// </summary>
  [Fact]
  public void WhenBothFieldsNull_ShouldFail()
  {
    var model = new TestModel { Name = null, Barcode = null };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
    Assert.Contains(nameof(TestModel.Name), result.MemberNames);
  }

  /// <summary>
  /// hu: Teszt: Mindkét mező üres string - sikertelen
  /// <br />
  /// en: Test: Both fields empty string - fails
  /// </summary>
  [Fact]
  public void WhenBothFieldsEmptyString_ShouldFail()
  {
    var model = new TestModel { Name = "", Barcode = "" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Mindkét mező whitespace - sikertelen
  /// <br />
  /// en: Test: Both fields whitespace - fails
  /// </summary>
  [Fact]
  public void WhenBothFieldsWhitespace_ShouldFail()
  {
    var model = new TestModel { Name = "   ", Barcode = "  \t  " };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Első null, másik üres string - sikertelen
  /// <br />
  /// en: Test: First null, other empty string - fails
  /// </summary>
  [Fact]
  public void WhenFirstNull_AndOtherEmptyString_ShouldFail()
  {
    var model = new TestModel { Name = null, Barcode = "" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Property Not Found Tests

  /// <summary>
  /// hu: Teszt: Nem létező property - hibát ad
  /// <br />
  /// en: Test: Non-existent property - returns error
  /// </summary>
  [Fact]
  public void WhenOtherPropertyNotFound_ShouldFail()
  {
    var model = new TestModel { Name = "Product", Barcode = "12345" };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute("NonExistentProperty");

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
    Assert.Contains("NonExistentProperty", result.ErrorMessage);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Hibaüzenet tartalmazza mindkét mező nevét
  /// <br />
  /// en: Test: Error message contains both field names
  /// </summary>
  [Fact]
  public void ErrorMessage_ShouldContainBothFieldNames()
  {
    var model = new TestModel { Name = null, Barcode = null };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode));

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotNull(result);
    Assert.Contains("Name", result.ErrorMessage);
    Assert.Contains("Barcode", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var model = new TestModel { Name = null, Barcode = null };
    var context = CreateValidationContext(model, nameof(TestModel.Name));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModel.Barcode))
    {
      ErrorMessage = "Custom: {0} or {1} required"
    };

    var result = attribute.GetValidationResult(model.Name, context);

    Assert.NotNull(result);
    Assert.Equal("Custom: Name or Barcode required", result.ErrorMessage);
  }

  #endregion

  #region OtherProperty Property Tests

  /// <summary>
  /// hu: Teszt: OtherProperty property visszaadja a konstruktorban megadott értéket
  /// <br />
  /// en: Test: OtherProperty returns the value specified in constructor
  /// </summary>
  [Fact]
  public void OtherProperty_ShouldReturnConstructorValue()
  {
    var attribute = new RequiredIfEmptyAttribute("TestProperty");

    Assert.Equal("TestProperty", attribute.OtherProperty);
  }

  #endregion

  #region Non-String Type Tests

  /// <summary>
  /// hu: Teszt: Nem-string típusú mező null esetén
  /// <br />
  /// en: Test: Non-string field when null
  /// </summary>
  [Fact]
  public void WhenNonStringFieldIsNull_ShouldBeConsideredEmpty()
  {
    var model = new TestModelWithInt { Value = null, OtherValue = null };
    var context = CreateValidationContext(model, nameof(TestModelWithInt.Value));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModelWithInt.OtherValue));

    var result = attribute.GetValidationResult(model.Value, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Nem-string típusú mező kitöltve
  /// <br />
  /// en: Test: Non-string field when filled
  /// </summary>
  [Fact]
  public void WhenNonStringFieldHasValue_ShouldNotBeConsideredEmpty()
  {
    var model = new TestModelWithInt { Value = null, OtherValue = 42 };
    var context = CreateValidationContext(model, nameof(TestModelWithInt.Value));
    var attribute = new RequiredIfEmptyAttribute(nameof(TestModelWithInt.OtherValue));

    var result = attribute.GetValidationResult(model.Value, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Helper Methods and Classes

  private static ValidationContext CreateValidationContext<T>(T AInstance, string AMemberName) where T : notnull
  {
    return new ValidationContext(AInstance)
    {
      MemberName = AMemberName,
      DisplayName = AMemberName
    };
  }

  private class TestModel
  {
    public string? Name { get; set; }
    public string? Barcode { get; set; }
  }

  private class TestModelWithInt
  {
    public string? Value { get; set; }
    public int? OtherValue { get; set; }
  }

  #endregion
}
