using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class Result
    {
        public MessageList SuccessMessages { get; set; } = new MessageList();
        public MessageList WarningMessages { get; set; } = new MessageList();
        public MessageList ErrorMessages { get; set; } = new MessageList();

        public Result() { }
        public Result(params Result[] results)
        {
            foreach (Result result in results) Merge(result);
        }

        // SUCCESS LIST
        public void AddSuccess(String _message, byte _code = 0x00)
        {
            SuccessMessages.Add(_message, _code);
        }

        public void AddSuccess(List<String> _messages, byte _code = 0x00)
        {
            SuccessMessages.AddRange(_messages, _code);
        }

        public void AddSuccess(MessageList _message)
        {
            if (_message != null && _message.Count > 0) SuccessMessages.AddRange(_message);
        }

        public long CountSuccesses()
        {
            return SuccessMessages != null ? SuccessMessages.Count : 0;
        }

        public bool HasSuccesses()
        {
            return CountSuccesses() > 0;
        }

        public String SuccessesToString(bool _showCodes = false)
        {
            return SuccessMessages.ToString(_showCodes);
        }

        // WARNING LIST
        public void AddWarning(String _message, byte _code = 0x00)
        {
            WarningMessages.Add(_message, _code);
        }

        public void AddWarning(List<String> _messages, byte _code = 0x00)
        {
            WarningMessages.AddRange(_messages, _code);
        }

        public void AddWarning(MessageList _message)
        {
            if (_message != null && _message.Count > 0) WarningMessages.AddRange(_message);
        }

        public long CountWarnings()
        {
            return WarningMessages != null ? WarningMessages.Count : 0;
        }

        public bool HasWarnings()
        {
            return CountWarnings() > 0;
        }

        public String WarningsToString(bool _showCodes = false)
        {
            return WarningMessages.ToString(_showCodes);
        }

        // ERRORS LIST
        public void AddError(String _message, byte _code = 0x00)
        {
            ErrorMessages.Add(_message, _code);
        }

        public void AddError(List<String> _messages, byte _code = 0x00)
        {
            ErrorMessages.AddRange(_messages, _code);
        }

        public void AddError(MessageList _message)
        {
            if (_message != null && _message.Count > 0) ErrorMessages.AddRange(_message);
        }

        public long CountErrors()
        {
            return ErrorMessages != null ? ErrorMessages.Count : 0;
        }

        public bool HasErrors()
        {
            return CountErrors() > 0;
        }

        public String ErrorsToString(bool _showCodes = false)
        {
            return ErrorMessages.ToString(_showCodes);
        }
    
        // Merge Result
        public void Merge(Result _result)
        {
            if (_result != null)
            {
                AddError(_result.ErrorMessages);
                AddWarning(_result.WarningMessages);
                AddSuccess(_result.SuccessMessages);
            }
        }
    }

    public class Result<T> : Result
    {
        public T ResultObject;

        public Result() : base() { }
        public Result(params Result[] _results) : base(_results) { }
    }
}
