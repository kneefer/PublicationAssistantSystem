using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Misc
{
    public class JournalDTO : JournalPostDTO
    {
        public JournalDTO() { }

        public JournalDTO(Journal journal) 
            : base(journal)
        {
            Id         = journal.Id;
            IsOnMNISZW = journal.IsOnMNISZW;
            IsOnWOS    = journal.IsOnWOS;
            IsOnJCR    = journal.IsOnJCR;
        }

        public int Id { get; set; }
        public bool IsOnMNISZW { get; set; }
        public bool IsOnWOS { get; set; }
        public bool IsOnJCR { get; set; }
    }

    public class JournalPostDTO
    {
        protected JournalPostDTO() { }

        public JournalPostDTO(Journal journal)
        {
            ISSN = journal.ISSN;
            eISSN = journal.ISSN;
            Title = journal.Title;
        }

        [Required]
        public string ISSN { get; set; }
        public string eISSN { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
