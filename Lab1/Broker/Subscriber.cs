using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace Brokers
{
    public class Subscriber
    {
        public Subscriber(){

        }
        public IPEndPoint IpEndPoint{get;set;}

        public Socket Socket{get;set;}
    }
}
