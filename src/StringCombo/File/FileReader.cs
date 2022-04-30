using Microsoft.Extensions.Logging;

namespace StringCombo.File;

internal class FileReader : IFileReader
{
    private readonly ILogger<FileReader> _logger;
    public FileReader(ILogger<FileReader> logger)
    {
        _logger = logger;
    }
    
    public IEnumerable<string> GetRecordsFromFile(string filePath)
    {
        _logger.LogDebug("Reading from file location '{FileLocation}'", filePath);
        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        using var fileStream = new StreamReader(filePath);
        while (fileStream.ReadLine() is {} line)
        {
            yield return line;
        }
    }
}