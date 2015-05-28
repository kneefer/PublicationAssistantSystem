using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    [RoutePrefix("Publications/All")]
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
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var resultAll = _publicationBaseRepository.Get();
            var resultsArticles = _publicationBaseRepository.GetOfType<Article, JournalEdition>(null, null, x => x.Journal);

            foreach (var record in resultAll.OfType<Article>())
                record.Journal = resultsArticles.Single(x => x.Id == record.Id).Journal;

            var toReturn = resultAll.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            return toReturn;
        }

        /// <summary>
        /// Returns publication with given id.
        /// </summary>
        /// <param name="publicationId"> Publication id. </param>
        /// <returns> Publication with specified id. </returns>
        [Route("{publicationId:int}")]
        public PublicationBaseDTO GetPublication(int publicationId)
        {
            var result = _publicationBaseRepository.Get(x => x.Id == publicationId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var article = result as Article;
            if (article != null)
                article.Journal = _publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == article.Id, null, x => x.Journal).SingleOrDefault().Journal;

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}