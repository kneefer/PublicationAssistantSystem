using PublicationAssistantSystem.DAL.Models.Misc;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.Publications
{
    public class Article : PublicationBase
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        [Required]
        public JournalEdition Journal { get; set; }
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
