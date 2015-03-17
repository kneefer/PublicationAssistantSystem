using System.IO;
using System.Xml.Serialization;

namespace PublicationAssistantSystem.Core.Infrastructure
{
    public static class Extensions
    {
        private const string Path = "results.xml";

        public static void SerializeAndSave<T>(this T results)
        {
            using (var file = File.Open(Path, FileMode.CreateNew))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(file, results);
            }
        }
        public static T Deserialize<T>() where T:class
        {
            using (var file = File.Open(Path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(file) as T;
            }
        }
    }
}
