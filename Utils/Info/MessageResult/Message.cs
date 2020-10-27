using Newtonsoft.Json;

namespace Utils.Info
{
    public enum MESSAGE_TYPE
    {
        SUCCESS,
        WARN,
        ERROR,
        FATAL
    }

    public static class MessageType
    {
        public static string GetHeader(this MESSAGE_TYPE type)
        {
            switch (type)
            {
                case MESSAGE_TYPE.SUCCESS: return "Success";
                case MESSAGE_TYPE.WARN: return "Warning";
                case MESSAGE_TYPE.ERROR: return "Error";
                case MESSAGE_TYPE.FATAL: return "Fatal";
                default: return "";
            }
        }
    }

    public class Message
    {
        public MESSAGE_TYPE Type { get; set; }
        public string Header { get; set; }
        public string Msg { get; set; }

        [JsonConstructor]
        public Message(MESSAGE_TYPE type, string header, string msg)
        {
            Type = type;
            Header = header;
            Msg = msg;
        }

        public Message(MESSAGE_TYPE type, string msg)
        {
            Type = type;
            Header = type.GetHeader();
            Msg = msg;
        }
    }
}
