using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subscribers
{
    public class Subscriber
    {
        bool isInitialized = false;
        public Subscriber()
        {
            Console.WriteLine("Welcome to subscriber.");

            // channelId = 10;
        }

        public void Connect()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Loopback, 5600);

                Console.WriteLine("Please introduce channel id (Add :)");
                do
                {
                    var line = Console.ReadLine();
                    //line = "Add :10";
                    if (line.Contains("Add :")){
                        var channel = line.Replace("Add :", "").Trim();
                        if (string.IsNullOrWhiteSpace(channel)){
                            continue;
                        }
                        
                        var message = new Message
                        {
                            ChannelId = channel,
                            IsSubscriber = true
                        };
                        SendMessage(client, message);
                        Console.WriteLine("Connected to broker at channel " + channel);
                    
                        if (!isInitialized){
                            Task.Run(() => ReadMessages(client));
                            isInitialized = true;
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Please introduce channel id (Add :)");
                    }

                }
                while(true);
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

                var messages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Message>>(builder.ToString());
                PrintMessages(messages);
            }
        }

        private void SendMessage(TcpClient client, Message message){
                NetworkStream stream = client.GetStream();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                json += "ENDMSG";
                byte[] data = Encoding.UTF8.GetBytes(json);

                stream.Write(data, 0, data.Length);
        }

        private void PrintMessages(List<Message> messages){
            foreach (var message in messages)
            {
                Console.WriteLine($"{message.ChannelId} : {message.TimeCreated} : {message.Data}");
            }
        }
    }
}