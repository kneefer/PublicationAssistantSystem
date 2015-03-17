using System.Collections.Generic;
using System.Web.Http;
using PublicationAssistantSystem.Core.Infrastructure;
using PublicationAssistantSystem.Core.Mappers.WOS;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
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
