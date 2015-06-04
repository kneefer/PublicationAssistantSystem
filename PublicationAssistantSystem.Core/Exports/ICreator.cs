using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public interface ICreator
    {
        string Create(PublicationBaseDTO publication);
        string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume);
        string CreateBook(BookDTO book);
        string CreateDataset(DatasetDTO dataset);
        string CreateConferencePaper(ConferencePaperDTO book);
        string CreatePatent(PatentDTO book);
        string CreateTechnicalReport(TechnicalReportDTO book);
        string CreateThesis(ThesisDTO book);
    }
}