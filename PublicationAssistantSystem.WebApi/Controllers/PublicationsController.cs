using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Mappers;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    [RoutePrefix("api/Publications")]
    public class PublicationsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public PublicationsController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        
        /// <summary>
        /// Returns all publications
        /// </summary>
        /// <remarks> GET: api/Publications </remarks>
        /// <returns> All publications. </returns>
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var results = _publicationBaseRepository.Get().Select(y => y.ToDTO());

            return results;
        }
    }
}