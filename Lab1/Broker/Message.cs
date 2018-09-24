using System;

namespace Brokers
{
    public class Message
    {
        public string Data{get;set;}
        public string ChannelId{get;set;}
        public bool IsSubscriber{get;set;}
        public DateTime TimeCreated{get;set;}
    }
}
