using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyStarter
{
    class LocaleConstants
    {
        public const string LOCALE_NEUTRAL = LOCALE_ENGLISH;

        public const string LOCALE_ENGLISH = "English";
        public const string LOCALE_ENGLISH_SHORTLOWER = "en";
        public const string LOCALE_RUSSIAN = "Русский";
        public const string LOCALE_RUSSIAN_SHORTLOWER = "ru";
        
        public static string GetLocaleLowerName(string locale)
        {
            if (locale == LOCALE_RUSSIAN) return LOCALE_RUSSIAN_SHORTLOWER;
            return LOCALE_ENGLISH_SHORTLOWER;
        }


        public const string ACTION_ADDPROGRAM = "ACTION_ADDPROGRAM";
        public const string ACTION_EDITPROGRAM = "ACTION_EDITPROGRAM";
        public const string ACTION_RUNPROGRAM = "ACTION_RUNPROGRAM";
        public const string ACTION_REMOVEPROGRAM = "ACTION_REMOVEPROGRAM";
        public const string LABEL_ADDITEMS = "LABEL_ADDITEMS";
    }
}
