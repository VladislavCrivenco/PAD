using System;
using System.Collections.Generic;
using System.Net.Http;
using PAD;

namespace PAD
{
    public class RedisManager : IRedisManager
    {
        private List<HttpClient> _clients;
        private Random _random;
        public RedisManager()
        {
            _random = new Random(42);
            _clients = new List<HttpClient>();
            _clients.Add(CreateClient(5001));
            _clients.Add(CreateClient(5002));
            _clients.Add(CreateClient(5003));
        }

        private HttpClient CreateClient(int port)
        {
            return new HttpClient{
                BaseAddress = new Uri($"http://localhost:{port}")
            };
        }

        public HttpClient GetConnection()
        {
            return _clients[_random.Next(_clients.Count)];
        }
    }
}
