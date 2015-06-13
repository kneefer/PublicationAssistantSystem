using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.Core.PostAddJobs;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to journals repository.
    /// </summary>
    [RoutePrefix("api/Journals")]
    public class JournalsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IJournalRepository _journalRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="journalRepository"> Repository of journals. </param>
        public JournalsController(
            IPublicationAssistantContext db,
            IJournalRepository journalRepository)
        {
            _db = db;
            _journalRepository = journalRepository;
        }

        /// <summary>
        /// Gets all journals.
        /// </summary>
        /// <returns> All journals. </returns>        
        [Route("")]  
        public IEnumerable<JournalDTO> GetAll()
        {
            var journals = _journalRepository.Get();
            
            var mapped = journals.Select(Mapper.Map<JournalDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns journal with given id.
        /// </summary>
        /// <param name="journalId"> Journal id. </param>
        /// <returns> Journal DTO with specified id. </returns>
        [Route("{journalId:int}")]
        public JournalDTO GetJournalById(int journalId)
        {
            var journal = _journalRepository.GetById(journalId);
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<JournalDTO>(journal);
            return mapped;
        }

        /// <summary>
        /// Returns journal found by ISSN.
        /// </summary>
        /// <param name="issn"> Journal ISSN. </param>
        /// <returns> Journal DTO with specified ISSN. </returns>
        [Route("ISSN/{issn}")]
        public JournalDTO GetByISSN(string issn)
        {
            var journal = _journalRepository.Get(x => x.ISSN.Equals(issn, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<JournalDTO>(journal);
            return mapped;
        }

        /// <summary>
        /// Returns journal found by eISSN.
        /// </summary>
        /// <param name="eIssn"> Journal eISSN. </param>
        /// <returns> Journal DTO with specified eISSN. </returns>
        [Route("eISSN/{eIssn}")]
        public JournalDTO GetByEISSN(string eIssn)
        {
            var journal = _journalRepository.Get(x => x.eISSN != null && x.eISSN.Equals(eIssn, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            var mapped = Mapper.Map<JournalDTO>(journal);
            return mapped;
        }

        /// <summary>
        /// Returns journals with titles containing given part of title.
        /// </summary>
        /// <param name="titlePart"> Part of title to search with. </param>
        /// <returns> Journals containing titlePart. </returns>
        [Route("Like/{titlePart}")]
        public IEnumerable<JournalDTO> GetWithTitleLike(string titlePart)
        {
            var journal = _journalRepository.Get(x => x.Title.Contains(titlePart));

            var mapped = journal.Select(Mapper.Map<JournalDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given journal.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The journal to add. </param>
        /// <returns> The added journal. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(JournalDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, JournalPostDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Journal>(item);
            dbObject.IsComputing = true;

            _journalRepository.Insert(dbObject);
            _db.SaveChanges();

            var jobs = new JournalsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<JournalDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the journal.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated journal. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(JournalDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, JournalDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Journal>(item);

            _journalRepository.Update(dbObject);
            _db.SaveChanges();

            var jobs = new JournalsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<JournalDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the given journal.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="journalId"> The ID of journal to delete. </param>
        [HttpDelete]
        [Route("{journalId:int}")]
        public void Delete(int journalId)
        {
            _journalRepository.Delete(journalId);
            _db.SaveChanges();
        }
    }
}