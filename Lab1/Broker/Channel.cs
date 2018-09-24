using System.Collections.Generic;
using System.Net;

namespace Brokers
{
    public class Channel
    {
        public string Id{get; set;}
        public List<Message> Events{get; private set;}

        public List<Subscriber> Subscribers{get; private set;}

        public Channel(string id)
        {
            Events = new List<Message>();
            Subscribers = new List<Subscriber>();
            Id = id;
        }
        public Channel(string id, Message msg)
        {
            Events = new List<Message>();
            Subscribers = new List<Subscriber>();
            Id = id;

            Events.Add(msg);
        }
    }
}
