using System;
using System.Collections.Generic;

namespace Utils.Info
{
    public class MessageList : List<String>
    {
        public MessageList() { }
        public MessageList(params String[] _messages)
        {
            AddRange(_messages);
        }

        public MessageList(List<String> _messages)
        {
            AddRange(_messages);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public override string ToString()
        {
            String strMessage = "";
            foreach (String message in this) strMessage += message + "\n";
            return strMessage;
        }
    }
}
