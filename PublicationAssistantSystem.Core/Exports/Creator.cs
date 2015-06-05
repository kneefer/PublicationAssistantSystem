using System;
using System.Collections.Generic;
using System.Linq;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public abstract class Creator : ICreator
    {
        public string Create(PublicationBaseDTO publication)
        {
            if (publication is BookDTO) return CreateBook((BookDTO)publication);
            if (publication is DatasetDTO) return CreateDataset((DatasetDTO)publication);
            if (publication is ConferencePaperDTO) return CreateConferencePaper((ConferencePaperDTO)publication);
            if (publication is PatentDTO) return CreatePatent((PatentDTO)publication);
            if (publication is TechnicalReportDTO) return CreateTechnicalReport((TechnicalReportDTO)publication);
            if (publication is ThesisDTO) return CreateThesis((ThesisDTO)publication);

            throw new ArgumentException("Argument publication is not valid!");
        }

        public string Create(PublicationBaseDTO publication, string journalTitle, int journalVolume)
        {
            if (publication is ArticleDTO)
                return CreateArticle((ArticleDTO) publication, journalTitle, journalVolume);

            throw new ArgumentException("Argument publication is not valid!");
        }

        public abstract string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume);
        public abstract string CreateBook(BookDTO book);
        public abstract string CreateDataset(DatasetDTO dataset);
        public abstract string CreateConferencePaper(ConferencePaperDTO book);
        public abstract string CreatePatent(PatentDTO book);
        public abstract string CreateTechnicalReport(TechnicalReportDTO book);
        public abstract string CreateThesis(ThesisDTO book);

        #region Helpers

        protected static IList<string> GetAuthors(PublicationBaseDTO publication)
        {
            return publication.Employees
                              .Select(employee => string.Format("{0} {1}", employee.FirstName, employee.LastName))
                              .ToList();
        }

        #endregion Helpers
    }
}