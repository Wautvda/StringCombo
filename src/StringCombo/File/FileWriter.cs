using System.Text;
using Microsoft.Extensions.Logging;
using StringCombo.Models;

namespace StringCombo.File;

public class FileWriter : IFileWriter
{
    private readonly ILogger<FileWriter> _logger;
    public FileWriter(ILogger<FileWriter> logger)
    {
        _logger = logger;
    }
    public void WriteToFile(string? folder, IEnumerable<JoinableString> values)
    {
        if (folder == null)
        {
            _logger.LogError("Output folder is required to create and output file, but none provided. No output file will be created");
            return;
        }
        
        if(!Directory.Exists(folder))
        {
            _logger.LogDebug("Creating folder {Folder}", folder);
            Directory.CreateDirectory(folder);
        }
        
        FileStream? stream = null;
        var filePath = $"{folder}/output.{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.txt";
        try
        {
            stream = new FileStream(filePath, FileMode.OpenOrCreate);
            using var writer = new StreamWriter(stream, Encoding.UTF8);
            foreach (var value in values)
            {
                writer.WriteLine(value.GetOutput());
            }
            _logger.LogInformation("Result written to '{FilePath}'", filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occured");
        }
        finally
        {
            stream?.Dispose();
        }  
    }
}