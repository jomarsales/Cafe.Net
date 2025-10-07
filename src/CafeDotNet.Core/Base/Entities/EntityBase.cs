namespace CafeDotNet.Core.Base.Entities;

public abstract class EntityBase
{
    public ulong Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    protected EntityBase()
    {
        CreatedAt = DateTime.Now;
    }

    protected void Deactivate() => IsActive = false;
    protected void Activate() => IsActive = true;
    protected void SetUpdated() => UpdatedAt = DateTime.Now;
}