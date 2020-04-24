using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Utils.Validators
{
    public class IsEmpty : ValidatorModel
    {
        public IsEmpty() { }
        public IsEmpty(String _error) : base(_error) { }

        public override bool IsValid(object _value)
        {
            return _value == null ? true : (_value.GetType() == typeof(String) && String.IsNullOrWhiteSpace((String)_value));
        }
    }

    public class IsRequired : ValidatorModel
    {
        public IsRequired() { }
        public IsRequired(String _error) : base(_error) { }

        public override bool IsValid(object _value)
        {
            return !new IsEmpty().IsValid(_value);
        }
    }

    public class IsInteger : ValidatorModel
    {
        public IsInteger(bool _ignoreEmpty) : base(_ignoreEmpty) { }
        public IsInteger(bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { }

        public override bool IsValid(object _value)
        {
            return IsValid(Convert.ToString(_value));
        }

        public bool IsValid(String _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty;

            foreach (char c in _value)
                if (!Char.IsDigit(c))
                    return false;

            return true;
        }
    }

    public class IsNumber : ValidatorModel
    {
        public readonly char DecimalDelimiter = ',';
        public IsNumber(bool _ignoreEmpty) : base(_ignoreEmpty) { }
        public IsNumber(char _decimalDelimiter, bool _ignoreEmpty) : base(_ignoreEmpty) { DecimalDelimiter = _decimalDelimiter; }
        public IsNumber(bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { }
        public IsNumber(char _decimalDelimiter, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { DecimalDelimiter = _decimalDelimiter; }

        public override bool IsValid(object _value)
        {
            return IsValid(Convert.ToString(_value));
        }

        public bool IsValid(String _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty;

            foreach (char c in _value)
                if (!Char.IsDigit(c) && c != DecimalDelimiter)
                    return false;

            return true;
        }
    }

    public class IsDate : ValidatorModel
    {
        public readonly String DatePattern;

        public IsDate(bool _ignoreEmpty) : base(_ignoreEmpty) { }
        public IsDate(bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { }

        public IsDate(String _datePattern, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            DatePattern = _datePattern;
        }

        public IsDate(String _datePattern, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            DatePattern = _datePattern;
        }


        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                typeof(String).Equals(_value.GetType()) ? IsValid((String)_value) :
                throw new ArgumentException("the _value argument must be of type string");
        }

        public bool IsValid(String _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                DatePattern == null ? !DateTime.TryParse(_value, out _) :
                !DateTime.TryParseExact(_value, DatePattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }

    public class IsPattern : ValidatorModel
    {
        public readonly String Pattern;

        public IsPattern(String _pattern, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Pattern = _pattern;
        }

        public IsPattern(String _pattern, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Pattern = _pattern;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                typeof(String).Equals(_value.GetType()) ? IsValid((String)_value) :
                throw new ArgumentException("the _value argument must be of type string");
        }

        public bool IsValid(String _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : Regex.IsMatch(_value, Pattern);
        }
    }

    public class MaxLength : ValidatorModel
    {
        public readonly int Max;

        public MaxLength(int _maxLength, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Max = _maxLength;
        }

        public MaxLength(int _maxLength, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Max = _maxLength;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                typeof(String).Equals(_value.GetType()) ? IsValid((String)_value) :
                typeof(ICollection).IsAssignableFrom(_value.GetType()) ? IsValid((ICollection)_value) :
                throw new ArgumentException("the _value argument must be of type string or implements ICollection interface");
        }

        public bool IsValid(String _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : _value.Length <= Max;
        }

        public bool IsValid(ICollection _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : _value.Count <= Max;
        }
    }

    public class MinLength : ValidatorModel
    {
        public readonly int Min;

        public MinLength(int _minLength, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Min = _minLength;
        }

        public MinLength(int _minLength, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Min = _minLength;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                typeof(String).Equals(_value.GetType()) ? IsValid((String)_value) :
                typeof(ICollection).IsAssignableFrom(_value.GetType()) ? IsValid((ICollection)_value) :
                throw new ArgumentException("the _value argument must be of type string or implements ICollection interface");
        }

        public bool IsValid(String _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : _value.Length >= Min;
        }

        public bool IsValid(ICollection _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : _value.Count >= Min;
        }
    }

    public class MaxValue : ValidatorModel
    {
        public readonly double Max;

        public MaxValue(double _maxValue, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Max = _maxValue;
        }

        public MaxValue(int _maxValue, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Max = _maxValue;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                new IsNumber(false).IsValid(_value) ? IsValid(Convert.ToDouble(_value)) :
                throw new ArgumentException("the _value argument must be a number type");
        }

        public bool IsValid(double _value)
        {
            return _value <= Max;
        }
    }

    public class MinValue : ValidatorModel
    {
        public readonly double Min;

        public MinValue(double _minValue, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Min = _minValue;
        }

        public MinValue(int _minValue, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Min = _minValue;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty :
                new IsNumber(false).IsValid(_value) ? IsValid(Convert.ToDouble(_value)) :
                throw new ArgumentException("the _value argument must be a number type");
        }

        public bool IsValid(double _value)
        {
            return _value >= Min;
        }
    }

    public class InList : ValidatorModel
    {
        public readonly IList List;

        public InList(IList _list, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            List = _list;
        }

        public InList(IList _list, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            List = _list;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : List.Contains(_value);
        }
    }

    public class OutList : ValidatorModel
    {
        public readonly IList List;

        public OutList(IList _list, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            List = _list;
        }

        public OutList(IList _list, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            List = _list;
        }

        public override bool IsValid(object _value)
        {
            return new IsEmpty().IsValid(_value) ? IgnoreEmpty : !List.Contains(_value);
        }
    }

    public class ListElement : ValidatorModel
    {
        public readonly ValidatorModel Validator;

        public ListElement(ValidatorModel _validator, bool _ignoreEmpty) : base(_ignoreEmpty)
        {
            Validator = _validator;
        }

        public ListElement(ValidatorModel _validator, bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error)
        {
            Validator = _validator;
        }

        public override bool IsValid(object _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty;

            if (!_value.GetType().IsAssignableFrom(typeof(IList)))
                throw new ArgumentException("the _value argument must implements IList interface");

            foreach (object value in (IList)_value)
                if (!Validator.IsValid(value))
                    return false;

            return true;
        }

        public bool IsValid(IList _value)
        {
            if (new IsEmpty().IsValid(_value)) return IgnoreEmpty;

            foreach (object value in _value)
                if (!Validator.IsValid(value))
                    return false;

            return true;
        }
    }

    public class ValidateObject : ValidatorModel
    {
        public List<String> Errors { get; private set; }

        public ValidateObject(bool _ignoreEmpty) : base(_ignoreEmpty) { }
        public ValidateObject(bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { }

        public override bool IsValid(object _value)
        {
            Errors = new List<string>();
            if (new IsEmpty().IsValid(_value))
                if (IgnoreEmpty) { return true; }
                else { Errors.Add("can't be null object"); return false; }

            if (!_value.GetType().IsClass) return true;

            bool isValid = true;
            foreach (PropertyInfo property in _value.GetType().GetProperties())
            {
                object propertyValue = property.GetValue(_value);
                ValidatorModel[] validators = property.GetCustomAttributes<ValidatorModel>(false) as ValidatorModel[];
                foreach (ValidatorModel validator in validators)
                {
                    if (!validator.IsValid(propertyValue))
                    {
                        if (new IsRequired().IsValid(validator.Error)) Errors.Add(validator.Error);

                        if (typeof(ValidateObject).Equals(validator.GetType()))
                        {
                            if (((ValidateObject)validator).Errors != null)
                                Errors.AddRange(((ValidateObject)validator).Errors);
                        }
                        else if (typeof(ValidateList).Equals(validator.GetType()))
                        {
                            if (((ValidateList)validator).Errors != null)
                                Errors.AddRange(((ValidateList)validator).Errors);
                        }

                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        public String GetErrorsString()
        {
            String returnedError = new IsEmpty().IsValid(Error) ? "" : Error + "\n";
            foreach (String error in Errors)
                if (new IsRequired().IsValid(error))
                    returnedError += error + "\n";

            return returnedError;
        }
    }

    public class ValidateList : ValidatorModel
    {
        public List<String> Errors { get; private set; }

        public ValidateList(bool _ignoreEmpty) : base(_ignoreEmpty) { }
        public ValidateList(bool _ignoreEmpty, String _error) : base(_ignoreEmpty, _error) { }

        public override bool IsValid(object _value)
        {
            if (new IsEmpty().IsValid(_value))
                if (IgnoreEmpty) { return true; }
                else { Errors = new List<string>(); Errors.Add("can't be null object"); return false; }

            return _value is IList && _value.GetType().IsGenericType ? IsValid((IList)_value) :
                throw new ArgumentException("the _value argument must implements IList interface");
        }

        public bool IsValid(IList _values)
        {
            Errors = new List<string>();
            if (new IsEmpty().IsValid(_values))
                if (IgnoreEmpty) { return true; }
                else { Errors.Add("can't be null object"); return false; }

            bool isValid = true;
            foreach (object value in _values)
            {
                ValidateObject validator = new ValidateObject(IgnoreEmpty);
                if (!validator.IsValid(value))
                {
                    Errors.AddRange(validator.Errors);
                    isValid = false;
                }
            }
            return isValid;
        }

        public String GetErrorsString()
        {
            String returnedError = new IsEmpty().IsValid(Error) ? "" : Error + "\n";
            foreach (String error in Errors)
                if (new IsRequired().IsValid(error))
                    returnedError += error + "\n";

            return returnedError;
        }
    }
}
