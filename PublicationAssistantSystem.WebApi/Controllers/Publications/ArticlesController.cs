using System;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJournalEditionRepository _journalEditionRepository;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        /// <param name="journalEditionRepository"> Repository of journal editions. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public ArticlesController(
            IPublicationAssistantContext db, 
            IEmployeeRepository employeeRepository,
            IJournalEditionRepository journalEditionRepository, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _journalEditionRepository = journalEditionRepository;
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
            
            var mapped = results.Select(Mapper.Map<ArticleDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns article with given id.
        /// </summary>
        /// <param name="articleId"> Article id. </param>
        /// <remarks> GET: api/Publications/Articles/Id </remarks>
        /// <returns> Article with specified id. </returns>
        [Route("{articleId:int}")]
        public ArticleDTO GetArticleById(int articleId)
        {
            var result = _publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == articleId, null, x => x.Journal).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ArticleDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are articles of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose articles will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Articles </remarks>
        /// <returns> Articles associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Articles")]
        public IEnumerable<ArticleDTO> GetArticlesOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var articlesId = employee.Publications.Where(x => x is Article).Select(y => y.Id);

            var results = new List<Article>();
            foreach (var id in articlesId)
            {
                var tmp = id;
                results.Add(_publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == tmp, null, x => x.Journal).SingleOrDefault());
            }

            var mapped = results.Select(Mapper.Map<ArticleDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given article.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The article to add. </param>
        /// <remarks> POST api/Publications/Articles </remarks>
        /// <returns> The added article DTO. </returns>
        [HttpPost]
        [Route("")]
        public ArticleDTO Add(ArticleDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var journal = _journalEditionRepository.Get(x => x.Id == item.JournalEditionId).FirstOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var article = Mapper.Map<Article>(item);
            article.Journal = journal;

            _publicationBaseRepository.Insert(article);
            _db.SaveChanges();

            item.Id = article.Id;

            return item;
        }

        /// <summary>
        /// Updates the article.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Articles </remarks>
        /// <returns> An updated article DTO. </returns>
        [HttpPatch]
        [Route("")]
        public ArticleDTO Update(ArticleDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var journal = _journalEditionRepository.Get(x => x.Id == item.JournalEditionId).FirstOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var article = Mapper.Map<Article>(item);
            article.Journal = journal;

            _publicationBaseRepository.Update(article);
            _db.SaveChanges();

            var mapped = Mapper.Map<ArticleDTO>(article);
            return mapped;
        }

        /// <summary>
        /// Deletes the article with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="articleId"> The id of article to delete. </param>
        /// <remarks> DELETE api/Publications/Articles </remarks>
        [HttpDelete]
        [Route("{articleId:int}")]
        public void Delete(int articleId)
        {
            _publicationBaseRepository.Delete(articleId);
            _db.SaveChanges();
        }
    }
}