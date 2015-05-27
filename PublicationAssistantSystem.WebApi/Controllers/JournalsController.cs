using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to journals repository
    /// </summary>
    [RoutePrefix("api/Journals")]
    public class JournalsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IJournalRepository _journalRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Db context</param>
        /// <param name="journalRepository">Repository of journals</param>
        public JournalsController(
            IPublicationAssistantContext db,
            IJournalRepository journalRepository)
        {
            _db = db;
            _journalRepository = journalRepository;
        }

        /// <summary> Gets all journals. </summary>
        /// <returns> All journals. </returns>        
        public IEnumerable<JournalDTO> GetAll()
        {
            var results = _journalRepository
                .Get()
                .Select(x => new JournalDTO(x));

            return results;
        }

        /// <summary>
        /// Returns journal found by ISSN
        /// </summary>
        /// <param name="issn">Journal ISSN</param>
        /// <returns> Journal DTO with specified ISSN or null, if not found. </returns>
        [Route("ISSN/{issn}")]
        public JournalDTO GetByISSN(string issn)
        {
            var result = _journalRepository
                .Get(x => x.ISSN.Equals(issn))
                .Select(y => new JournalDTO(y))
                .SingleOrDefault();

            if(result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return result;
        }

        /// <summary>
        /// Returns journal found by eISSN
        /// </summary>
        /// <param name="eIssn">Journal eISSN</param>
        /// <returns> Journal DTO with specified ISSN or null, if not found. </returns>
        [Route("eISSN/{eIssn}")]
        public JournalDTO GetByEISSN(string eIssn)
        {
            var result = _journalRepository
                .Get(x => x.eISSN != null && x.eISSN.Equals(eIssn))
                .Select(y => new JournalDTO(y))
                .SingleOrDefault();

            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return result;
        }

        /// <summary>
        /// Returns journals with titles containing given part of title.
        /// </summary>
        /// <param name="titlePart"> Part of title to search with. </param>
        /// <returns> Journals containing titlePart. </returns>
        [Route("Like/{titlePart}")]
        public IEnumerable<JournalDTO> GetWithTitleLike(string titlePart)
        {
            var results = _journalRepository
                .Get(x => x.Title.Contains(titlePart))
                .Select(y => new JournalDTO(y));

            return results;
        }

        /// <summary> Adds the given journal. </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The journal to add. </param>
        /// <returns> The added journal. </returns>
        [HttpPost]
        public JournalDTO Add(JournalPostDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var journal = new Journal
            {
                Title = item.Title,
                ISSN  = item.ISSN,
                eISSN = item.eISSN,
            };

            _journalRepository.Insert(journal);
            _db.SaveChanges();
            
            return new JournalDTO(journal);
        }

        /// <summary> Deletes the given journal. </summary>
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

        /// <summary> Updates the journal. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated journal. </returns>
        [HttpPatch]
        public JournalDTO Update(JournalDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var journal = new Journal
            {
                Title = item.Title,
                ISSN  = item.ISSN,
                eISSN = item.eISSN,
            };

            _journalRepository.Update(journal);
            _db.SaveChanges();

            item.Id = journal.Id;

            return item;
        }
    }
}