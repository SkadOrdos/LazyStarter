using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    public class ResourceHelper
    {
        public static string[] GetResourceNames()
        {
            return typeof(ResourceHelper).Assembly.GetManifestResourceNames();
        }
        
        public static string GetResourceName(string resourceName)
        {
            var allRes = GetResourceNames();
            var resource = allRes.FirstOrDefault(line => line.Contains(resourceName));
            return resource;
        }

        public static Stream GetResourceStream(string resourceName)
        {
            var allRes = GetResourceNames();
            var resource = allRes.FirstOrDefault(line => line.Contains(resourceName));
            return typeof(ResourceHelper).Assembly.GetManifestResourceStream(resource);
        }
        
        private static Stream GetStream(Assembly assembly, string resourceName)
        {
            if (!String.IsNullOrWhiteSpace(resourceName))
            {
                var resNames = assembly.GetManifestResourceNames();
                foreach (string name in resNames)
                    if (String.CompareOrdinal(resourceName, name) == 0)
                        return assembly.GetManifestResourceStream(name);

                foreach (string name in resNames)
                    if (name.Contains(resourceName))
                        return assembly.GetManifestResourceStream(name);
            }

            return new MemoryStream(new byte[0], false);
        }

        public static Stream GetStream(string ResourceName)
        {
            return GetStream(Assembly.GetCallingAssembly(), ResourceName);
        }
        
        public static Stream GetResourceStream(Type assembly, string resourceName)
        {
            return Assembly.GetAssembly(assembly).GetManifestResourceStream(resourceName);
        }

        public static string[] GetResources(Type assembly)
        {
            return Assembly.GetAssembly(assembly).GetManifestResourceNames();
        }
    }
}
