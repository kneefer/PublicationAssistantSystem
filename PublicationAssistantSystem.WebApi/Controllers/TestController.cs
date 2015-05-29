using System.Collections.Generic;
using System.Web.Http;
using PublicationAssistantSystem.Core.Infrastructure;
using PublicationAssistantSystem.Core.Mappers.WOS;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// For testing purposes
    /// </summary>
    public class TestController : ApiController
    {
        /// <summary>
        /// Gets something.
        /// </summary>
        /// <remarks> GET: api/Test </remarks>
        /// <returns> Something special for you. </returns>
        public IEnumerable<string> Get()
        {
            //var test = new Test();
            //test.Run();
            var converter = new WOSRecordToIRecordConverter();
            var res = Extensions.Deserialize<searchResults>();
            var result = converter.ToIRecord(res);

            return new[] { "value1", "value2" };
        }
    }
}
