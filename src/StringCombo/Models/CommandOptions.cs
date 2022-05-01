using CommandLine;
using Microsoft.Extensions.Options;

namespace StringCombo.Models;

public class CommandOptions
{
    [Value(index: 0, Required = false, HelpText = "File location with input text file.", Default = "./Data/input.txt")]
    public string Path { get; }
    
    [Option(shortName: 'l', longName: "length", Required = false, HelpText = "The length of the combined strings.", Default = 6)]
    public int Length { get; }
    
    [Option(longName: "write-to-console", Required = false, HelpText = "Writes the acquired results to the console", Default = true)]
    public bool WriteOutputToConsole { get; }
    
    public CommandOptions(): this("./Data/input.txt", 6, true)
    {
        
    }
    
    public CommandOptions(string path, int length, bool writeOutputToConsole)
    {
        Path = path;
        Length = length;
        WriteOutputToConsole = writeOutputToConsole;
    }
}