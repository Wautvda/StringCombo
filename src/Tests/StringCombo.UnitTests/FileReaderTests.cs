using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using StringCombo.File;
using Xunit;

namespace StringCombo.UnitTests;

public class FileReaderTests
{
    private readonly IFileReader _reader;
    public FileReaderTests()
    {
        _reader = new FileReader(NullLogger<FileReader>.Instance);
    }
    
    [Fact]
    public void ShouldThrowWhenFileNotFound()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "NonExistingFile.txt");
        
        var ex = Assert.Throws<FileNotFoundException>(() => { _reader.GetRecordsFromFile(filePath).ToList(); });
        Assert.NotNull(ex);
    }
    
    [Fact]
    public void ShouldReturnValuesWhenFileExists()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "Input-test.txt");

        var result = _reader.GetRecordsFromFile(filePath).ToList();
        Assert.Equal(4, result.Count);
    }
}