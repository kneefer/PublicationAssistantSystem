using PublicationAssistantSystem.DAL.Models.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicationAssistantSystem.DAL.Models.Publications
{
    public class Article : PublicationBase
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }

        [ForeignKey("JournalEdition")]
        public int JournalEditionId { get; set; }
        [Required]
        public JournalEdition JournalEdition { get; set; }
    }

    public class Book : PublicationBase
    {
        [Required]
        public string ISBN { get; set; }

        public string Publisher { get; set; }
    }

    public class Dataset : PublicationBase
    {

    }

    public class ConferencePaper : PublicationBase
    {

    }

    public class Patent : PublicationBase
    {

    }

    public class TechnicalReport : PublicationBase
    {

    }

    public class Thesis : PublicationBase
    {

    }
}