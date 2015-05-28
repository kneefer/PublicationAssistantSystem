using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access for patents in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Patents")]
    public class PatentsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public PatentsController(
            IPublicationAssistantContext db,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are patents.
        /// </summary>
        /// <remarks> GET: api/Publications/Patents </remarks>
        /// <returns> All patents. </returns>
        [Route("")]
        public IEnumerable<PatentDTO> GetAllPatents()
        {
            var results = _publicationBaseRepository.GetOfType<Patent>();

            var mapped = results.Select(Mapper.Map<PatentDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns patent with given id.
        /// </summary>
        /// <param name="patentId"> Patent id. </param>
        /// <remarks> GET: api/Publications/Patents/Id </remarks>
        /// <returns> Patent with specified id. </returns>
        [Route("{patentId:int}")]
        public PatentDTO GetPatent(int patentId)
        {
            var result = _publicationBaseRepository.GetOfType<Patent>(x => x.Id == patentId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<PatentDTO>(result);
            return mapped;
        }
    }
}