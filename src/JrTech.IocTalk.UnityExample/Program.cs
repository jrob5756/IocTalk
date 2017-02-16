using System;
using System.Diagnostics;
using JrTech.IocTalk.StandardLibrary;
using JrTech.IocTalk.StandardLibrary.Utilities.Logging;
using Microsoft.Practices.Unity;

namespace JrTech.IocTalk.UnityExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Register
            var container = new UnityContainer();
            container.RegisterTypes(
                AllClasses.FromAssembliesInBasePath(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);
            container.RegisterType<ILogger, ConsoleLogger>();

            // Resolve
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 100000; i++)
            {
                var application = container.Resolve<Application>();
            }

            sw.Stop();

            // Observe
            Console.WriteLine($"Unity - {sw.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
