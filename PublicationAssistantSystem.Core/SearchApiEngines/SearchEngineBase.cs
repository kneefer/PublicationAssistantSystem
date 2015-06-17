using System;
using System.Collections.Generic;
using PublicationAssistantSystem.Core.Mappers.Common;

namespace PublicationAssistantSystem.Core.SearchApiEngines
{
    public abstract class SearchEngineBase
    {
        //public IEnumerable<IRecord> GetResults()
        //{
        //    if (IsReadyToQuery)
        //        return RunQuery();
            
        //    throw new ArgumentException("Engine is not ready to search.");
        //}

        //protected abstract bool IsReadyToQuery { get; }
        //protected abstract IEnumerable<IRecord> RunQuery();

        //public abstract SearchEngineBase ByTitle(string title);
        //public abstract SearchEngineBase ByISBNISSN(string isbnOrIssn);
        //public abstract SearchEngineBase ByAuthors(string[] authorsSecondNames);
    }
}
