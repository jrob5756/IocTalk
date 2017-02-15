using System;
using System.IO;
using System.Linq;

namespace JrTech.IocTalk.Custom.Injection.Conventions
{
    public sealed class DefaultConvention : IConvention
    {
        public DefaultConvention()
        {
            ScanDirectory = GetScanDirectory();
            AssemblyFilter = GetAssemblyFilter();
            TypeFilter = GetTypeFilter();
            MappingConvention = GetMappingConvention();
        }

        public string ScanDirectory { get; set; }

        public Func<string, bool> AssemblyFilter { get; set; }

        public Func<Type, Type> MappingConvention { get; set; }

        public Func<Type, bool> TypeFilter { get; set; }

        public bool IsInjectable(Type type)
        {
            return type.IsPublic &&
                   type.IsClass &&
                   type.IsGenericType == false &&
                   type.IsAbstract == false &&
                   type.GetConstructors().Length <= 1;
        }

        private static string GetScanDirectory()
        {
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var binPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;

            if (Path.IsPathRooted(binPath))
                return binPath;

            var path = binPath?.Split(';').FirstOrDefault();
            return Path.Combine(assemblyPath, path ?? string.Empty);
        }

        private static Func<string, bool> GetAssemblyFilter()
        {
            return f =>
                   {
                       var fileName = Path.GetFileName(f);
                       return fileName != null && (fileName.StartsWith("JrTech.") && (fileName.EndsWith(".dll") || fileName.EndsWith(".exe")));
                   };
        }

        private static Func<Type, Type> GetMappingConvention()
        {
            return t => t
                .GetInterfaces()
                .SingleOrDefault(
                    i => i.Name.Equals($"I{t.Name}", StringComparison.OrdinalIgnoreCase));
        }

        private Func<Type, bool> GetTypeFilter()
        {
            return t => IsInjectable(t) && t.FullName.StartsWith("JrTech.");
        }
    }
}
