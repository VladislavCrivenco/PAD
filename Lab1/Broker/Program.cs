using System;

namespace Brokers
{
    class Program
    {
        static void Main(string[] args)
        {
            var broker = new Broker();
            broker.StartListening();
        }
    }
}
