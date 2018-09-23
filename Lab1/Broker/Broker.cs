using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Brokers
{
    public class Broker
    {
        private Socket m_mainSocket;
        private Dictionary<int, Channel> channels;

        public Broker()
        {
            Console.WriteLine("Welcome to Broker");

            channels = new Dictionary<int, Channel>();

            var ch = new Channel(10);
            ch.Events.Add(new Message
            {
                ChannelId = 10,
                Data = "Hello",
            });

            channels[10] = ch;
        }
        public void StartListening()
        {
            if (m_mainSocket != null)
            {
                return;
            }

            m_mainSocket = new Socket(AddressFamily.InterNetwork,
                          SocketType.Stream,
                          ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Loopback, 5600);

            m_mainSocket.Bind(ipLocal);
            m_mainSocket.Listen(4);
            Console.WriteLine("Broker is listening at " + ipLocal.Address + " : " + ipLocal.Port);

            while (true)
            {
                try
                {
                    var clientSocket = m_mainSocket.Accept();
                    Task.Run(() => ServeClient(clientSocket));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error :" + e.Message);
                }

            }
        }

        private void ServeClient(Socket clientSocket)
        {
            Console.WriteLine("Broker is connected to "
                       + IPAddress.Parse(((IPEndPoint)clientSocket.RemoteEndPoint).Address.ToString())
                       + "on port number "
                       + ((IPEndPoint)clientSocket.RemoteEndPoint).Port.ToString()
                    );

            var message = ReadMessage(clientSocket);
            if (message != null)
            {
                if (message.IsSubscriber)
                {
                    ServeSubscriber(clientSocket, message);
                }
                else
                {
                    ServePublisher(clientSocket, message);
                }
            }
        }

        private Message ReadMessage(Socket clientSocket)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                do
                {
                    bytes = clientSocket.Receive(data);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (!builder.ToString().EndsWith("ENDMSG"));
                builder.Replace("ENDMSG", "");

                Console.WriteLine("Recieved message " + builder);
                var message = (Message)Newtonsoft.Json.JsonConvert.DeserializeObject(builder.ToString(), typeof(Message));

                return message;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading message\n" + e.Message);
                return null;
            }
        }

        private void ServePublisher(Socket socket, Message message)
        {
            do
            {
                if (!channels.ContainsKey(message.ChannelId))
                {
                    channels[message.ChannelId] = new Channel(message.ChannelId, message);
                }

                channels[message.ChannelId].Events.Add(message);

                SendMessagesToSubscribers(message.ChannelId, new List<Message>() { message });

                message = ReadMessage(socket);
            } while (message != null);
        }

        private void ServeSubscriber(Socket socket, Message message)
        {
            if (!channels.ContainsKey(message.ChannelId))
            {
                channels[message.ChannelId] = new Channel(message.ChannelId);
            }

            channels[message.ChannelId].Subscribers.Add(
                new Subscriber
                {
                    Socket = socket
                });

            SendMessagesToSubscribers(message.ChannelId, channels[message.ChannelId].Events);
        }
        private void SendMessagesToSubscribers(int channelId, List<Message> messages)
        {
            if (channels.ContainsKey(channelId))
            {
                foreach (var subscriber in channels[channelId].Subscribers)
                {
                    SendMessages(subscriber.Socket, messages);
                }
            }
        }

        private void SendMessages(Socket socket, List<Message> messages)
        {
            if (socket.Connected)
            {
                try
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(messages);
                    json += "ENDMSG";
                    byte[] data = Encoding.UTF8.GetBytes(json);

                    socket.Send(data);
                    Console.WriteLine("Messages sent: {0}", json);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error reading message\n" + e.Message);
                }
            }
            else
            {
                Console.WriteLine("User is not connected");
            }
        }
    }
}
