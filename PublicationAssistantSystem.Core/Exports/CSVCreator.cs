using System.Collections.Generic;
using System.IO;
using CsvHelper;
using PublicationAssistantSystem.DAL.DTO.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class CSVCreator : Creator
    {
        private readonly StreamWriter _streamWriter;
        private CsvWriter _csvWriter;

        public CSVCreator()
            : this(Stream.Null)
        {
        }

        public CSVCreator(Stream stream)
            : base(stream)
        {
            if (Stream == Stream.Null)
                Stream = new MemoryStream();

            _streamWriter = new StreamWriter(Stream);
            _csvWriter = new CsvWriter(_streamWriter);
        }

        public override bool CreateToStream(IEnumerable<PublicationBaseDTO> publications)
        {
            var a = string.Empty;
            return base.CreateToStream(publications);
        }

        protected override string CreateArticle(ArticleDTO article, string journalTitle, int journalVolume)
        {
            return string.Empty;
        }

        protected override string CreateBook(BookDTO book)
        {
            return string.Empty;
        }

        protected override string CreateDataset(DatasetDTO dataset)
        {
            return string.Empty;
        }

        protected override string CreateConferencePaper(ConferencePaperDTO book)
        {
            return string.Empty;
        }

        protected override string CreatePatent(PatentDTO book)
        {
            return string.Empty;
        }

        protected override string CreateTechnicalReport(TechnicalReportDTO book)
        {
            return string.Empty;
        }

        protected override string CreateThesis(ThesisDTO book)
        {
            return string.Empty;
        }
    }
}