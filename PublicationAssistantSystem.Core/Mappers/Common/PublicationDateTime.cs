using System.Linq;

namespace PublicationAssistantSystem.Core.Mappers.Common
{
    public class PublicationDateTime
    {
        private const string PublicationYearLabel = "Published.BiblioYear";
        private const string PublicationDateLabel = "Published.BiblioDate";

        public int Year { get; private set; }
        public string ExtraPart { get; private set; }

        public PublicationDateTime(WebOfKnowledgeApi.Search.liteRecord r)
        {
            Year = int.Parse(r.source.First(x => x.label == PublicationYearLabel).value[0]);
            try
            {
                ExtraPart = r.source.First(x => x.label == PublicationDateLabel).value[0];
            }
            catch { }
        }
    }
}
