using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class JournalEdition
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public int VolumeNumber { get; set; }
        [ForeignKey("Journal")]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}