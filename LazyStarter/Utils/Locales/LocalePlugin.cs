using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    class LocalePlugin
    {
        static LocaleHandler FHandler;
        private static string lang = LocaleConstants.LOCALE_ENGLISH;

        public static void Init()
        {
            FHandler = new LocaleHandler();
        }

        public static string GetString(string stringId, params object[] args)
        {
            return FHandler.GetString(stringId, lang, args);
        }
    }
}
