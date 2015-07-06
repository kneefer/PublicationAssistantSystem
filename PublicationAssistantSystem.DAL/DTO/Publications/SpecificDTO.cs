using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    [XmlType("Article")]
    public class ArticleDTO : PublicationBaseDTO
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }

        [Required]
        public int JournalEditionId { get; set; }

        public JournalDTO Journal { get; set; }
        public JournalEditionDTO JournalEdition { get; set; }
    }

    [XmlType("Book")]
    public class BookDTO : PublicationBaseDTO
    {
        [Required]
        public string ISBN { get; set; }

        public string Publisher { get; set; }
    }

    [XmlType("Dataset")]
    public class DatasetDTO : PublicationBaseDTO
    {

    }

    [XmlType("ConferencePaper")]
    public class ConferencePaperDTO : PublicationBaseDTO
    {

    }

    [XmlType("Patent")]
    public class PatentDTO : PublicationBaseDTO
    {

    }

    [XmlType("TechnicalReport")]
    public class TechnicalReportDTO : PublicationBaseDTO
    {

    }

    [XmlType("Thesis")]
    public class ThesisDTO : PublicationBaseDTO
    {

    }
}