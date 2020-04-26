using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class Result
    {
        public MessageList SuccessMessages { get; set; }
        public MessageList WarningMessages { get; set; }
        public MessageList ErrorMessages { get; set; }

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
            SuccessMessages.AddRange(_message);
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
            WarningMessages.AddRange(_message);
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
            ErrorMessages.AddRange(_message);
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
            AddError(_result.ErrorMessages);
            AddWarning(_result.WarningMessages);
            AddSuccess(_result.SuccessMessages);
        }
    }

    public class Result<T> : Result
    {
        public T ResultObject;
    }
}
