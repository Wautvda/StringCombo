using System.Collections.Generic;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using StringCombo.Exceptions;
using StringCombo.Models;
using StringCombo.Provider;
using StringCombo.Validators;
using Xunit;

namespace StringCombo.UnitTests.Providers;

public class CombinableListProviderTests
{
    private readonly Mock<IJoinableStringValidator> _joinableStringValidatorMock;
    private readonly CombinableListProvider _provider;
    private readonly CommandOptions _options;

    public CombinableListProviderTests()
    {
        var commandOptionsMock = new Mock<IOptions<CommandOptions>>(MockBehavior.Strict);
        _joinableStringValidatorMock = new Mock<IJoinableStringValidator>(MockBehavior.Strict);
        
        _options = new CommandOptions();
        commandOptionsMock
            .Setup(o => o.Value)
            .Returns(_options);
        _provider = new CombinableListProvider(commandOptionsMock.Object, _joinableStringValidatorMock.Object, NullLogger<CombinableListProvider>.Instance);
    }

    [Fact]
    public void WhenListContainsJoinableValuesShouldReturnAllValidCombination()
    {
        var testList = new List<string>
        {
            "abc"
            , "def"
        };

        _joinableStringValidatorMock
            .SetupSequence(m => m.Validate(It.IsAny<JoinableString>(), _options.Length))
            .Returns((ValidationException?)null)
            .Returns(new ToLongValidationException());

        var resultList = _provider.GetJoinableStrings(testList);
        
        var result = Assert.Single(resultList);
        Assert.Equal("abc", result.ToString());
    }

    [Fact]
    public void WhenValueToShortShouldCombineUntillValid()
    {
        var testList = new List<string>
        {
            "abc"
            , "de"
            , "f"
        };

        _joinableStringValidatorMock
            .SetupSequence(m => m.Validate(It.IsAny<JoinableString>(), _options.Length))
            .Returns(new ToShortValidationException())
            .Returns(new ToShortValidationException())
            .Returns((ValidationException?)null)
            .Returns(new ToLongValidationException())
            .Returns(new ToLongValidationException())
            .Returns(new ToLongValidationException());

        var resultList = _provider.GetJoinableStrings(testList);
        
        var result = Assert.Single(resultList);
        Assert.Equal("abcdef", result.ToString());
    }

    [Fact]
    public void WhenNoValuesValidShouldReturnEmptyList()
    {
        var testList = new List<string>
        {
            "abc"
        };

        _joinableStringValidatorMock
            .SetupSequence(m => m.Validate(It.IsAny<JoinableString>(), _options.Length))
            .Returns(new ToLongValidationException());

        var resultList = _provider.GetJoinableStrings(testList);
        
        Assert.Empty(resultList);
    }
}