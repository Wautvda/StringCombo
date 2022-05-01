using CommandLine;

namespace StringCombo.Models;

public class CommandOptions
{
    [Value(index: 0, Required = true, HelpText = "File location with input text file.")]
    public string? Path { get; }

    [Option(shortName: 'l', longName: "length", Required = false, HelpText = "The length of the combined strings.", Default = 6)]
    public int Length { get; }

    [Option(longName: "write-to-console", Required = false, HelpText = "Writes the acquired results to the console", Default = true)]
    public bool WriteOutputToConsole { get; }

    [Option(shortName: 'o', longName: "output-folder", Required = false, HelpText = "Output folder where result files will be written", Default = "./Result/")]
    public string? OutputFolder { get; }

    public CommandOptions()
    {
        Length = 6;
        WriteOutputToConsole = true;
    }

    public CommandOptions(
        string path
        , int length
        , bool writeOutputToConsole
        , string outputFolder
    )
    {
        Path = path;
        Length = length;
        WriteOutputToConsole = writeOutputToConsole;
        OutputFolder = outputFolder;
    }
}