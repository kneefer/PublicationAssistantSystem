using PublicationAssistantSystem.Core.ExportFormatters.BIB;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class BIBCreator : Creator
    {
        public override string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume)
        {
            var formatter = new BIBFormatter();
            formatter.CreateArticle();
            formatter.AppendQuoteMarker(
                GetAuthorsLastNames(article), 
                article.PublicationDate.Year);
            formatter.AppendAuthors(GetAuthors(article));
            formatter.AppendTitle(article.Title);
            formatter.AppendDate(article.PublicationDate);
            formatter.AppendJournal(journalTitle);
            formatter.AppendVolume(journalVolume);
            formatter.AppendPages(article.PageFrom, article.PageTo);
            return formatter.GetEntry();
        }

        public override string CreateBook(BookDTO book)
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

        public override string CreateDataset(DatasetDTO dataset)
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

        public override string CreateConferencePaper(ConferencePaperDTO conferencePaper)
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

        public override string CreatePatent(PatentDTO patent)
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

        public override string CreateTechnicalReport(TechnicalReportDTO technicalReport)
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

        public override string CreateThesis(ThesisDTO thesis)
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