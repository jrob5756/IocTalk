using Autofac;
using DryIoc;
using JrTech.IocTalk.Custom.Injection;
using JrTech.IocTalk.Library.Utilities.Logging;
using Microsoft.Practices.Unity;

namespace JrTech.IocTalk.Custom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dryIoc = new DryIoc.Container();
            ContainerRegistrar.Register((x, y) => dryIoc.Register(x, y));
            dryIoc.Register<ILogger, ConsoleLogger>();

            var unity = new UnityContainer();
            ContainerRegistrar.Register((x, y) => unity.RegisterType(x, y));
            unity.RegisterType<ILogger, ConsoleLogger>();

            var structureMap = new StructureMap.Container(c => {
                ContainerRegistrar.Register((x, y) => c.For(x).Use(y));
                c.For<ILogger>().Use<ConsoleLogger>();
            });

            var autofac = new Autofac.ContainerBuilder();
            ContainerRegistrar.Register((x, y) => autofac.RegisterType(x).As(y));
            autofac.RegisterType<ILogger>().As<ConsoleLogger>();
        }
    }
}
