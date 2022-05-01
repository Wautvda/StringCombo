using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StringCombo.Exceptions;
using StringCombo.Models;
using StringCombo.Validators;

namespace StringCombo.Provider;

public interface ICombinableListProvider
{
    List<JoinableString> GetJoinableStrings(IList<string> values, CancellationToken token = default);
}

public class CombinableListProvider : ICombinableListProvider
{
    private readonly CommandOptions _commandOptions;
    private readonly IJoinableStringValidator _validator;
    private readonly ILogger<CombinableListProvider> _logger;

    public CombinableListProvider(
        IOptions<CommandOptions> commandOptions
        , IJoinableStringValidator validator
        , ILogger<CombinableListProvider> logger
    )
    {
        _commandOptions = commandOptions.Value;
        _validator = validator;
        _logger = logger;
    }

    public List<JoinableString> GetJoinableStrings(IList<string> values, CancellationToken cancellationToken = default)
    {
        var result = new List<JoinableString>();
        for (int i = 0; i < values.Count; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            var value = new JoinableString(values.ElementAt(i));
            switch (_validator.Validate(value, _commandOptions.Length))
            {
                case null:
                    result.Add(value);
                    WriteOutput(value);
                    continue;
                case ToLongValidationException:
                    continue;
                case ToShortValidationException:
                    result.AddRange(Combine(new Dictionary<int, string> { { i, values.ElementAt(i) } }, values, cancellationToken));
                    continue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return result;
    }

    private IEnumerable<JoinableString> Combine(Dictionary<int, string> combined, IList<string> values, CancellationToken cancellationToken)
    {
        var result = new List<JoinableString>();
        for (int i = 0; i < values.Count; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            if (combined.ContainsKey(i))
            {
                continue;
            }

            var value = values.ElementAt(i);
            switch (IsCombinable(combined, value, out var combinedResult))
            {
                case null:
                    result.Add(combinedResult);
                    WriteOutput(combinedResult);
                    continue;
                case ToLongValidationException:
                    continue;
                case ToShortValidationException:
                    combined.Add(i, value);
                    result.AddRange(Combine(combined, values, cancellationToken));
                    continue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return result;
    }

    private ValidationException? IsCombinable(Dictionary<int, string> combined, string value, out JoinableString result)
    {
        var joinable = combined.Select(x => x.Value).ToList();
        joinable.Add(value);
        result = new JoinableString(joinable.ToArray());
        return _validator.Validate(result, _commandOptions.Length);
    }

    private void WriteOutput(JoinableString value)
    {
        if (_commandOptions.WriteOutputToConsole)
        {
            _logger.LogInformation(value.GetOutput());
        }
    }
}