using System.Collections.Generic;
using System.Linq;
using PublicationAssistantSystem.Core.Mappers.Common;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.Core.Mappers.WOS
{
    public class WOSRecordToIRecordConverter : IRecordConverter
    {
        public IEnumerable<IRecord> ToIRecord(searchResults results)
        {
            var records = new List<WOSRecord>();

            if(results.records == null) return records;

            foreach (var r in results.records)
            {
                records.Add(new WOSRecord
                {
                    Title = r.title.First().value[0],
                    Authors = r.authors.SelectMany(x => x.value.Select(y =>
                    {
                        var s = y.Split(new[] { ',' });
                        return new Author
                        {
                            FirstName = s[1],
                            SecondName = s[0]
                        };
                    })).ToList(),
                    PublicationDate = new PublicationDateTime(r),
                    });
            }
            return records;
        }
    }
}
