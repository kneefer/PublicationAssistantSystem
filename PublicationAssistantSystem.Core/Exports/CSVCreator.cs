using System.Collections.Generic;
using System.IO;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.Exports
{
    public class CSVCreator : Creator
    {
        private readonly StreamWriter _streamWriter;

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
            return string.Empty;
        }

        protected override string CreateBook(Book book)
        {
            return string.Empty;
        }

        protected override string CreateDataset(Dataset dataset)
        {
            return string.Empty;
        }

        protected override string CreateConferencePaper(ConferencePaper conferencePaper)
        {
            return string.Empty;
        }

        protected override string CreatePatent(Patent patent)
        {
            return string.Empty;
        }

        protected override string CreateTechnicalReport(TechnicalReport technicalReport)
        {
            return string.Empty;
        }

        protected override string CreateThesis(Thesis thesis)
        {
            return string.Empty;
        }
    }
}