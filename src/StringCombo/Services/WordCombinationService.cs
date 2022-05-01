using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StringCombo.File;
using StringCombo.Models;
using StringCombo.Provider;

namespace StringCombo.Services;

public class WordCombinationService
{
    private readonly ILogger<WordCombinationService> _logger;
    private readonly IFileReader _fileReader;
    private readonly CommandOptions _commandOptions;
    private readonly ICombinableListProvider _combinableListProvider;
    private readonly IFileWriter _fileWriter;

    public WordCombinationService(
        ILogger<WordCombinationService> logger
        , IFileReader fileReader
        , IOptions<CommandOptions> commandOptions
        , ICombinableListProvider combinableListProvider
        , IFileWriter fileWriter
    )
    {
        _logger = logger;
        _fileReader = fileReader;
        _commandOptions = commandOptions.Value;
        _combinableListProvider = combinableListProvider;
        _fileWriter = fileWriter;
    }

    public Task GetCombinationsAsync(CancellationToken cancellationToken = default)
    {
        var inputCollection = _fileReader.GetRecordsFromFile(_commandOptions.Path).ToList();
        var combinationResult = _combinableListProvider.GetJoinableStrings(inputCollection, cancellationToken);
        _fileWriter.WriteToFile(_commandOptions.OutputFolder, combinationResult);
        return Task.CompletedTask;
    }
}