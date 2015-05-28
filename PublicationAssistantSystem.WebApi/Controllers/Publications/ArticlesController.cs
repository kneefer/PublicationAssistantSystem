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
        public IEnumerable<ArticleDTO> GetAllArticles()
        {
            var results = _publicationBaseRepository.GetOfType<Article, JournalEdition>(null, null, x => x.Journal);
            
            var mapped = results.Select(Mapper.Map<ArticleDTO>);
            return mapped;
        }

        /// <summary>
        /// Returns article with given id.
        /// </summary>
        /// <param name="articleId"> Article id. </param>
        /// <remarks> GET: api/Publications/Articles/Id </remarks>
        /// <returns> Article with specified id. </returns>
        [Route("{articleId:int}")]
        public ArticleDTO GetArticle(int articleId)
        {
            var result = _publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == articleId, null, x => x.Journal).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ArticleDTO>(result);
            return mapped;
        }
    }
}