using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace PublicationAssistantSystem.Core.Mappers.Common
{
    interface IRecordConverter
    {
        /// <summary>
        /// Converts search results into set of IRecords
        /// </summary>
        /// <param name="xmlFileName">Name of XML file</param>
        /// <returns>Set of deserialized IRecords</returns>
        IEnumerable<IRecord> ToIRecord(searchResults results);
    }
}
