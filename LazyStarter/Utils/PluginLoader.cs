using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    public class PluginLoader
    {
        public static void Init()
        {
            LocalePlugin.Init();
        }
    }
}
