﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicationAssistantSystem.Core.ExportFormatters.BIB
{
    public partial class BIBFormatter
    {
        private const string BeginFormat = "@{0}{{[[quoteMarker]],{1}";
        private const string FieldFormat = "\t{{0,-{0}}} = {{{{{{1}}}}}},{{2}}";
        private const string EndFormat   = "}}{0}{0}";

        private readonly StringBuilder _formatBuilder;

        public BIBFormatter()
        {
            _formatBuilder = new StringBuilder();
        }

        #region Private appending rules

        private  void AppendBegin(string publicationName)
        {
            _formatBuilder.Clear();
            _formatBuilder.AppendFormat(BeginFormat, publicationName, Environment.NewLine);
        }

        private void AppendEnd()
        {
            _formatBuilder.AppendFormat(EndFormat, Environment.NewLine);
        }

        private void AppendField(string fieldName, int fieldValue)
        {
            var format = string.Format(FieldFormat, Fields.MaxFieldLength);
            _formatBuilder.AppendFormat(format, fieldName, fieldValue, Environment.NewLine);
        }

        private void AppendField(string fieldName, string fieldValue)
        {
            var format = string.Format(FieldFormat, Fields.MaxFieldLength);
            _formatBuilder.AppendFormat(format, fieldName, fieldValue, Environment.NewLine);
        }

        private void RemoveMarkers()
        {
            _formatBuilder.Replace("[[quoteMarker]],", string.Empty);
        }

        #endregion Private appending rules

        #region Creating publication entry

        public string GetEntry()
        {
            RemoveMarkers();
            AppendEnd();
            return _formatBuilder.ToString();
        }

        public void CreateArticle()
        {
            AppendBegin(Entries.Article);   
        }

        public void CreateBook()
        {
            AppendBegin(Entries.Book);   
        }

        public void CreateConferencePaper()
        {
            AppendBegin(Entries.ConferencePaper);   
        }

        public void CreateTechnicalReport()
        {
            AppendBegin(Entries.TechnicalReport);   
        }

        public void CreateThesis()
        {
            AppendBegin(Entries.Thesis);   
        }

        public void CreateMisc()
        {
            AppendBegin(Entries.Misc);   
        }

        #endregion Creating publication entry

        #region Quote marker

        public void AppendQuoteMarker(string customMarker)
        {
            _formatBuilder.Replace("[[quoteMarker]]", customMarker);
        }

        public void AppendQuoteMarker(IEnumerable<string> authorsLastNames, int year)
        {
            // Cast to array and check for null
            var lastNames = authorsLastNames as string[] ?? authorsLastNames.ToArray();
            if (lastNames == null || !lastNames.Any())
                throw new ArgumentNullException("authorsLastNames");
            
            // Append firts author last name
            var sb = new StringBuilder(lastNames[0]);

            // Append others authors first letter of last name
            for (var i = 1; i < lastNames.Length; i++)
                sb.Append(lastNames[i][0]);

            // Append year
            sb.Append(year);

            AppendQuoteMarker(sb.ToString());
        }

        #endregion Quote marker

        #region Appending specific fields

        public void AppendAuthor(string author)
        {
            AppendField(Fields.Author, author);
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
            AppendField(Fields.Title, title);
        }

        public void AppendJournal(string journal)
        {
            AppendField(Fields.Journal, journal);
        }

        public void AppendYear(int year)
        {
            AppendField(Fields.Year, year);
        }

        public void AppendMonth(int month)
        {
            AppendField(Fields.Month, month);
        }

        public void AppendDay(int day)
        {
            AppendField(Fields.Day, day);
        }

        public void AppendDate(DateTime date)
        {
            AppendYear(date.Year);
            AppendMonth(date.Month);
            AppendDay(date.Day);
        }

        public void AppendVolume(int volume)
        {
            AppendField(Fields.Volume, volume);
        }

        public void AppendVolume(string volume)
        {
            AppendField(Fields.Volume, volume);
        }

        public void AppendNumber(string number)
        {
            AppendField(Fields.Number, number);
        }

        public void AppendPages(string pages)
        {
            AppendField(Fields.Pages, pages);
        }

        public void AppendPages(int pageFrom, int pageTo)
        {
            AppendField(Fields.Pages, string.Format("{0}-{1}", pageFrom, pageTo));
        }

        public void AppendNote(string note)
        {
            AppendField(Fields.Note, note);
        }

        public void AppendKey(string key)
        {
            AppendField(Fields.Key, key);
        }

        public void AppendPublisher(string publisher)
        {
            AppendField(Fields.Publisher, publisher);
        }

        public void AppendSeries(string series)
        {
            AppendField(Fields.Series, series);
        }

        public  void AppendAddress(string address)
        {
            AppendField(Fields.Address, address);
        }

        public void AppendEdition(string edition)
        {
            AppendField(Fields.Edition, edition);
        }

        public void AppendISBN(string isbn)
        {
            AppendField(Fields.ISBN, isbn);
        }

        public void AppendBooktitle(string booktitle)
        {
            AppendField(Fields.Booktitle, booktitle);
        }

        public void AppendOrganization(string organization)
        {
            AppendField(Fields.Organization, organization);
        }

        public void AppendInstitution(string institution)
        {
            AppendField(Fields.Institution, institution);
        }

        public void AppendType(string type)
        {
            AppendField(Fields.Type, type);
        }

        public void AppendNationality(string nationality)
        {
            AppendField(Fields.Nationality, nationality);
        }

        public void AppendPatentRefs(string patentRefs)
        {
            AppendField(Fields.PatentRefs, patentRefs);
        }

        public void AppendURL(string url)
        {
            AppendField(Fields.URL, url);
        }

        #endregion Appending specific fields
    }
}