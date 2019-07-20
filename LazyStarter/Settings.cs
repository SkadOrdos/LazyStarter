using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LazyStarter
{
    [Serializable]
    public class Settings
    {
        public Settings()
        {
            WokrbookFile = String.Empty;
            ProcessedSheetCount = 1;
            OutFileExtension = "csv";
            OutFileSeparator = "=";
        }


        public string WokrbookFile { get; set; }

        public int ProcessedSheetCount { get; set; }

        public string OutFileExtension { get; set; }

        public string OutFileSeparator { get; set; }
        

        public static void SaveToXml(String fileName, Settings serializableObject)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                serializer.Serialize(textWriter, serializableObject);
            }
        }

        public static Settings LoadFromXml(String fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            using (TextReader textReader = new StreamReader(fileName))
            {
                return (Settings)serializer.Deserialize(textReader);
            }
        }
    }
}
