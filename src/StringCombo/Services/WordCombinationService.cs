using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StringCombo.File;
using StringCombo.Models;

namespace StringCombo.Services;

public class WordCombinationService
{
    private readonly ILogger<WordCombinationService> _logger;
    private readonly IFileReader _fileReader;
    private readonly CommandOptions _commandOptions;

    public WordCombinationService(
        ILogger<WordCombinationService> logger
        , IFileReader fileReader
        , IOptions<CommandOptions> commandOptions
    )
    {
        _logger = logger;
        _fileReader = fileReader;
        _commandOptions = commandOptions.Value;
    }

    public Task GetCombinationsAsync(CancellationToken stoppingToken = default)
    {
        var inputCollection = _fileReader.GetRecordsFromFile(_commandOptions.Path).ToList();
        return Task.CompletedTask;
    }
}