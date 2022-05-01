namespace StringCombo.Models;

public class JoinableString
{
    public int Length => ToString().Length;
    
    private readonly IEnumerable<string> _values;
    
    public JoinableString(params string [] values)
    {
        _values = values;
    }

    public string GetOutput() => $"{string.Join("+", _values)}={string.Join("", _values)}";

    public override string ToString() => string.Join("", _values);
}