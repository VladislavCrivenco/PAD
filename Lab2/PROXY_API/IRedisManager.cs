using System.Net.Http;

namespace PAD
{
    public interface IRedisManager
    {
        HttpClient GetConnection();

        string GetBaseAddress();
    }
}