using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class Journal : IComputableEntity
    {
        public int Id { get; set; }

        [Required]
        public string ISSN { get; set; }
        public string eISSN { get; set; }

        [Required]
        public string Title { get; set; }


        public bool IsComputing { get; set; }
        public bool IsOnMNISZW { get; set; }
        public bool IsOnWOS { get; set; }
        public bool IsOnJCR { get; set; }

        public virtual ICollection<JournalEdition> JournalEditions { get; set; }
    }
}