using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System.Net.Http;
using System.Diagnostics;

namespace PROXY_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {

            try
            {
                var httpClient = new HttpClient();
                var d = await httpClient.GetStringAsync("https://google.com/");

                Debug.WriteLine(d);
            }
            catch (Exception e){

            }

            });


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("https://localhost:5000")
            .UseStartup<Startup>();
    }
}
