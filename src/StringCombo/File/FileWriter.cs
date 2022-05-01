using System.Text;
using StringCombo.Models;

namespace StringCombo.File;

public class FileWriter : IFileWriter
{
    public void WriteToFile(string folder, IEnumerable<JoinableString> values)
    {
        if(!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        
        FileStream? stream = null;  
        try  
        {  
            stream = new FileStream($"{folder}/output.{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.txt", FileMode.OpenOrCreate);  
            using var writer = new StreamWriter(stream, Encoding.UTF8 );
            foreach (var value in values)
            {
                writer.WriteLine(value.GetOutput());  
            }
        }  
        finally
        {
            stream?.Dispose();
        }  
    }
}