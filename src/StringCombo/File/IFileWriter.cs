using StringCombo.Models;

namespace StringCombo.File;

public interface IFileWriter
{
    void WriteToFile(string path, IEnumerable<JoinableString> values);
}