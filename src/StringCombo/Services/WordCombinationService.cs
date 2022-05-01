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
    }

    public Task GetCombinationsAsync(CancellationToken stoppingToken = default)
    {
        var inputCollection = _fileReader.GetRecordsFromFile(_commandOptions.Path).ToList();
        _combinableListProvider.GetJoinableStrings(inputCollection);
        return Task.CompletedTask;
    }
}