using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    public class ArticleDTO : PublicationBaseDTO
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        [Required]
        public int JournalEditionId { get; set; }
    }

    public class BookDTO : PublicationBaseDTO
    {
        [Required]
        public string ISBN { get; set; }
        public string Publisher { get; set; }
    }

    public class DatasetDTO : PublicationBaseDTO
    {

    }

    public class ConferencePaperDTO : PublicationBaseDTO
    {

    }

    public class PatentDTO : PublicationBaseDTO
    {

    }

    public class TechnicalReportDTO : PublicationBaseDTO
    {

    }

    public class ThesisDTO : PublicationBaseDTO
    {

    }
}