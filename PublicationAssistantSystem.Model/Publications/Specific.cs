using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.Model.Misc;

namespace PublicationAssistantSystem.Model.Publications
{
    public class Article : PublicationBase
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public JournalEdition Journal { get; set; }
    }

    public class Book : PublicationBase
    {
        [Key]
        public string ISBN { get; set; }

        public string Publisher { get; set; }
    }

    public class Chapter : PublicationBase
    {
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
