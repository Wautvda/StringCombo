namespace StringCombo.File;

internal class FileReader : IFileReader
{
    public IEnumerable<string> GetRecordsFromFile(string filePath)
    {
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