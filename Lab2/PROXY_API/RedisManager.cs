using System;
using System.Collections.Generic;
using System.Net.Http;
using PAD;
using System.Net.Http.Headers;

namespace PAD
{
    public class RedisManager : IRedisManager
    {
        private HttpClient _client;
        private Random _random;
        public RedisManager()
        {
            _random = new Random(42);
            _client = new HttpClient();
            //_clients.Add(CreateClient(5001));
            ///_clients.Add(CreateClient(5002));
        }

        private HttpClient CreateClient(int port)
        {
            return new HttpClient();
        }

        public HttpClient GetConnection()
        {
            return _client;
        }

        public string GetBaseAddress(){
            return $"https://facebook.com";
        }
    }
}
