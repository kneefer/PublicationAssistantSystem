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
    /// Provides access for conference papers in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/ConferencePapers")]
    public class ConferencePapersController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public ConferencePapersController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are conference papers.
        /// </summary>
        /// <remarks> GET: api/Publications/ConferencePapers </remarks>
        /// <returns> All conference papers. </returns>
        [Route("")]
        public IEnumerable<ConferencePaperDTO> GetAllConferencePapers()
        {
            var results = _publicationBaseRepository.GetOfType<ConferencePaper>();

            var mapped = results.Select(Mapper.Map<ConferencePaperDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns conference paper with given id.
        /// </summary>
        /// <param name="conferencePaperId"> Conference paper id. </param>
        /// <remarks> GET: api/Publications/ConferencePapers/Id </remarks>
        /// <returns> Conference paper with specified id. </returns>
        [Route("{conferencePaperd:int}")]
        public ConferencePaperDTO GetConferencePaper(int conferencePaperId)
        {
            var result = _publicationBaseRepository.GetOfType<ConferencePaper>(x => x.Id == conferencePaperId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ConferencePaperDTO>(result);
            return mapped;
        }
    }
}