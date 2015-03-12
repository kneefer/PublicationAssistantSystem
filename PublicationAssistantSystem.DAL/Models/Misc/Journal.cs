using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class Journal
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual ICollection<JournalEdition> JournalEditions { get; set; }
    }
}