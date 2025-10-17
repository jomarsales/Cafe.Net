using CafeDotNet.Core.Validation.Interfaces;
using System.Collections.Concurrent;

namespace CafeDotNet.Core.Validation.Services;

public class ValidationBuffer : IValidationBuffer
{
    private readonly AsyncLocal<ConcurrentDictionary<string, List<string>>> _buffer = new();

    private ConcurrentDictionary<string, List<string>> Buffer
    {
        get
        {
            _buffer.Value ??= new ConcurrentDictionary<string, List<string>>();
            return _buffer.Value;
        }
    }

    public void Add(string key, string message)
    {
        Buffer.AddOrUpdate(
            key,
            _ => new List<string> { message },
            (_, list) =>
            {
                list.Add(message);
                return list;
            });
    }

    public bool HasErrors => !Buffer.IsEmpty;

    public IReadOnlyDictionary<string, List<string>> GetAll() => Buffer;

    public void Clear() => Buffer.Clear();
}
