namespace StringCombo.File;

public interface IFileReader
{
    IEnumerable<string> GetRecordsFromFile(string filePath);
}