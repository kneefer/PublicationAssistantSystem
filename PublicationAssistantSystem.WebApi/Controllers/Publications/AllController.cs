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
    [RoutePrefix("api/Publications/All")]
    public class AllController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        public AllController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository, IEmployeeRepository employeeRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
            _employeeRepository = employeeRepository;
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

            var mapped = resultAll.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            return mapped;
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
            {
                var singleJournal = _publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == article.Id, null, x => x.Journal).SingleOrDefault();
                if (singleJournal != null)
                    article.Journal = singleJournal.Journal;
            }

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications of employee with specified id.
        ///  </summary>
        /// <param name="employeeId"> Identifier of employee whose publications will be returned. </param>
        /// <returns> Publications associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId}/Publications")]
        public IEnumerable<PublicationBaseDTO> GetPublicationsOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            foreach (var article in employee.Publications.OfType<Article>())
                article.Journal = _publicationBaseRepository.GetOfType<Article, JournalEdition>(x => x.Id == article.Id, null, x => x.Journal).SingleOrDefault().Journal;

            var mapped = employee.Publications.Select(Mapper.DynamicMap<PublicationBaseDTO>).ToList();
            return mapped;
        } 
    }
}