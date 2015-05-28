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
        public IEnumerable<PublicationBaseDTO> GetAllTheses()
        {
            var results = _publicationBaseRepository.Get(p => p is Thesis).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns theses with given id.
        /// </summary>
        /// <param name="id"> Thesis id. </param>
        /// <remarks> GET: api/Publications/Theses/Id </remarks>
        /// <returns> Thesis with specified id. </returns>
        [Route("{id:int}")]
        public PublicationBaseDTO GetThesis(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is Thesis && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}