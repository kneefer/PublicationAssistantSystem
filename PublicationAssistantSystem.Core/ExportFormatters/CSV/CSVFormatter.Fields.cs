namespace PublicationAssistantSystem.Core.ExportFormatters.CSV
{
    public partial class CSVFormatter
    {
        /// <summary>
        /// Contains entries names for CSV format.
        /// </summary>
        private static class Entries
        {
            public const string Article         = "article";
            public const string Book            = "book";
            public const string Dataset         = "dataset";
            public const string ConferencePaper = "conference paper";
            public const string Patent          = "patent";
            public const string TechnicalReport = "technical report";
            public const string Thesis          = "thesis";
        }

        private static class Fields
        {
            public static class Misc
            {
                public const string Type = "Type";    
            }

            public static class Base
            {
                public const string Title = "Title";
                public const string Authors = "Authors";
                public const string PublicationDate = "PublicationDate";
                public const string IsOnWOS = "IsOnWOS";
            }

            public static class Article
            {
                public const string JournalName = "Journal";
                public const string JournalEdition = "Edition";
                public const string Pages = "Pages";
            }

            public static class Book
            {
                public const string ISBN = "ISBN";
                public const string Publisher = "Publisher";    
            }
            
            /*Dataset*/

            /*ConferencePaper*/

            /*Patent*/

            /*TechnicalReport*/

            /*Thesis*/

        }
    }
}