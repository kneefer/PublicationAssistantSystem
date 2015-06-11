using System.Collections.Generic;
using System.IO;
using CsvHelper;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class CSVCreator : Creator
    {
        private readonly StreamWriter _streamWriter;
        private readonly CsvWriter _csvWriter;

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

        public override void CreateToStream(IEnumerable<PublicationBase> publications)
        {
            foreach (var publication in publications)
            {
                Create(publication);
            }
        }

        protected override string CreateArticle(Article article)
        {
            _csvWriter.WriteRecord(article);
            return string.Empty;
        }

        protected override string CreateBook(Book book)
        {
            _csvWriter.WriteRecord(book);
            return string.Empty;
        }

        protected override string CreateDataset(Dataset dataset)
        {
            _csvWriter.WriteRecord(dataset);
            return string.Empty;
        }

        protected override string CreateConferencePaper(ConferencePaper conferencePaper)
        {
            _csvWriter.WriteRecord(conferencePaper);
            return string.Empty;
        }

        protected override string CreatePatent(Patent patent)
        {
            _csvWriter.WriteRecord(patent);
            return string.Empty;
        }

        protected override string CreateTechnicalReport(TechnicalReport technicalReport)
        {
            _csvWriter.WriteRecord(technicalReport);
            return string.Empty;
        }

        protected override string CreateThesis(Thesis thesis)
        {
            _csvWriter.WriteRecord(thesis);
            return string.Empty;
        }
    }
}