using StringCombo.Exceptions;
using StringCombo.Models;

namespace StringCombo.Validators;

public interface IJoinableStringValidator
{
    ValidationException? Validate(JoinableString value, int expectedLength);
}

internal class JoinableStringValidator : IJoinableStringValidator
{
    public ValidationException? Validate(JoinableString value, int expectedLength)
    {
        if (value.Length < expectedLength)
        {
            return new ToShortValidationException();
        }
        
        if (value.Length > expectedLength)
        {
            return new ToLongValidationException();
        }

        return null;
    }
}