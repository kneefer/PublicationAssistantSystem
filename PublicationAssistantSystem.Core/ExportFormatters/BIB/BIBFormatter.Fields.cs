namespace PublicationAssistantSystem.Core.ExportFormatters.BIB
{
    public partial class BIBFormatter
    {
        /// <summary>
        /// Contains fields names in bib format.
        /// </summary>
        private static class Fields
        {
            public const string Author       = "author";
            public const string Title        = "title";
            public const string Journal      = "journal";
            public const string Year         = "year";
            public const string Month        = "month";
            public const string Day          = "day";
            public const string Volume       = "volume";
            public const string Number       = "number";
            public const string Pages        = "pages";
            public const string Note         = "note";
            public const string Key          = "key";
            public const string Publisher    = "publisher";
            public const string Series       = "series";
            public const string Address      = "address";
            public const string Edition      = "edition";
            public const string ISBN         = "isbn";
            public const string Booktitle    = "booktitle";
            public const string Organization = "organization";
            public const string Institution  = "institution";
            public const string Type         = "type";
            public const string Nationality  = "nationality";
            public const string PatentRefs   = "pat_refs";
            public const string URL          = "url";

            /// <summary>
            /// Length name of the longest field.
            /// </summary>
            /// <remarks> Used for output formatting. </remarks>
            public const int MaxFieldLength  = 12;
        }

        /// <summary>
        /// Contains entries names in bib format.
        /// </summary>
        private static class Entries
        {
            public const string Article         = "article";
            public const string Book            = "book";
            public const string ConferencePaper = "conference";
            public const string TechnicalReport = "techreport";
            public const string Thesis          = "mastersthesis";
            public const string Misc            = "misc";
        }
    }
}