using System.Collections.Generic;

namespace PublicationAssistantSystem.Core.Mappers.Common
{
    public interface IRecord
    {
        /// <summary>
        /// Title of publication
        /// </summary>
        string Title { get; set; }
        
        /// <summary>
        /// Collection of authors
        /// </summary>
        IEnumerable<Author> Authors { get; set; }

        /// <summary>
        /// Date of publication
        /// </summary>
        PublicationDateTime PublicationDate { get; set; }
    }
}
