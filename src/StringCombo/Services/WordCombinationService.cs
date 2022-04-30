using Microsoft.Extensions.Logging;
using StringCombo.Models;

namespace StringCombo.Services;

public class WordCombinationService
{
    private readonly ILogger<WordCombinationService> _logger;

    public WordCombinationService(ILogger<WordCombinationService> logger)
    {
        _logger = logger;
    }
    
    public Task GetCombinationsAsync(CommandOptions commandOptions, CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("Doing something");
        return Task.CompletedTask;
    }
}