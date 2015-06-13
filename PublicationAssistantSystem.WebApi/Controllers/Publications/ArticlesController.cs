using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;
using System.Web.Http.Description;
using PublicationAssistantSystem.Core.PostAddJobs;

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
            var articles = _publicationBaseRepository.GetOfType<Article>();
            
            var mapped = articles.Select(Mapper.Map<ArticleDTO>).ToList();
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
            var article = _publicationBaseRepository.GetOfType<Article>(x => x.Id == articleId).FirstOrDefault();
            if (article == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ArticleDTO>(article);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are articles of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose articles will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Articles </remarks>
        /// <returns> Articles associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Articles")]
        [ResponseType(typeof(IEnumerable<ArticleDTO>))]
        public HttpResponseMessage GetArticlesOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<Article>().Select(Mapper.Map<ArticleDTO>).ToList();
            return request.CreateResponse(mapped);
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
        /// <param name="request">Http request</param>
        /// <param name="item"> The article to add. </param>
        /// <remarks> POST api/Publications/Articles </remarks>
        /// <returns> The added article DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ArticleDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, ArticleDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Article>(item);

            if (_journalEditionRepository.GetById(dbObject.JournalEditionId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found journal edition with id: {0}", dbObject.JournalEditionId));
            }

            _publicationBaseRepository.Insert(dbObject);
            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<ArticleDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the article.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Articles </remarks>
        /// <returns> An updated article DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(ArticleDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, ArticleDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Article>(item);

            if (_journalEditionRepository.GetById(dbObject.JournalEditionId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found journal edition with id: {0}", dbObject.JournalEditionId));
            }

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<ArticleDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
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