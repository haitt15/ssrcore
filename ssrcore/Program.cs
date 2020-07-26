using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using ssrcore.Helpers;
using ssrcore.Services.BackgroundServices;
using Utils = ssrcore.Helpers.Utils;

namespace ssrcore
{
    public class Program
    {
      
        public static void Main(string[] args)
        {

            RequestSheetUtils.Init();

            CreateHostBuilder(args).Build().Run();
        }

        

      

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<StatusManagerService>();
                    services.AddHostedService<JsonInfomationService>();
                    services.AddHostedService<GoogleSheetApiService>();
                });
    }
}
