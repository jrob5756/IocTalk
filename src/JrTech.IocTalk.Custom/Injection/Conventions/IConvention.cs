using System;

namespace JrTech.IocTalk.Custom.Injection.Conventions
{
    public interface IConvention
    {
        string ScanDirectory { get; set; }

        Func<string, bool> AssemblyFilter { get; set; }

        Func<Type, Type> MappingConvention { get; set; }

        Func<Type, bool> TypeFilter { get; set; }

        bool IsInjectable(Type type);
    }
}