using System.Collections.Generic;
using PublicationAssistantSystem.Core.Mappers.Common;

namespace PublicationAssistantSystem.Core.SearchApiEngines
{
    public class MniszwSearchEngine : SearchEngineBase
    {
        protected override bool IsReadyToQuery
        {
            get { throw new System.NotImplementedException(); }
        }

        protected override IEnumerable<IRecord> RunQuery()
        {
            throw new System.NotImplementedException();
        }

        public override SearchEngineBase ByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public override SearchEngineBase ByISBNISSN(string isbnOrIssn)
        {
            throw new System.NotImplementedException();
        }

        public override SearchEngineBase ByAuthors(string[] authorsSecondNames)
        {
            throw new System.NotImplementedException();
        }
    }
}
