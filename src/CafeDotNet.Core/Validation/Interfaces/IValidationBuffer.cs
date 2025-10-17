namespace CafeDotNet.Core.Validation.Interfaces
{
    public interface IValidationBuffer
    {
        void Add(string key, string message);
        bool HasErrors { get; }
        IReadOnlyDictionary<string, List<string>> GetAll();
        void Clear();
    }
}
