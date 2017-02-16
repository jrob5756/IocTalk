using System;
using System.Diagnostics;
using JrTech.IocTalk.StandardLibrary;
using JrTech.IocTalk.StandardLibrary.Utilities;
using JrTech.IocTalk.StandardLibrary.Utilities.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace JrTech.IocTalk.AspNetCoreExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Register
            var serviceProvider = 
                new ServiceCollection()
                    .AddTransient<ICalculator, Calculator>()
                    .AddTransient<ILogger, ConsoleLogger>()
                    .BuildServiceProvider();
            
            // Resolve
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 100000; i++)
            {
                var application = serviceProvider.GetService<Application>();
            }

            sw.Stop();

            // Observe
            Console.WriteLine($".Net Core - {sw.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
