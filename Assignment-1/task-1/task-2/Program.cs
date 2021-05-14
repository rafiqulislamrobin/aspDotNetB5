using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TASK_1
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var configBuilder = new
            ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();


            
            Log.Logger = new LoggerConfiguration()
                 .WriteTo
                 .MSSqlServer(
                     connectionString: @"Server=DESKTOP-QO6CDJ4\SQLEXPRESS; Database=SeriDB; Integrated Security=SSPI;",
                     sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" })
                 .CreateLogger();


            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

          public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
              .UseSerilog()
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
                 webBuilder.UseUrls("http://*:80");
              });

    }
}
