using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RTMS_MVVM
{
    public static class clsUtil
    {
        public static void SaveData<T>(string fileName, T dataToSave)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                serializer.Serialize(textWriter, dataToSave);
                textWriter.Close();
            }
        }

        public static T ReadData<T>(string fileName)
        {
            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(fileName))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            return default(T);
        }
    }
}
