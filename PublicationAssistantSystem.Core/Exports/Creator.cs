using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public abstract class Creator
    {
        #region Properties
        
        protected Stream Stream { get; set; }

        #endregion Properties

        #region Initialization

        protected Creator()
            : this(Stream.Null) 
        { }

        protected Creator(Stream stream)
        {
            Stream = stream;
        }

        #endregion Initialization

        public virtual bool CreateToStream(IEnumerable<PublicationBaseDTO> publications)
        {
            if(Stream == Stream.Null)
                throw new ArgumentException("Stream must be initialized!");
            if(publications == null)
                throw new ArgumentNullException("publications");

            var encoding = new UTF8Encoding();
            foreach (var publication in publications)
            {
                var serialized = Create(publication);
                var bytes = encoding.GetBytes(serialized);
                Stream.Write(bytes, 0, bytes.Length);
            }

            return true;
        }

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

        protected abstract string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume);
        protected abstract string CreateBook(BookDTO book);
        protected abstract string CreateDataset(DatasetDTO dataset);
        protected abstract string CreateConferencePaper(ConferencePaperDTO book);
        protected abstract string CreatePatent(PatentDTO book);
        protected abstract string CreateTechnicalReport(TechnicalReportDTO book);
        protected abstract string CreateThesis(ThesisDTO book);

        #region Helpers

        protected static IList<string> GetAuthors(PublicationBaseDTO publication)
        {
            return publication.Employees
                              .Select(employee => string.Format("{0} {1}", employee.FirstName, employee.LastName))
                              .ToList();
        }
        
        protected static IList<string> GetAuthorsLastNames(PublicationBaseDTO publication)
        {
            return publication.Employees
                              .Select(employee => employee.LastName)
                              .ToList();
        }

        #endregion Helpers
    }
}