using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JrTech.IocTalk.Library;
using JrTech.IocTalk.Library.Utilities.Logging;
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
