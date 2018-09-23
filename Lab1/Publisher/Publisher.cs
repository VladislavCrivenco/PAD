using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Publishers
{
    public class Publisher
    {
        private TcpClient m_client;
        public Publisher()
        {
            Connect();
        }

        private void Connect()
        {
            try
            {
                m_client = new TcpClient();
                m_client.Connect(IPAddress.Loopback, 5600);
                Console.WriteLine("You are connected to broker");
                Console.WriteLine("Type any channel and message");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting\n" + e.Message);
                return;
            }

            while (true)
            {
                var line = Console.ReadLine();
                SendData(new Message
                {
                    ChannelId = 10,
                    Data = line,
                    IsSubscriber = false
                });
            }
        }

        public void SendData(Message message)
        {
            try
            {
                NetworkStream stream = m_client.GetStream();

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                json += "ENDMSG";
                byte[] data = Encoding.UTF8.GetBytes(json);

                stream.Write(data, 0, data.Length);
                Console.WriteLine("Message sent: {0}", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error sending message\n" + e.Message);
            }

            //GetData(stream);
        }

        private ResponseMessage GetData(NetworkStream stream)
        {
            try
            {
                var response = new StringBuilder();
                byte[] responseBytes = new byte[2046];
                do
                {
                    var bytes = stream.Read(responseBytes, 0, responseBytes.Length);
                    response.Append(Encoding.UTF8.GetString(responseBytes, 0, bytes));
                } while (!response.ToString().EndsWith("ENDMSG"));

                response = response.Replace("ENDMSG", "");

                var result = (ResponseMessage)Newtonsoft.Json.JsonConvert.DeserializeObject(response.ToString());
                return result;
            }
            catch (Exception e)
            {
                return new ResponseMessage
                {
                    Status = ResponseMessage.MessageStatus.NetworkError,
                    Message = e.Message
                };
            }
        }
    }
}
