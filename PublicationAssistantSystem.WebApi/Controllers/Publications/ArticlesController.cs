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
    /// Provides access for articles in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Articles")]
    public class ArticlesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public ArticlesController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are articles.
        /// </summary>
        /// <remarks> GET: api/Publications/Articles </remarks>
        /// <returns> All articles. </returns>
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAllArticles()
        {
            var results = _publicationBaseRepository.Get(p => p is Article).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns article with given id.
        /// </summary>
        /// <param name="id"> Article id. </param>
        /// <remarks> GET: api/Publications/Articles/Id </remarks>
        /// <returns> Article with specified id. </returns>
        [Route("{id:int}")]
        public PublicationBaseDTO GetArticle(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is Article && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}