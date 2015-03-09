using PublicationAssistantSystem.Core.Mappers.Common;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationAssistantSystem.Core.Mappers.Common
{
    public class MNISWRecordToIRecordConverter : IRecordConverter
    {
        public IEnumerable<IRecord> ToIRecord(searchResults results)
        {
            throw new NotImplementedException();
        }
    }
}
