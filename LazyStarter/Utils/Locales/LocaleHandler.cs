using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LazyStarter
{
    class LocaleHandler
    {
        readonly Dictionary<string, Dictionary<string, string>> FTable;

        string[] FLocales;
        internal string[] Locales
        {
            get
            {
                if (FLocales == null) FLocales = LocaleFiles.Keys.ToArray();
                return FLocales;
            }
        }

        private Dictionary<string, string> localeFiles;
        internal Dictionary<string, string> LocaleFiles
        {
            get
            {
                if (localeFiles == null)
                {
                    localeFiles = new Dictionary<string, string>
                    {
                        {LocaleConstants.LOCALE_ENGLISH, "english.csv"},
                        {LocaleConstants.LOCALE_RUSSIAN, "russian.csv"}
                    };
                }
                return localeFiles;
            }
        }


        internal string GetString(string stringId, string locale, params object[] args)
        {
            Dictionary<string, string> locales;
            if (FTable.TryGetValue(locale, out locales))
            {
                // Вернем по локали
                string result;
                if (locales.TryGetValue(stringId, out result))
                {
                    if (args != null && args.Length > 0)
                        result = String.Format(result, args);
                    return result;
                }
            }

            return stringId;
        }


        internal LocaleHandler()
        {
            FTable = new Dictionary<string, Dictionary<string, string>>();
            var langs = ResourceHelper.GetResources(typeof(LocaleHandler)).Where(r => r.Contains(".csv"));
            FillLocales(langs);
        }

        private void FillLocales(IEnumerable<string> files)
        {
            foreach (var lFile in LocaleFiles)
            {
                string fileName = lFile.Value;
                string locale = lFile.Key;

                foreach (var rf in files)
                {
                    var file = rf.Trim();
                    if (file.EndsWith(fileName)) FTable[locale] = ParseResourceFile(file);
                }
            }
        }

        private string[] GetAllLines(string resourceName)
        {
            List<string> lines = new List<string>(1024);
            using (var reader = new StreamReader(ResourceHelper.GetResourceStream(typeof(LocaleHandler), resourceName)))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            return lines.ToArray();
        }

        private Dictionary<string, string> ParseResourceFile(string file)
        {
            var codes = new Dictionary<string, string>();
            try
            {
                var coldeLines = GetAllLines(file);
                foreach (var line in coldeLines)
                {
                    string[] split = line.Trim().Split('=');
                    if (split.Length == 2)
                    {
                        var key = split[0].Trim();
                        var value = split[1].Trim().Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
                        codes[key] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return codes;
        }
    }
}
