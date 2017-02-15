using System;
using System.Linq;
using JrTech.IocTalk.Custom.Injection.Conventions;
using JrTech.IocTalk.Custom.Injection.Util;

namespace JrTech.IocTalk.Custom.Injection
{
    public static class ContainerRegistrar
    {
        public static void Register(Action<Type, Type> registerAction)
        {
            Register(registerAction, new DefaultConvention());
        }

        public static void Register(Action<Type, Type> registerAction, Action<IConvention> setup)
        {
            var convention = new DefaultConvention();
            setup.Invoke(convention);

            Register(registerAction, convention);
        }

        public static void Register(Action<Type, Type> registerAction, IConvention convention)
        {
            RegisterTypes(registerAction, convention);
        }
        
        private static void RegisterTypes(Action<Type, Type> registerAction, IConvention config)
        {
            var assemblies = AssemblyFinder.Find(config.ScanDirectory, config.AssemblyFilter);
            var types = assemblies.SelectMany(a => a.GetTypes()).Where(config.TypeFilter);
            foreach (var implementationType in types)
            {
                registerAction.Invoke(implementationType, implementationType);
                var serviceType = config.MappingConvention(implementationType);
                if (serviceType != null)
                {
                    registerAction.Invoke(serviceType, implementationType);
                }
            }
        }
    }
}
