namespace CafeDotNet.Core.Validation
{
    public class ValidationError
    {
        public string Name { get; private set; }
        public string Message { get; private set; }

        public ValidationError(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
