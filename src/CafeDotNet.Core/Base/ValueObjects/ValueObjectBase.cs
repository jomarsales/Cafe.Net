using CafeDotNet.Core.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeDotNet.Core.Base.ValueObjects
{
    public abstract class ValueObjectBase
    {
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }

        protected ValueObjectBase()
        {
            ValidationResult = new ValidationResult();
        }
        protected abstract void Validate();
    }
}
