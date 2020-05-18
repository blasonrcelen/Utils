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
        public IsEmpty(string error) : base(error) { }

        public override bool IsValid(object value) => value == null ? true : (value.GetType() == typeof(string) && string.IsNullOrWhiteSpace((string)value));
    }

    public class IsRequired : ValidatorModel
    {
        public IsRequired() { }
        public IsRequired(string error) : base(error) { }

        public override bool IsValid(object value) => !new IsEmpty().IsValid(value);
    }

    public class IsInteger : ValidatorModel
    {
        public IsInteger(bool ignoreEmpty) : base(ignoreEmpty) { }
        public IsInteger(bool ignoreEmpty, string error) : base(ignoreEmpty, error) { }

        public override bool IsValid(object value) => IsValid(Convert.ToString(value));

        public bool IsValid(string value)
        {
            if (new IsEmpty().IsValid(value)) return IgnoreEmpty;

            foreach (char c in value)
                if (!Char.IsDigit(c))
                    return false;

            return true;
        }
    }

    public class IsNumber : ValidatorModel
    {
        public readonly char DecimalDelimiter = ',';
        public IsNumber(bool ignoreEmpty) : base(ignoreEmpty) { }
        public IsNumber(char decimalDelimiter, bool ignoreEmpty) : base(ignoreEmpty) { DecimalDelimiter = decimalDelimiter; }
        public IsNumber(bool ignoreEmpty, string error) : base(ignoreEmpty, error) { }
        public IsNumber(char decimalDelimiter, bool ignoreEmpty, string error) : base(ignoreEmpty, error) { DecimalDelimiter = decimalDelimiter; }

        public override bool IsValid(object value) => IsValid(Convert.ToString(value));

        public bool IsValid(string value)
        {
            if (new IsEmpty().IsValid(value)) return IgnoreEmpty;

            foreach (char c in value)
                if (!Char.IsDigit(c) && c != DecimalDelimiter)
                    return false;

            return true;
        }
    }

    public class IsDate : ValidatorModel
    {
        public readonly string DatePattern;

        public IsDate(bool ignoreEmpty) : base(ignoreEmpty) { }
        public IsDate(bool ignoreEmpty, string error) : base(ignoreEmpty, error) { }
        public IsDate(string datePattern, bool ignoreEmpty) : base(ignoreEmpty) => DatePattern = datePattern;
        public IsDate(string datePattern, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => DatePattern = datePattern;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                typeof(string).Equals(value.GetType()) ? IsValid((string)value) :
                throw new ArgumentException("the value argument must be of type string");

        public bool IsValid(string value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                DatePattern == null ? !DateTime.TryParse(value, out _) :
                !DateTime.TryParseExact(value, DatePattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    public class IsPattern : ValidatorModel
    {
        public readonly string Pattern;

        public IsPattern(string pattern, bool ignoreEmpty) : base(ignoreEmpty) => Pattern = pattern;
        public IsPattern(string pattern, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Pattern = pattern;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                typeof(string).Equals(value.GetType()) ? IsValid((string)value) :
                throw new ArgumentException("the value argument must be of type string");

        public bool IsValid(string value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : Regex.IsMatch(value, Pattern);
    }

    public class MaxLength : ValidatorModel
    {
        public readonly int Max;

        public MaxLength(int maxLength, bool ignoreEmpty) : base(ignoreEmpty) => Max = maxLength;
        public MaxLength(int maxLength, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Max = maxLength;


        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                typeof(string).Equals(value.GetType()) ? IsValid((string)value) :
                typeof(ICollection).IsAssignableFrom(value.GetType()) ? IsValid((ICollection)value) :
                throw new ArgumentException("the value argument must be of type string or implements ICollection interface");

        public bool IsValid(string value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : value.Length <= Max;
        public bool IsValid(ICollection value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : value.Count <= Max;
    }

    public class MinLength : ValidatorModel
    {
        public readonly int Min;

        public MinLength(int minLength, bool ignoreEmpty) : base(ignoreEmpty) => Min = minLength;
        public MinLength(int minLength, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Min = minLength;


        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                typeof(string).Equals(value.GetType()) ? IsValid((string)value) :
                typeof(ICollection).IsAssignableFrom(value.GetType()) ? IsValid((ICollection)value) :
                throw new ArgumentException("the value argument must be of type string or implements ICollection interface");

        public bool IsValid(string value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : value.Length >= Min;
        public bool IsValid(ICollection value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : value.Count >= Min;
    }

    public class MaxValue : ValidatorModel
    {
        public readonly double Max;

        public MaxValue(double maxValue, bool ignoreEmpty) : base(ignoreEmpty) => Max = maxValue;
        public MaxValue(int maxValue, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Max = maxValue;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                new IsNumber(false).IsValid(value) ? IsValid(Convert.ToDouble(value)) :
                throw new ArgumentException("the value argument must be a number type");


        public bool IsValid(double value) => value <= Max;
    }

    public class MinValue : ValidatorModel
    {
        public readonly double Min;

        public MinValue(double minValue, bool ignoreEmpty) : base(ignoreEmpty) => Min = minValue;

        public MinValue(int minValue, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Min = minValue;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty :
                new IsNumber(false).IsValid(value) ? IsValid(Convert.ToDouble(value)) :
                throw new ArgumentException("the value argument must be a number type");

        public bool IsValid(double value) => value >= Min;
    }

    public class InList : ValidatorModel
    {
        public readonly IList List;

        public InList(IList list, bool ignoreEmpty) : base(ignoreEmpty) => List = list;
        public InList(IList list, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => List = list;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : List.Contains(value);
    }

    public class OutList : ValidatorModel
    {
        public readonly IList List;

        public OutList(IList list, bool ignoreEmpty) : base(ignoreEmpty) => List = list;
        public OutList(IList list, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => List = list;

        public override bool IsValid(object value) => new IsEmpty().IsValid(value) ? IgnoreEmpty : !List.Contains(value);
    }

    public class ListElement : ValidatorModel
    {
        public readonly ValidatorModel Validator;

        public ListElement(ValidatorModel validator, bool ignoreEmpty) : base(ignoreEmpty) => Validator = validator;
        public ListElement(ValidatorModel validator, bool ignoreEmpty, string error) : base(ignoreEmpty, error) => Validator = validator;


        public override bool IsValid(object values)
        {
            if (new IsEmpty().IsValid(values)) return IgnoreEmpty;
            if (!values.GetType().IsAssignableFrom(typeof(IList))) throw new ArgumentException("the value argument must implements IList interface");

            foreach (object value in (IList)values) if (!Validator.IsValid(value)) return false;
            return true;
        }

        public bool IsValid(IList values)
        {
            if (new IsEmpty().IsValid(values)) return IgnoreEmpty;
            foreach (object value in values) if (!Validator.IsValid(value)) return false;
            return true;
        }
    }

    public class ValidateObject : ValidatorModel
    {
        public List<string> Errors { get; private set; }

        public ValidateObject(bool ignoreEmpty) : base(ignoreEmpty) { }
        public ValidateObject(bool ignoreEmpty, string error) : base(ignoreEmpty, error) { }

        public override bool IsValid(object value)
        {
            Errors = new List<string>();
            if (new IsEmpty().IsValid(value))
            {
                if (IgnoreEmpty) { return true; }
                else { Errors.Add("can't be null object"); return false; }
            }

            if (!value.GetType().IsClass) return true;

            bool isValid = true;
            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                object propertyValue = property.GetValue(value);
                ValidatorModel[] validators = property.GetCustomAttributes<ValidatorModel>(false) as ValidatorModel[];
                foreach (ValidatorModel validator in validators)
                {
                    if (!validator.IsValid(propertyValue))
                    {
                        if (new IsRequired().IsValid(validator.Error)) Errors.Add(validator.Error);
                        if (typeof(ValidateObject).Equals(validator.GetType()))
                        {
                            if (((ValidateObject)validator).Errors != null) Errors.AddRange(((ValidateObject)validator).Errors);
                        }
                        else if (typeof(ValidateList).Equals(validator.GetType()))
                        {
                            if (((ValidateList)validator).Errors != null) Errors.AddRange(((ValidateList)validator).Errors);
                        }
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        public string GetErrorsString()
        {
            string returnedError = new IsEmpty().IsValid(Error) ? "" : Error + "\n";
            foreach (string error in Errors) if (new IsRequired().IsValid(error)) returnedError += error + "\n";
            return returnedError;
        }
    }

    public class ValidateList : ValidatorModel
    {
        public List<string> Errors { get; private set; }

        public ValidateList(bool ignoreEmpty) : base(ignoreEmpty) { }
        public ValidateList(bool ignoreEmpty, string error) : base(ignoreEmpty, error) { }

        public override bool IsValid(object value)
        {
            Errors = new List<string>();
            if (new IsEmpty().IsValid(value))
            {
                if (IgnoreEmpty) { return true; }
                else { Errors.Add("can't be null object"); return false; }
            }

            return value is IList && value.GetType().IsGenericType ? IsValid((IList)value) : throw new ArgumentException("the value argument must implements IList interface");
        }

        public bool IsValid(IList values)
        {
            Errors = new List<string>();
            if (new IsEmpty().IsValid(values))
            {
                if (IgnoreEmpty) { return true; }
                else { Errors.Add("can't be null object"); return false; }
            }

            bool isValid = true;
            foreach (object value in values)
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

        public string GetErrorsString()
        {
            string returnedError = new IsEmpty().IsValid(Error) ? "" : Error + "\n";
            foreach (string error in Errors) if (new IsRequired().IsValid(error)) returnedError += error + "\n";
            return returnedError;
        }
    }
}
