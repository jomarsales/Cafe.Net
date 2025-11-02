using CafeDotNet.Core.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeDotNet.Core.Base.Entities;

public abstract class EntityBase
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    [NotMapped]
    public ValidationResult ValidationResult { get; set; }

    protected EntityBase()
    {
        CreatedAt = DateTime.UtcNow;
        
        ValidationResult = new ValidationResult();
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    protected void SetUpdated() => UpdatedAt = DateTime.UtcNow;
    protected abstract void Validate();
}