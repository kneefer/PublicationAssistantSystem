using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    public class ArticleDTO : PublicationBaseDTO
    {
        public ArticleDTO() { }

        public ArticleDTO(Article article)
            : base(article)
        {
            PageFrom = article.PageFrom;
            PageTo = article.PageTo;
            JournalEditionId = article.Journal.Id;
        }

        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        [Required]
        public int JournalEditionId { get; set; }
    }

    public class BookDTO : PublicationBaseDTO
    {
        public BookDTO() { }

        public BookDTO(Book book)
            : base(book)
        {
            ISBN = book.ISBN;
            Publisher = book.Publisher;
        }
        
        [Required]
        public string ISBN { get; set; }
        public string Publisher { get; set; }
    }

    public class DatasetDTO : PublicationBaseDTO
    {
        public DatasetDTO() { }

        public DatasetDTO(Dataset dataset)
            : base(dataset)
        {
            
        }
    }

    public class ConferencePaperDTO : PublicationBaseDTO
    {
        public ConferencePaperDTO() { }

        public ConferencePaperDTO(ConferencePaper conferencePaper)
            : base(conferencePaper)
        {
            
        }
    }

    public class PatentDTO : PublicationBaseDTO
    {
        public PatentDTO() { }

        public PatentDTO(Patent patent)
            : base(patent)
        {
            
        }
    }

    public class TechnicalReportDTO : PublicationBaseDTO
    {
        public TechnicalReportDTO() { }

        public TechnicalReportDTO(TechnicalReport technicalReport)
            : base(technicalReport)
        {
            
        }
    }

    public class ThesisDTO : PublicationBaseDTO
    {
        public ThesisDTO() { }

        public ThesisDTO(Thesis thesis)
            : base (thesis)
        {
            
        }
    }
}