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
                Console.WriteLine("You are connected to broker. Introduce channel id");
                Console.WriteLine("Type any channel and message");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting\n" + e.Message);
                return;
            }

            while (true)
            {
                try {
                    var line = Console.ReadLine();

                    if (!line.Contains(":")){
                        throw new Exception("Invalid format");
                    }

                    string channelId = line.Split(":")[0].Trim();
                    string message = line.Split(":")[1].Trim();

                    SendData(new Message(message, channelId));

                }catch(Exception e){
                    Console.WriteLine(e.Message + " \t channelId : message");
                }
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
                //Console.WriteLine("Message sent: {0}", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error sending message\n" + e.Message);
            }

        }
    }
}
