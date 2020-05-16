using System;

namespace Utils.Validators
{
    public interface IValidator
    {
        bool IsValid(object value);
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public abstract class ValidatorModel : Attribute, IValidator
    {
        public readonly bool IgnoreEmpty;
        public readonly string Error;

        public ValidatorModel() { }

        public ValidatorModel(bool ignoreEmpty)
        {
            IgnoreEmpty = ignoreEmpty;
        }

        public ValidatorModel(string error)
        {
            Error = error;
        }

        public ValidatorModel(bool ignoreEmpty, string error)
        {
            IgnoreEmpty = ignoreEmpty;
            Error = error;
        }

        public abstract bool IsValid(object value);
    }
}
