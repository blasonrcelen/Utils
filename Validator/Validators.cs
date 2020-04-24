using System;

namespace Validators
{
    public class IsEmpty : ValidatorModel
    {
        public override bool IsValid(object _value)
        {
            return _value == null ? true : (_value.GetType() == typeof(String) && String.IsNullOrWhiteSpace((String)_value)) ? true : false;
        }
    }

    public class IsInteger : ValidatorModel
    {
        public IsInteger(bool _ignoreEmpty) : base(_ignoreEmpty) { }

        public override bool IsValid(object _value)
        {
            return _value == null ? (IgnoreEmpty ? true : false) : IsValid(Convert.ToString(_value));
        }

        public bool IsValid(String _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty ? true : false;
            foreach (char c in _value) if (!Char.IsDigit(c)) return false;
            return true;
        }
    }

    public class IsNumber : ValidatorModel
    {
        public IsNumber(bool _ignoreEmpty) : base(_ignoreEmpty) { }

        public override bool IsValid(object _value)
        {
            return _value == null ? (IgnoreEmpty ? true : false) : IsValid(Convert.ToString(_value));
        }

        public bool IsValid(String _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty ? true : false;
            foreach (char c in _value) if (!Char.IsDigit(c) && c != '.') return false;
            return true;
        }
    }

    public class IsDate : ValidatorModel
    {
        public override bool IsValid(object _value)
        {
            throw new NotImplementedException();
        }
    }

}
