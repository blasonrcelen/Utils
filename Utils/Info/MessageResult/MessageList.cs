using System.Collections.Generic;
using System.Text;

namespace Utils.Info
{
    public class MessageList : List<string>
    {
        public string Header { get; set; }

        public MessageList(string header = null)
        {
            this.Header = header;
        }

        public MessageList(string header, params string[] messages)
        {
            this.Header = header;
            AddRange(messages);
        }

        public MessageList(string header, List<string> messages)
        {
            this.Header = header;
            AddRange(messages);
        }

        public MessageList(params string[] messages) => AddRange(messages);
        public MessageList(List<string> messages) => AddRange(messages);

        public bool IsEmpty() => Count == 0;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            if (Header != null)
            {
                builder.AppendLine(Header);
                builder.Append('=', Header.Length);
                builder.AppendLine();
            }

            foreach (string message in this) builder.AppendLine(message);
            return builder.ToString();
        }
    }
}
