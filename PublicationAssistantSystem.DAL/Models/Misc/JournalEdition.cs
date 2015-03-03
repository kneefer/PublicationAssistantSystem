using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class JournalEdition
    {
        [Key]
        public string ISSN { get; set; }

        public Journal Journal { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}