using System.IO;
using PublicationAssistantSystem.Core.ExportFormatters.BIB;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class BIBCreator : Creator
    {
        public BIBCreator()
            : this(Stream.Null)
        { }

        public BIBCreator(Stream stream)
            : base(stream)
        { }

        protected override string CreateArticle(Article article)
        {
            var formatter = new BIBFormatter();
            formatter.CreateArticle();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(article), 
                article.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(article));
            formatter.AppendTitle(article.Title);
            formatter.AppendDate(article.PublicationDate);
            formatter.AppendJournal(article.JournalEdition.Journal.Title);
            formatter.AppendVolume(article.JournalEdition.VolumeNumber);
            formatter.AppendPages(article.PageFrom, article.PageTo);
            return formatter.GetEntry();
        }

        protected override string CreateBook(Book book)
        {
            var formatter = new BIBFormatter();
            formatter.CreateBook();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(book),
                book.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(book));
            formatter.AppendTitle(book.Title);
            formatter.AppendDate(book.PublicationDate);
            formatter.AppendPublisher(book.Publisher);
            formatter.AppendISBN(book.ISBN);
            return formatter.GetEntry();
        }

        protected override string CreateDataset(Dataset dataset)
        {
            var formatter = new BIBFormatter();
            formatter.CreateMisc();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(dataset),
                dataset.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(dataset));
            formatter.AppendTitle(dataset.Title);
            formatter.AppendDate(dataset.PublicationDate);
            return formatter.GetEntry();
        }

        protected override string CreateConferencePaper(ConferencePaper conferencePaper)
        {
            var formatter = new BIBFormatter();
            formatter.CreateConferencePaper();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(conferencePaper),
                conferencePaper.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(conferencePaper));
            formatter.AppendTitle(conferencePaper.Title);
            formatter.AppendDate(conferencePaper.PublicationDate);
            return formatter.GetEntry();
        }

        protected override string CreatePatent(Patent patent)
        {
            var formatter = new BIBFormatter();
            formatter.CreateMisc();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(patent),
                patent.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(patent));
            formatter.AppendTitle(patent.Title);
            formatter.AppendDate(patent.PublicationDate);
            return formatter.GetEntry();
        }

        protected override string CreateTechnicalReport(TechnicalReport technicalReport)
        {
            var formatter = new BIBFormatter();
            formatter.CreateTechnicalReport();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(technicalReport),
                technicalReport.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(technicalReport));
            formatter.AppendTitle(technicalReport.Title);
            formatter.AppendDate(technicalReport.PublicationDate);
            return formatter.GetEntry();
        }

        protected override string CreateThesis(Thesis thesis)
        {
            var formatter = new BIBFormatter();
            formatter.CreateThesis();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(thesis),
                thesis.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(thesis));
            formatter.AppendTitle(thesis.Title);
            formatter.AppendDate(thesis.PublicationDate);
            return formatter.GetEntry();
        }
    }
}