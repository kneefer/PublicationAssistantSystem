using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace PublicationAssistantSystem.Core.SearchApiEngines
{
    public class MniszwSearchEngine
    {
        private IEnumerable<MNiSzWElement> _elements;

        public MniszwSearchEngine(StreamReader fileStream)
        {
            var csvHelper = new CsvReader(fileStream);
            _elements = csvHelper.GetRecords<MNiSzWElement>();
        }

        public IEnumerable<MNiSzWElement> GetResults()
        {
            return _elements;
        }

        public MniszwSearchEngine ByTitle(string title)
        {
            _elements = _elements.Where(x => x.JournalTitle == title);
            return this;
        }

        public MniszwSearchEngine ByISSN(string issn)
        {
            _elements = _elements.Where(x => x.ISSN == issn);
            return this;
        }
    }

    public class MNiSzWElement
    {
        public string MNiSzWList { get; set; }
        public int PositionOnList { get; set; }
        public string JournalTitle { get; set; }
        public string ISSN { get; set; }
        public string MNiSzwPoints { get; set; }
    }
}
