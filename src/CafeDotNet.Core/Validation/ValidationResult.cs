namespace CafeDotNet.Core.Validation;

public class ValidationResult
{
    public string Message { get; set; }
    public bool IsValid { get => !(Errors?.Any() ?? false); }
    public IEnumerable<ValidationError?> Errors { get; private set; }

    public ValidationResult()
    {
        Errors = [];
    }

    public void Add(ValidationError? error)
    {
        var list = new List<ValidationError?>(Errors) { error };
        
        SetErrors(list);

    }

    public void Add(params ValidationResult[] validationResults)
    {
        var list = new List<ValidationError?>(Errors);
        
        foreach (var validation in validationResults)
            list.AddRange(validation.Errors);

        SetErrors(list);
    }

    public void Remove(ValidationError error)
    {
        var list = new List<ValidationError?>(Errors);
        list.Remove(error);
       
        SetErrors(list);
    }

    private void SetErrors(List<ValidationError?> errors)
    {
        Errors = errors;

        if (!IsValid && errors.Any())
            Message = errors[0]!.Message;
    }
}
