using System;

namespace Utils.Validators
{
    public interface IValidator
    {
        bool IsValid(object _value);
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class ValidatorModel : Attribute, IValidator
    {
        public readonly bool IgnoreEmpty;
        public readonly String Error;

        public ValidatorModel() { }

        public ValidatorModel(bool _ignoreEmpty)
        {
            IgnoreEmpty = _ignoreEmpty;
        }

        public ValidatorModel(String _error)
        {
            Error = _error;
        }

        public ValidatorModel(bool _ignoreEmpty, String _error)
        {
            IgnoreEmpty = _ignoreEmpty;
            Error = _error;
        }

        public abstract bool IsValid(object _value);
    }
}
