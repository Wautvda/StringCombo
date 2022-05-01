using StringCombo.Exceptions;
using StringCombo.Models;
using StringCombo.UnitTests.Factories;
using StringCombo.Validators;
using Xunit;

namespace StringCombo.UnitTests.Validators;

public class JoinableStringValidatorTests
{
    private readonly IJoinableStringValidator _validator;

    public JoinableStringValidatorTests()
    {
        _validator = new JoinableStringValidator();
    }

    [Theory]
    [InlineData(6)]
    [InlineData(4)]
    [InlineData(10)]
    public void WhenTotalLengthToShortShouldReturnToShortValidationException(int length)
    {
        var testString = new JoinableString(StringFactory.RandomString(length - 1));

        Assert.IsType<ToShortValidationException>(_validator.Validate(testString, length));
    }

    [Theory]
    [InlineData(6)]
    [InlineData(4)]
    [InlineData(10)]
    public void WhenTotalLengthToLongShouldReturnToLongValidationException(int length)
    {
        var testString = new JoinableString(StringFactory.RandomString(length + 1));

        Assert.IsType<ToLongValidationException>(_validator.Validate(testString, length));
    }

    [Theory]
    [InlineData(6)]
    [InlineData(4)]
    [InlineData(10)]
    public void WhenTotalLengthIsExpectedShoulNotReturnValidationError(int length)
    {
        var testString = new JoinableString(StringFactory.RandomString(length));

        Assert.Null(_validator.Validate(testString, length));
    }
}