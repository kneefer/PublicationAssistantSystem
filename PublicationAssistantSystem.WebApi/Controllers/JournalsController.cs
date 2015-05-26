using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    [RoutePrefix("api/Journals")]
    public class JournalsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IJournalRepository _journalRepository;

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

        /// <summary>   Deletes the given journal. </summary>
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
                Id    = item.Id,
                Title = item.Title,
                ISSN  = item.ISSN,
                eISSN = item.eISSN,
            };

            _journalRepository.Update(journal);
            _db.SaveChanges();

            return new JournalDTO(journal);
        }
    }
}
