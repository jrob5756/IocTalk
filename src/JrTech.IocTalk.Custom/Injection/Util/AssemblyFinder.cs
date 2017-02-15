using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JrTech.IocTalk.Custom.Injection.Util
{
    /// <summary>
    /// The assembly finder class.
    /// </summary>
    internal static class AssemblyFinder
    {
        /// <summary>
        /// Finds assemblies from the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filter">The filter.</param>
        /// <exception cref="FileLoadException">Could not load the file.</exception>
        internal static IEnumerable<Assembly> Find(string path, Func<string, bool> filter)
        {
            var dllFiles = Directory.EnumerateFiles(path).Where(filter);
            foreach (var file in dllFiles)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                Assembly assembly;

                try
                {
                    assembly = AppDomain.CurrentDomain.Load(name);
                }
                catch (Exception)
                {
                    try
                    {
                        assembly = Assembly.LoadFrom(file);
                    }
                    catch (Exception ex)
                    {
                        throw new FileLoadException("Could not load the file.", file, ex);
                    }
                }

                if (assembly != null)
                {
                    yield return assembly;
                }
            }
        }
    }
}