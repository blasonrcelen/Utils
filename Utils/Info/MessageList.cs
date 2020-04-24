using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class Message
    {
        public byte Code { get; set; }
        public String Msg { get; set; }

        public Message(String _message) { Msg = _message; }
        public Message(String _message, byte _code)
        {
            Msg = _message;
            Code = _code;
        }

        public override string ToString()
        {
            return Msg == null ? "" : Msg;
        }

        public string ToString(bool _showCode)
        {
            return _showCode ? $"[{Code}]: " + ToString() : ToString();
        }
    }

    public class MessageList : List<Message>
    {
        public MessageList() { }
        public MessageList(String _message, byte _code = 0x00)
        {
            if (_message != null) Add(new Message(_message, _code));
        }

        public MessageList(List<Message> _messages)
        {
            if (_messages != null) AddRange(_messages);
        }

        public void Add(String _message, byte _code = 0x00)
        {
            Add(new Message(_message, _code));
        }

        public void Add(String _message, String _prefix, byte _code = 0x00)
        {
            if (_message == null) return;
            if (_prefix == null) _prefix = "";

            Add(new Message(_prefix + _message, _code));
        }

        public void AddRange(List<String> _messages, byte _code = 0x00)
        {
            if (_messages == null) return;

            foreach (string message in _messages)
                if (message != null) Add(new Message(message, _code));
        }

        public void AddRange(List<String> _messages, String _prefix, byte _code = 0x00)
        {
            if (_messages == null) return;
            if (_prefix == null) _prefix = "";

            foreach (string message in _messages)
                if (message != null) Add(new Message(_prefix + message, _code));
        }

        public void AddRange(List<Message> _messages, String _prefix)
        {
            if (_messages == null) return;
            if (_prefix == null) _prefix = "";

            foreach (Message msg in _messages)
            {
                if (msg != null)
                {
                    msg.Msg = _prefix + _messages;
                    Add(msg);
                }
            }
        }

        public void AddRange(MessageList _messages)
        {
            if (_messages == null) return;
            AddRange(_messages);
        }

        public bool HasMessages()
        {
            return Count > 0;
        }

        public override string ToString() => ToString(false);
        public string ToString(bool _showCodes = false)
        {
            String strMessage = "";
            foreach (Message message in this)
                strMessage += message.ToString(_showCodes) + "\n";

            return strMessage;
        }

        public string ToString(String _iniMessage, bool _showCodes = false)
        {
            return (_iniMessage == null ? "" : _iniMessage + "\n") + ToString(_showCodes);
        }
    }
}
