using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Base.Entities;

public abstract class EntityBase
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public ValidationResult ValidationResult { get; set; }

    protected EntityBase()
    {
        CreatedAt = DateTime.UtcNow;
        
        ValidationResult = new ValidationResult();
    }

    protected void Deactivate() => IsActive = false;
    protected void Activate() => IsActive = true;
    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;
    protected abstract void Validate();
}