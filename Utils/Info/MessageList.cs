using System.Collections.Generic;

namespace Utils.Info
{
    public class MessageList : List<string>
    {
        public MessageList() { }
        public MessageList(params string[] messages) => AddRange(messages);
        public MessageList(List<string> messages) => AddRange(messages);

        public bool IsEmpty() => Count == 0;

        public override string ToString()
        {
            string strMessage = "";
            foreach (string message in this) strMessage += message + "\n";
            return strMessage;
        }
    }
}
