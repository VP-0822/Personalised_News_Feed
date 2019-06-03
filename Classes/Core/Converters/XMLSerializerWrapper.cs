using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Personalised_News_Feed.Core.Converters
{
    public class XMLSerializerWrapper
    {
        public static void WriteXML<T>(T data, string filename)
        {
            FileStream stream;
            try
            {
                XmlSerializer seralizer = new XmlSerializer(typeof(T));
                stream = new FileStream(filename, FileMode.Create);
                seralizer.Serialize(stream, data);
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static T ReadXML<T>(string filename)
        {

            Object returnData = null;
            try
            {
                //string data = "";
                using (StreamReader sr = new StreamReader(filename))
                {
                    //data += sr.ReadLine();
                    XmlSerializer seralizer = new XmlSerializer(typeof(T));
                    returnData = seralizer.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
            return (T)returnData;
        }
    }
}
