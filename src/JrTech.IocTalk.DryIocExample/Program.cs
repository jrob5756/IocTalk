using System;
using System.Diagnostics;
using DryIoc;
using JrTech.IocTalk.Library;
using JrTech.IocTalk.Library.Utilities.Logging;

namespace JrTech.IocTalk.DryIocExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Register
            var container = new Container();
            container.RegisterMany(
                new [] { typeof(Application).Assembly },
                action: (registrator, serviceTypes, implType) =>
                {
                    registrator.RegisterMany(serviceTypes, implType, Reuse.Transient);
                });
            container.Register<ILogger, ConsoleLogger>(ifAlreadyRegistered: IfAlreadyRegistered.Replace);

            // Resolve
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 100000; i++)
            {
                var application = container.Resolve<Application>();
            }

            sw.Stop();

            // Observe
            Console.WriteLine($"DryIoc - {sw.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
