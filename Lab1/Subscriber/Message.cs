using System;

namespace Subscribers
{
    public class Message
    {
        public string Data{get;set;}
        public int ChannelId{get;set;}
        public bool IsSubscriber{get;set;}
    }
}