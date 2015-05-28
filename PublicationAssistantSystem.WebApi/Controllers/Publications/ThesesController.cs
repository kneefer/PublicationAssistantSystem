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
    /// Provides access for theses in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Theses")]
    public class ThesesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public ThesesController(
            IPublicationAssistantContext db,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are theses.
        /// </summary>
        /// <remarks> GET: api/Publications/Theses </remarks>
        /// <returns> All theses. </returns>
        [Route("")]
        public IEnumerable<ThesisDTO> GetAllTheses()
        {
            var results = _publicationBaseRepository.GetOfType<Thesis>();

            var mapped = results.Select(Mapper.Map<ThesisDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns theses with given id.
        /// </summary>
        /// <param name="thesisId"> Thesis id. </param>
        /// <remarks> GET: api/Publications/Theses/Id </remarks>
        /// <returns> Thesis with specified id. </returns>
        [Route("{thesisId:int}")]
        public ThesisDTO GetThesis(int thesisId)
        {
            var result = _publicationBaseRepository.GetOfType<Thesis>(x => x.Id == thesisId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ThesisDTO>(result);
            return mapped;
        }
    }
}