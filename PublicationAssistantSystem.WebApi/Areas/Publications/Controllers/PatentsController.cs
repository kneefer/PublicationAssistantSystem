using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Areas.Publications.Controllers
{
    /// <summary>
    /// Provides access for patents in publications repository.
    /// </summary>
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
        public IEnumerable<PublicationBaseDTO> GetAllPatents()
        {
            var results = _publicationBaseRepository.Get(p => p is Patent).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns patent with given id.
        /// </summary>
        /// <param name="id"> Patent id. </param>
        /// <remarks> GET: api/Publications/Patents/Id </remarks>
        /// <returns> Patent with specified id. </returns>
        public PublicationBaseDTO GetPatent(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is Patent && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}