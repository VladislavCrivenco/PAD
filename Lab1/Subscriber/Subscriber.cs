using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Subscribers
{
    public class Subscriber
    {
        private int channelId;
        public Subscriber()
        {
            Console.WriteLine("Welcome to subscriber");
            Console.WriteLine("Introduce channel id");
            //var line = Console.ReadLine();

            //channelId = int.Parse(line);
            channelId = 10;
        }

        public void Connect()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Loopback, 5600);

                NetworkStream stream = client.GetStream();

                var message = new Message
                {
                    ChannelId = channelId,
                    IsSubscriber = true
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                json += "ENDMSG";
                byte[] data = Encoding.UTF8.GetBytes(json);

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Message sent");
                ReadMessages(client);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                client.Close();
            }

        }

        private void ReadMessages(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();

            while (true)
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (!builder.ToString().EndsWith("ENDMSG"));
                builder.Replace("ENDMSG", "");

                Console.WriteLine("Received " + builder.ToString());
            }
        }
    }
}