using System.Net.Sockets;
using System.Net;

namespace Brokers
{
    public static class SocketExtensions{
        public static string GetInfo(this Socket socket){
            return IPAddress.Parse(((IPEndPoint)socket.RemoteEndPoint).Address.ToString())
            + ":"
            + ((IPEndPoint)socket.RemoteEndPoint).Port.ToString();
        }
    }
    
}