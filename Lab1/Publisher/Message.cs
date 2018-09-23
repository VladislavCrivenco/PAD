namespace Publishers
{
    public class Message
    {
        public string Data{get;set;}
        public int ChannelId{get;set;}
        public bool IsSubscriber{get;set;}
        public Message(string data, int channelId, bool isSubscriber)
        {
            Data = data;
            ChannelId = channelId;
            IsSubscriber = isSubscriber;
        }
        public Message(){}
    }
}