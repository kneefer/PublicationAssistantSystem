using System.Collections.Generic;
using System.IO;
using System.Text;
using PublicationAssistantSystem.Core.ExportFormatters.CSV;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class CSVCreator : Creator
    {
        private readonly CSVFormatter _formatter;

        public CSVCreator()
            : this(Stream.Null)
        {
        }

        public CSVCreator(Stream stream)
            : base(stream)
        {
            if (Stream == Stream.Null)
                Stream = new MemoryStream();

            _formatter = new CSVFormatter();
        }

        public override void CreateToStream(IEnumerable<PublicationBase> publications)
        {
            var header = _formatter.CreateHeader();
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(header);
            Stream.Write(bytes, 0, bytes.Length);

            base.CreateToStream(publications);
        }

        private void CreateBase(PublicationBase publication)
        {
            _formatter.AppendAuthors(GetAuthors(publication));
            _formatter.AppendTitle(publication.Title);
            _formatter.AppendPublicationDate(publication.PublicationDate);
            _formatter.AppendIsOnWOS(publication.IsOnWOS);
        }

        protected override string CreateArticle(Article article)
        {
            _formatter.CreateArticle();
            _formatter.AppendJournal(article.JournalEdition.Journal.Title);
            _formatter.AppendJournalEdition(article.JournalEdition.VolumeNumber);
            _formatter.AppendPages(article.PageFrom, article.PageTo);
            CreateBase(article);

            return _formatter.GetRow();
        }

        protected override string CreateBook(Book book)
        {
            _formatter.CreateBook();
            _formatter.AppendISBN(book.ISBN);
            _formatter.AppendPublisher(book.Publisher);
            CreateBase(book);

            return _formatter.GetRow();
        }

        protected override string CreateDataset(Dataset dataset)
        {
            _formatter.CreateDataset();
            CreateBase(dataset);

            return _formatter.GetRow();
        }

        protected override string CreateConferencePaper(ConferencePaper conferencePaper)
        {
            _formatter.CreateConferencePaper();
            CreateBase(conferencePaper);

            return _formatter.GetRow();
        }

        protected override string CreatePatent(Patent patent)
        {
            _formatter.CreatePatent();
            CreateBase(patent);

            return _formatter.GetRow();
        }

        protected override string CreateTechnicalReport(TechnicalReport technicalReport)
        {
            _formatter.CreateTechnicalReport();
            CreateBase(technicalReport);

            return _formatter.GetRow();
        }

        protected override string CreateThesis(Thesis thesis)
        {
            _formatter.CreateThesis();
            CreateBase(thesis);

            return _formatter.GetRow();
        }
    }
}