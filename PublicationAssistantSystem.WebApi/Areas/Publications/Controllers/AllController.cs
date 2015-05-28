using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Areas.Publications.Controllers
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    public class AllController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public AllController(
            IPublicationAssistantContext db,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications.
        /// </summary>
        /// <remarks> GET: api/Publications </remarks>
        /// <returns> All publications. </returns>
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var results = _publicationBaseRepository.Get().ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();           
            return toReturn;
        }

        /// <summary>
        /// Returns publication with given id.
        /// </summary>
        /// <param name="id"> Publication id. </param>
        /// <returns> Publication with specified id. </returns>
        public PublicationBaseDTO GetPublication(int id)
        {
            var result = _publicationBaseRepository.Get(p => p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}