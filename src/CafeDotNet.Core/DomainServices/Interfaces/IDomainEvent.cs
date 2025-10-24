namespace CafeDotNet.Core.DomainServices.Interfaces;

public interface IDomainEvent
{
    int Versao { get; }
    DateTime DataCriacao { get; }
}
