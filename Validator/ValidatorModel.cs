using System;

namespace Validators
{
    public interface IValidator
    {
        bool IgnoreEmpty { get; set; }
        bool IsValid(object _value);
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class ValidatorModel : Attribute, IValidator
    {
        public bool IgnoreEmpty { get; set; }

        public ValidatorModel() { }

        public ValidatorModel(bool _ignoreEmpty)
        {
            IgnoreEmpty = _ignoreEmpty;
        }

        public abstract bool IsValid(object _value);
    }

}
