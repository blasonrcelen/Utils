using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class Result
    {
        public MessageList Successes { get; set; } = new MessageList();
        public MessageList Warnings { get; set; } = new MessageList();
        public MessageList Errors { get; set; } = new MessageList();

        public Result() { }
        public Result(params Result[] _results)
        {
            foreach (Result result in _results) Merge(result);
        }

        public Result Merge(Result _result)
        {
            if (_result != null)
            {
                Successes.AddRange(_result.Successes);
                Warnings.AddRange(_result.Warnings);
                Errors.AddRange(_result.Errors);
            }
            return this;
        }

        // ERRORS
        public Result AddError(String _error)
        {
            Errors.Add(_error);
            return this;
        }

        public Result AddErrors(List<String> _errors)
        {
            Errors.AddRange(_errors);
            return this;
        }

        public bool HasErrors()
        {
            return !Errors.IsEmpty();
        }

        // WARNINGS
        public Result AddWarning(String _warning)
        {
            Warnings.Add(_warning);
            return this;
        }

        public Result AddWarnings(List<String> _warnings)
        {
            Warnings.AddRange(_warnings);
            return this;
        }

        public bool HasWarnings()
        {
            return !Warnings.IsEmpty();
        }

        // ERRORS
        public Result AddSuccess(String _success)
        {
            Successes.Add(_success);
            return this;
        }

        public Result AddSuccesses(List<String> _successes)
        {
            Successes.AddRange(_successes);
            return this;
        }

        public bool HasSuccesses()
        {
            return !Successes.IsEmpty();
        }
    }

    public class Result<T> : Result
    {
        public T ResultObject;

        public Result() : base() { }
        public Result(T _resultObject) : base()
        {
            ResultObject = _resultObject;
        }

        public Result(T _resultObject, params Result[] _results) : base(_results)
        {
            ResultObject = _resultObject;
        }

        public new Result<T> Merge(Result _result)
        {
            return (Result<T>)base.Merge(_result);
        }

        // ERRORS
        public new Result<T> AddError(String _error)
        {
            return (Result<T>)base.AddError(_error);
        }

        public new Result<T> AddErrors(List<String> _errors)
        {
            return (Result<T>)base.AddErrors(_errors);
        }

        // WARNINGS
        public new Result<T> AddWarning(String _warning)
        {
            return (Result<T>)base.AddWarning(_warning);
        }

        public new Result<T> AddWarnings(List<String> _warnings)
        {
            return (Result<T>)base.AddWarnings(_warnings);
        }

        // SUCCESSES
        public new Result<T> AddSuccess(String _success)
        {
            return (Result<T>)base.AddSuccess(_success);
        }

        public new Result<T> AddSuccesses(List<String> _successes)
        {
            return (Result<T>)base.AddSuccesses(_successes);
        }

    }
}
