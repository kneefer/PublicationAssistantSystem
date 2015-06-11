using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class XMLCreator : Creator
    {
        protected override string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateBook(BookDTO book)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateDataset(DatasetDTO dataset)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateConferencePaper(ConferencePaperDTO book)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreatePatent(PatentDTO book)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateTechnicalReport(TechnicalReportDTO book)
        {
            throw new System.NotImplementedException();
        }

        protected override string CreateThesis(ThesisDTO book)
        {
            throw new System.NotImplementedException();
        }
    }
}