using System.Collections.Generic;

namespace Utils.Info.MessageResult
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
        public Result AddErrors(params string[] errors)
        {
            Errors.AddRange(errors);
            return this;
        }

        public Result AddErrors(List<string> errors) => AddErrors(errors.ToArray());

        public bool HasErrors() => !Errors.IsEmpty();

        // WARNINGS
        public Result AddWarnings(params string[] warnings)
        {
            Warnings.AddRange(warnings);
            return this;
        }

        public Result AddWarnings(List<string> warnings) => AddWarnings(warnings.ToArray());

        public bool HasWarnings() => !Warnings.IsEmpty();

        // ERRORS
        public Result AddSuccesses(params string[] successes)
        {
            Successes.AddRange(successes);
            return this;
        }

        public Result AddSuccesses(List<string> successes) => AddSuccesses(successes.ToArray());

        public bool HasSuccesses() => !Successes.IsEmpty();
    }

    public class Result<T> : Result
    {
        public T ResultObject;

        public Result() : base() { }
        public Result(T resultObject) : base() => ResultObject = resultObject;
        public Result(T resultObject, params Result[] results) : base(results) => ResultObject = resultObject;

        public new Result<T> Merge(Result result) => (Result<T>)base.Merge(result);

        // ERRORS
        public new Result<T> AddErrors(params string[] errors) => (Result<T>)base.AddErrors(errors);
        public new Result<T> AddErrors(List<string> errors) => (Result<T>)base.AddErrors(errors);

        // WARNINGS
        public new Result<T> AddWarnings(params string[] warnings) => (Result<T>)base.AddWarnings(warnings);
        public new Result<T> AddWarnings(List<string> warnings) => (Result<T>)base.AddWarnings(warnings);

        // SUCCESSES
        public new Result<T> AddSuccesses(params string[] successes) => (Result<T>)base.AddSuccesses(successes);
        public new Result<T> AddSuccesses(List<string> successes) => (Result<T>)base.AddSuccesses(successes);
    }
}
