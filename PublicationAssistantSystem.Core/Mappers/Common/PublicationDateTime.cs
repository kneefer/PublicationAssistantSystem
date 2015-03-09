using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            Year = int.Parse(r.source.Where(x => x.label == PublicationYearLabel).First().value[0]);
            try
            {
                ExtraPart = r.source.Where(x => x.label == PublicationDateLabel).First().value[0];
            }
            catch { }
        }
    }
}
