using CommandLine;

namespace StringCombo.Models;

public class CommandOptions
{
    [Value(index: 0, Required = false, HelpText = "File location with input text file.", Default = "./Data/input.txt")]
    public string Path { get; }
    
    [Option(shortName: 'l', longName: "length", Required = false, HelpText = "The length of the combined strings", Default = 6)]
    public int Length { get; }

    public CommandOptions(): this("./Data/input.txt", 6)
    {
        
    }
    
    public CommandOptions(string path, int length)
    {
        Path = path;
        Length = length;
    }
}