﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SERVER_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://localhost:" + args[0])
            .ConfigureAppConfiguration((hostingContext, config) =>

                { 

                config.SetBasePath(Directory.GetCurrentDirectory());

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                config.AddJsonFile($"appsettings.Development.json", optional: true);

                config.AddEnvironmentVariables();

                })
            .UseStartup<Startup>();
    }
}