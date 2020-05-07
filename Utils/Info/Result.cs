﻿using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class Result
    {
        public MessageList Successes { get; set; } = new MessageList();
        public MessageList Warnings { get; set; } = new MessageList();
        public MessageList Errors { get; set; } = new MessageList();

        public Result() { }
        public Result(params Result[] results)
        {
            foreach (Result result in results) Merge(result);
        }

        public Result Merge(Result result)
        {
            if (result != null)
            {
                Successes.AddRange(result.Successes);
                Warnings.AddRange(result.Warnings);
                Errors.AddRange(result.Errors);
            }
            return this;
        }

        // ERRORS
        public Result AddError(String error)
        {
            Errors.Add(error);
            return this;
        }

        public Result AddErrors(List<String> errors)
        {
            Errors.AddRange(errors);
            return this;
        }

        public bool HasErrors()
        {
            return !Errors.IsEmpty();
        }

        // WARNINGS
        public Result AddWarning(String warning)
        {
            Warnings.Add(warning);
            return this;
        }

        public Result AddWarnings(List<String> warnings)
        {
            Warnings.AddRange(warnings);
            return this;
        }

        public bool HasWarnings()
        {
            return !Warnings.IsEmpty();
        }

        // ERRORS
        public Result AddSuccess(String success)
        {
            Successes.Add(success);
            return this;
        }

        public Result AddSuccesses(List<String> successes)
        {
            Successes.AddRange(successes);
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
        public Result(T resultObject) : base()
        {
            ResultObject = resultObject;
        }

        public Result(T resultObject, params Result[] results) : base(results)
        {
            ResultObject = resultObject;
        }

        public new Result<T> Merge(Result result)
        {
            return (Result<T>)base.Merge(result);
        }

        // ERRORS
        public new Result<T> AddError(String error)
        {
            return (Result<T>)base.AddError(error);
        }

        public new Result<T> AddErrors(List<String> errors)
        {
            return (Result<T>)base.AddErrors(errors);
        }

        // WARNINGS
        public new Result<T> AddWarning(String warning)
        {
            return (Result<T>)base.AddWarning(warning);
        }

        public new Result<T> AddWarnings(List<String> warnings)
        {
            return (Result<T>)base.AddWarnings(warnings);
        }

        // SUCCESSES
        public new Result<T> AddSuccess(String success)
        {
            return (Result<T>)base.AddSuccess(success);
        }

        public new Result<T> AddSuccesses(List<String> successes)
        {
            return (Result<T>)base.AddSuccesses(successes);
        }

    }
}
