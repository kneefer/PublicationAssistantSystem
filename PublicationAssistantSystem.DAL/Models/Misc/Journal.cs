using System.Collections.Generic;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<JournalEdition> JournalEditions { get; set; }
    }
}