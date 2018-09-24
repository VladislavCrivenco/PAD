using System;

namespace Publishers
{
    public class Message
    {
        public string Data{get;set;}
        public string ChannelId{get;set;}
        public bool IsSubscriber{get;set;}

        public DateTime TimeCreated{get;set;}
        public Message(string data, string channelId)
        {
            Data = data;
            ChannelId = channelId;
            IsSubscriber = false;
            TimeCreated = DateTime.UtcNow;
        }
        public Message(){}
    }
}