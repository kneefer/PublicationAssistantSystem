using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PublicationAssistantSystem.DAL.Models.Publications;

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

        public virtual void CreateToStream(IEnumerable<PublicationBase> publications)
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
        }

        public string Create(PublicationBase publication)
        {
            if (publication is Book) return CreateBook((Book)publication);
            if (publication is Dataset) return CreateDataset((Dataset)publication);
            if (publication is ConferencePaper) return CreateConferencePaper((ConferencePaper)publication);
            if (publication is Patent) return CreatePatent((Patent)publication);
            if (publication is TechnicalReport) return CreateTechnicalReport((TechnicalReport)publication);
            if (publication is Thesis) return CreateThesis((Thesis)publication);
            if (publication is Article) return CreateArticle((Article) publication);

            throw new ArgumentException("Argument publication is not valid!");
        }

        protected abstract string CreateArticle(Article article);
        protected abstract string CreateBook(Book book);
        protected abstract string CreateDataset(Dataset dataset);
        protected abstract string CreateConferencePaper(ConferencePaper book);
        protected abstract string CreatePatent(Patent book);
        protected abstract string CreateTechnicalReport(TechnicalReport book);
        protected abstract string CreateThesis(Thesis book);

        #region Helpers

        protected static IList<string> GetAuthors(PublicationBase publication)
        {
            return publication.Employees
                              .Select(employee => string.Format("{0} {1}", employee.FirstName, employee.LastName))
                              .ToList();
        }
        
        protected static IEnumerable<string> GetAuthorsLastNames(PublicationBase publication)
        {
            return publication.Employees
                              .Select(employee => employee.LastName)
                              .ToList();
        }

        #endregion Helpers
    }
}