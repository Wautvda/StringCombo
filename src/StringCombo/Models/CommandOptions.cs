using CommandLine;

namespace StringCombo.Models;

public class CommandOptions
{
    [Value(index: 0, Required = true, HelpText = "File location with input text file.", Default = "./Data/input.txt")]
    public string Path { get; }
    
    public CommandOptions(string path)
    {
        Path = path;
    }
}