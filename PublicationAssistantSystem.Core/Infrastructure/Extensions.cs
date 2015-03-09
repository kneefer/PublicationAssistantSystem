using System.IO;
using System.Xml.Serialization;

namespace PublicationAssistantSystem.Core.Infrastructure
{
    public static class Extensions
    {
        public static void SerializeAndSave<T>(this T results)
        {
            using (var file = File.Open("results.xml", FileMode.CreateNew))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(file, results);
            }
        }
        public static T Deserialize<T>() where T:class
        {
            using (var file = File.Open("results.xml", FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(file) as T;
            }
        }
    }
}
