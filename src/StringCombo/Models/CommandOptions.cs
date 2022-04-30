using CommandLine;

namespace StringCombo.Models;

public class CommandOptions
{
    [Value(index: 0, Required = true, HelpText = "File location with input text file.")]
    public string Path { get; set; }
}