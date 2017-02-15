using System;
using System.Diagnostics;
using Autofac;
using JrTech.IocTalk.Library;

namespace JrTech.IocTalk.AutofacExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Register
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof (Application).Assembly)
                   .AsSelf()
                   .AsImplementedInterfaces();
            var container = builder.Build();

            // Resolve
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 100000; i++)
            {
                var application =  container.Resolve<Application>();
            }

            sw.Stop();

            // Observe
            Console.WriteLine($"StructureMap - {sw.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
