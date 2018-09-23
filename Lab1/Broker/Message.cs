using System;

namespace Brokers
{
    public class Message
    {
        public string Data{get;set;}
        public int ChannelId{get;set;}
        public bool IsSubscriber{get;set;}
    }
}
