using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class XMLCreator : Creator
    {
        public override string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume)
        {
            throw new System.NotImplementedException();
        }

        public override string CreateBook(BookDTO book)
        {
            throw new System.NotImplementedException();
        }

        public override string CreateDataset(DatasetDTO dataset)
        {
            throw new System.NotImplementedException();
        }

        public override string CreateConferencePaper(ConferencePaperDTO book)
        {
            throw new System.NotImplementedException();
        }

        public override string CreatePatent(PatentDTO book)
        {
            throw new System.NotImplementedException();
        }

        public override string CreateTechnicalReport(TechnicalReportDTO book)
        {
            throw new System.NotImplementedException();
        }

        public override string CreateThesis(ThesisDTO book)
        {
            throw new System.NotImplementedException();
        }
    }
}