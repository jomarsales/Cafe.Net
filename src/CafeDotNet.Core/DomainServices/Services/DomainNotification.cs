using CafeDotNet.Core.Base.ValueObjects;
using CafeDotNet.Core.DomainServices.Interfaces;

namespace CafeDotNet.Core.DomainServices.Services;

public class DomainNotification : IDomainEvent
{
    public int Versao { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public string Key { get; private set; }
    public string Value { get; private set; }
    public TypeNotification TypeNotification { get; private set; }

    public DomainNotification(string key, string value, TypeNotification typeNotification = TypeNotification.Error)
    {
        Versao = 1;
        DataCriacao = DateTime.UtcNow;
        TypeNotification = typeNotification;

        Key = key;
        Value = value;
    }
}
