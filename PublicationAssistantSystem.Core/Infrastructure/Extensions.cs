using System.IO;
using System.Xml.Serialization;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

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
    }
}
