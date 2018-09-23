using System.Collections.Generic;
using System.Net;

namespace Brokers
{
    public class Channel
    {
        public int Id{get; set;}
        public List<Message> Events{get; private set;}

        public List<Subscriber> Subscribers{get; private set;}

        public Channel(int id)
        {
            Events = new List<Message>();
            Subscribers = new List<Subscriber>();
            Id = id;
        }
        public Channel(int id, Message msg)
        {
            Events = new List<Message>();
            Subscribers = new List<Subscriber>();
            Id = id;

            Events.Add(msg);
        }
    }
}
