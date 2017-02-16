using System;
using System.Diagnostics;
using System.Net.Mime;
using JrTech.IocTalk.StandardLibrary;
using JrTech.IocTalk.StandardLibrary.Utilities.Logging;
using StructureMap;

namespace JrTech.IocTalk.StructureMapExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Register
            var container = new Container(c =>
            {
                c.Scan(a =>
                {
                    a.AssemblyContainingType<Application>();
                    a.WithDefaultConventions();
                });

                c.For<ILogger>().Use<ConsoleLogger>();
            });

            // Resolve
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < 100000; i++)
            {
                var application = container.GetInstance<Application>();
            }

            sw.Stop();

            // Observe
            Console.WriteLine($"StructureMap - {sw.ElapsedMilliseconds}");
            Console.Read();
        }
    }
}
