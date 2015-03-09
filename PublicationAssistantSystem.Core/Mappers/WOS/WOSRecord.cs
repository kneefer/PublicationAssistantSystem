using System.Collections.Generic;
using PublicationAssistantSystem.Core.Mappers.Common;

namespace PublicationAssistantSystem.Core.Mappers.WOS
{
    public class WOSRecord : IRecord
    {
        #region IRecord
        public string Title { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public PublicationDateTime PublicationDate { get; set; }
        #endregion IRecord
    }
}
