using CafeDotNet.Core.Validation;

namespace CafeDotNet.Core.Base.ValueObjects
{
    public abstract class ValueObjectBase
    {
        public ValidationResult ValidationResult { get; set; }

        protected ValueObjectBase()
        {
            ValidationResult = new ValidationResult();
        }
        protected abstract void Validate();
    }
}
