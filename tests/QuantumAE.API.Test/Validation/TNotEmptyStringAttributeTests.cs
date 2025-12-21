using System.ComponentModel.DataAnnotations;
using QuantumAE.Validation;

namespace QuantumAE.API.Test.Validation;

/// <summary>
/// hu: NotEmptyStringAttribute unit tesztek
/// <br />
/// en: NotEmptyStringAttribute unit tests
/// </summary>
public class TNotEmptyStringAttributeTests
{
  #region Valid String Tests

  /// <summary>
  /// hu: Teszt: Nem üres string - sikeres
  /// <br />
  /// en: Test: Non-empty string - passes
  /// </summary>
  [Theory]
  [InlineData("hello")]
  [InlineData("a")]
  [InlineData("Hello World")]
  [InlineData("  text  ")]
  public void WhenStringIsNotEmpty_ShouldPass(string AValue)
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Empty String Tests

  /// <summary>
  /// hu: Teszt: Üres string - sikertelen
  /// <br />
  /// en: Test: Empty string - fails
  /// </summary>
  [Fact]
  public void WhenStringIsEmpty_ShouldFail()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(string.Empty);

    var result = attribute.GetValidationResult(string.Empty, context);

    Assert.NotEqual(ValidationResult.Success, result);
    Assert.NotNull(result);
    Assert.Contains("TestProperty", result.MemberNames);
  }

  /// <summary>
  /// hu: Teszt: Csak szóközöket tartalmazó string - sikertelen
  /// <br />
  /// en: Test: Whitespace only string - fails
  /// </summary>
  [Theory]
  [InlineData(" ")]
  [InlineData("   ")]
  [InlineData("\t")]
  [InlineData("\n")]
  [InlineData("  \t\n  ")]
  public void WhenStringIsWhitespaceOnly_ShouldFail(string AValue)
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(AValue);

    var result = attribute.GetValidationResult(AValue, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Null Value Tests

  /// <summary>
  /// hu: Teszt: Null érték elfogadott (különbözik a Required-tól)
  /// <br />
  /// en: Test: Null value accepted (differs from Required)
  /// </summary>
  [Fact]
  public void WhenValueIsNull_ShouldPass()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(null);

    var result = attribute.GetValidationResult(null, context);

    Assert.Equal(ValidationResult.Success, result);
  }

  #endregion

  #region Non-String Type Tests

  /// <summary>
  /// hu: Teszt: Int típusú érték - sikertelen
  /// <br />
  /// en: Test: Int value - fails
  /// </summary>
  [Fact]
  public void WhenValueIsInt_ShouldFail()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(42);

    var result = attribute.GetValidationResult(42, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  /// <summary>
  /// hu: Teszt: Object típusú érték - sikertelen
  /// <br />
  /// en: Test: Object value - fails
  /// </summary>
  [Fact]
  public void WhenValueIsObject_ShouldFail()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(new object());

    var result = attribute.GetValidationResult(new object(), context);

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
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(true);

    var result = attribute.GetValidationResult(true, context);

    Assert.NotEqual(ValidationResult.Success, result);
  }

  #endregion

  #region Error Message Tests

  /// <summary>
  /// hu: Teszt: Hibaüzenet tartalmazza a mező nevét
  /// <br />
  /// en: Test: Error message contains field name
  /// </summary>
  [Fact]
  public void ErrorMessage_ShouldContainFieldName()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext("", "CustomerName");

    var result = attribute.GetValidationResult("", context);

    Assert.NotNull(result);
    Assert.Contains("CustomerName", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Egyedi hibaüzenet használata
  /// <br />
  /// en: Test: Custom error message usage
  /// </summary>
  [Fact]
  public void CustomErrorMessage_ShouldBeUsed()
  {
    var attribute = new NotEmptyStringAttribute { ErrorMessage = "Custom: {0} cannot be empty" };
    var context = CreateValidationContext("", "MyField");

    var result = attribute.GetValidationResult("", context);

    Assert.NotNull(result);
    Assert.Equal("Custom: MyField cannot be empty", result.ErrorMessage);
  }

  /// <summary>
  /// hu: Teszt: Hibaüzenet nem-string típusnál
  /// <br />
  /// en: Test: Error message for non-string type
  /// </summary>
  [Fact]
  public void ErrorMessage_ForNonString_ShouldIndicateStringRequired()
  {
    var attribute = new NotEmptyStringAttribute();
    var context = CreateValidationContext(123, "Value");

    var result = attribute.GetValidationResult(123, context);

    Assert.NotNull(result);
    Assert.Contains("string", result.ErrorMessage);
  }

  #endregion

  #region Difference from Required Attribute Tests

  /// <summary>
  /// hu: Teszt: NotEmptyString elfogadja a null-t, Required nem
  /// <br />
  /// en: Test: NotEmptyString accepts null, Required does not
  /// </summary>
  [Fact]
  public void DifferenceFromRequired_NullHandling()
  {
    var notEmptyAttr = new NotEmptyStringAttribute();
    var requiredAttr = new QuantumAE.Validation.RequiredAttribute();

    var notEmptyResult = notEmptyAttr.IsValid(null);
    var requiredResult = requiredAttr.IsValid(null);

    Assert.True(notEmptyResult);  // NotEmptyString accepts null
    Assert.False(requiredResult); // Required rejects null
  }

  /// <summary>
  /// hu: Teszt: Mindkét attribútum elutasítja az üres stringet
  /// <br />
  /// en: Test: Both attributes reject empty string
  /// </summary>
  [Fact]
  public void BothRejectEmptyString()
  {
    var notEmptyAttr = new NotEmptyStringAttribute();
    var requiredAttr = new QuantumAE.Validation.RequiredAttribute();
    var context = CreateValidationContext("");

    var notEmptyResult = notEmptyAttr.GetValidationResult("", context);
    var requiredResult = requiredAttr.IsValid("");

    Assert.NotEqual(ValidationResult.Success, notEmptyResult);
    Assert.False(requiredResult);
  }

  #endregion

  #region Combined with Required Tests

  /// <summary>
  /// hu: Teszt: Required + NotEmptyString együttes használata - null elutasítva
  /// <br />
  /// en: Test: Required + NotEmptyString combined - null rejected
  /// </summary>
  [Fact]
  public void CombinedWithRequired_NullRejected()
  {
    var requiredAttr = new QuantumAE.Validation.RequiredAttribute();
    var notEmptyAttr = new NotEmptyStringAttribute();

    // Először Required ellenőriz
    var requiredValid = requiredAttr.IsValid(null);

    Assert.False(requiredValid); // Required catches null first
  }

  /// <summary>
  /// hu: Teszt: Required + NotEmptyString együttes használata - üres string elutasítva
  /// <br />
  /// en: Test: Required + NotEmptyString combined - empty string rejected
  /// </summary>
  [Fact]
  public void CombinedWithRequired_EmptyStringRejected()
  {
    var requiredAttr = new QuantumAE.Validation.RequiredAttribute();
    var notEmptyAttr = new NotEmptyStringAttribute();
    var context = CreateValidationContext("");

    var requiredValid = requiredAttr.IsValid("");
    var notEmptyResult = notEmptyAttr.GetValidationResult("", context);

    Assert.False(requiredValid);
    Assert.NotEqual(ValidationResult.Success, notEmptyResult);
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
