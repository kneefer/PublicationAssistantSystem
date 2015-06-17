using System;
using System.Collections.Generic;
using System.Text;

namespace PublicationAssistantSystem.Core.ExportFormatters.CSV
{
    public partial class CSVFormatter
    {
        private readonly StringBuilder _row;

        private const string BaseMarker            = "[[TYPE]],[[TITLE]],[[AUTHORS]],[[PUBLICATIONDATE]],[[ISONWOS]]";
        private const string ArticleMarker         = "[[JOURNAL]],[[EDITION]],[[PAGES]]";
        private const string BookMarker            = "[[ISBN]],[[PUBLISHER]]";
        private const string DatasetMarker         = "";
        private const string ConferencePaperMarker = "";
        private const string PatentMarker          = "";
        private const string TechnicalReportMarker = "";
        private const string ThesisMarker          = "";
        private const string RowMarker             = BaseMarker + "," +
                                                     ArticleMarker + "," + 
                                                     BookMarker;

        public CSVFormatter()
        {
            _row = new StringBuilder(RowMarker);
        }

        #region Private appending rules

        private string RemoveMarkers(string row)
        {
            var begining = -1;
            while ((begining = row.IndexOf("[[", StringComparison.Ordinal)) > 0)
            {
                var end = row.IndexOf("]]", StringComparison.Ordinal);

                if (end > begining)
                {
                    row = row.Remove(begining, end);
                }
                else
                {
                    break;
                }
            }
            return row;
        }

        private void AppendField(string fieldName, int fieldValue)
        {
            var fieldMarker = "[[" + fieldName.ToUpper() + "]]";
            _row.Replace(fieldMarker, fieldValue.ToString());
        }

        private void AppendField(string fieldName, string fieldValue)
        {
            var fieldMarker = "[[" + fieldName.ToUpper() + "]]";
            _row.Replace(fieldMarker, fieldValue);
        }

        #endregion Private appending rules

        #region Creating publication entry

        public string GetEntry()
        {
            var row = RemoveMarkers(_row.ToString());
            return row;
        }

        public string GetHeader()
        {
            AppendType(Fields.Misc.Type);
            AppendAuthor(Fields.Base.Authors);
            AppendTitle(Fields.Base.Title);
            AppendField(Fields.Base.PublicationDate, Fields.Base.PublicationDate);
            AppendField(Fields.Base.IsOnWOS, Fields.Base.IsOnWOS);
            AppendJournal(Fields.Article.JournalName);
            AppendJournalEdition(Fields.Article.JournalEdition);
            AppendPages(Fields.Article.Pages);
            AppendPublisher(Fields.Book.Publisher);
            AppendISBN(Fields.Book.ISBN);
            
            return GetEntry();
        }

        private void AppendType(string type)
        {
            AppendField(Fields.Misc.Type, type);
        }

        #endregion Creating publication entry

        #region Appending specific fields

        #region Base

        public void AppendAuthor(string author)
        {
            AppendField(Fields.Base.Authors, author);
        }

        public void AppendAuthors(IList<string> authors)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < authors.Count; i++)
            {
                var author = authors[i];
                if (i != authors.Count - 1)
                    sb.AppendFormat("{0} and ", author);
                else
                    sb.Append(author);
            }

            AppendAuthor(sb.ToString());
        }

        public void AppendTitle(string title)
        {
            AppendField(Fields.Base.Title, title);
        }

        public void AppendPublicationDate(DateTime date)
        {
            AppendField(Fields.Base.PublicationDate, date.ToString("yy-MM-dd"));
        }
        public void AppendIsOnWOS(bool isOnWOS)
        {
            AppendField(Fields.Base.IsOnWOS, isOnWOS ? 1 : 0);
        }

        #endregion Base

        #region Article
        
        public void AppendJournal(string journal)
        {
            AppendField(Fields.Article.JournalName, journal);
        }
        public void AppendJournalEdition(int edition)
        {
            AppendField(Fields.Article.JournalEdition, edition);
        }

        public void AppendJournalEdition(string edition)
        {
            AppendField(Fields.Article.JournalEdition, edition);
        }

        public void AppendPages(string pages)
        {
            AppendField(Fields.Article.Pages, pages);
        }

        public void AppendPages(int pageFrom, int pageTo)
        {
            AppendField(Fields.Article.Pages, string.Format("{0}-{1}", pageFrom, pageTo));
        }

        #endregion Article

        #region Book
        
        public void AppendISBN(string isbn)
        {
            AppendField(Fields.Book.ISBN, isbn);
        }

        public void AppendPublisher(string publisher)
        {
            AppendField(Fields.Book.Publisher, publisher);
        }

        #endregion Book

        #endregion Appending specific fields
    }
}