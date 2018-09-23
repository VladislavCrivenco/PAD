namespace Publishers
{
    public class ResponseMessage
    {
        public enum MessageStatus{
            OK,
            InternalError,
            NetworkError
        }

        public string Message{get;set;}
        public MessageStatus Status{get;set;}
    }
}