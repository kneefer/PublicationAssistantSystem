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
            base.CreateToStream(publications);

            Stream.Seek(0, SeekOrigin.Begin);
            var header = _formatter.GetHeader();
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(header);
            Stream.Write(bytes, 0, bytes.Length);
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
            CreateBase(article);
            _formatter.AppendJournal(article.JournalEdition.Journal.Title);
            _formatter.AppendJournalEdition(article.JournalEdition.VolumeNumber);
            _formatter.AppendPages(article.PageFrom, article.PageTo);

            return string.Empty;
        }

        protected override string CreateBook(Book book)
        {
            CreateBase(book);
            _formatter.AppendISBN(book.ISBN);
            _formatter.AppendPublisher(book.Publisher);

            return string.Empty;
        }

        protected override string CreateDataset(Dataset dataset)
        {
            CreateBase(dataset);

            return string.Empty;
        }

        protected override string CreateConferencePaper(ConferencePaper conferencePaper)
        {
            CreateBase(conferencePaper);

            return string.Empty;
        }

        protected override string CreatePatent(Patent patent)
        {
            CreateBase(patent);

            return string.Empty;
        }

        protected override string CreateTechnicalReport(TechnicalReport technicalReport)
        {
            CreateBase(technicalReport);

            return string.Empty;
        }

        protected override string CreateThesis(Thesis thesis)
        {
            CreateBase(thesis);

            return string.Empty;
        }
    }
}