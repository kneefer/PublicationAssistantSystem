using PublicationAssistantSystem.Core.Mappers.Common;
using System.Collections.Generic;

namespace PublicationAssistantSystem.Core.Mappers.MNISW
{
    public class MNISWRecord : IRecord
    {
        #region IRecord
        public string Title { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public PublicationDateTime PublicationDate { get; set; }
        #endregion IRecord
    }
}
