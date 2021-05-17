using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
               
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            //database
           
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configBuilder)
                     .WriteTo
                     .MSSqlServer(
                         connectionString: @"Server=DESKTOP-QO6CDJ4\SQLEXPRESS; Database=SerilogDatabase; Integrated Security=True;",
                         sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs" })
                    


           // Email
          
                .WriteTo.Email(new EmailConnectionInfo
                {
                    FromEmail = "demo1@gmail.com",
                    ToEmail = "demo2@gmail.com",
                    MailServer = "smtp.gmail.com",
                    NetworkCredentials = new NetworkCredential
                    {
                        UserName = "demo1@gmail.com",
                        Password = "xyz123"
                    },
                    EnableSsl = true,
                    Port = 465,
                    EmailSubject = "ERROR!"
                },
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    batchPostingLimit: 10
                    , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                )
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

