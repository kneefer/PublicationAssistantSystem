﻿using System;
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
    /// Provides access to journal editions repository
    /// </summary>
    [RoutePrefix("api/JournalEdition")]
    public class JournalEditionsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IJournalRepository _journalRepository;
        private readonly IJournalEditionRepository _journalEditionRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="journalRepository"> Repository of journals. </param>
        /// <param name="journalEditionRepository"> Repository of journal editions. </param>
        public JournalEditionsController(
            IPublicationAssistantContext db,
            IJournalRepository journalRepository,
            IJournalEditionRepository journalEditionRepository)
        {
            _db = db;
            _journalRepository = journalRepository;
            _journalEditionRepository = journalEditionRepository;
        }

        /// <summary> Gets all journal editions. </summary>
        /// <returns> All journal editions. </returns>        
        public IEnumerable<JournalEditionDTO> GetAll()
        {
            var results = _journalEditionRepository
                .Get()
                .Select(x => new JournalEditionDTO(x));

            return results;
        }

        /// <summary>
        /// Returns all journal editions of journal
        /// </summary>
        /// <param name="journalId"> Journal identificator. </param>
        /// <returns> Journal editions of journal. </returns>
        [Route("~/api/Journals/{journalId}/JournalEditions")]
        public IEnumerable<JournalEditionDTO> GetJournalEditionsInJournal(int journalId)
        {
            var results = _journalEditionRepository
                .Get(x => x.Journal.Id == journalId, null, y => y.Journal)
                .Select(y => new JournalEditionDTO(y));

            return results;
        }
            
        /// <summary> Adds the given journal edition. </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The journal edition to add. </param>
        /// <returns> Added journal edition. </returns>
        [HttpPost]
        public JournalEditionDTO Add(JournalEditionDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var journal = _journalRepository.Get(x => x.Id == item.JournalId).FirstOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var journalEdition = new JournalEdition
            {
                Id = item.Id,
                PublishDate = item.PublishDate,
                VolumeNumber = item.VolumeNumber,
                Journal = journal,
            };

            _journalEditionRepository.Insert(journalEdition);
            _db.SaveChanges();
            
            item.Id = journal.Id;
            
            return item;
        }

        /// <summary> Deletes the given journal edition. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="journalEditionId"> The ID of journal edition to delete. </param>
        [HttpDelete]
        [Route("{journalEditionId:int}")]
        public void Delete(int journalEditionId)
        {
            _journalEditionRepository.Delete(journalEditionId);
            _db.SaveChanges();
        }

        /// <summary> Updates the journal edition. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated journal edition. </returns>
        [HttpPatch]
        public JournalEditionDTO Update(JournalEditionDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var journal = _journalRepository.Get(x => x.Id == item.JournalId).FirstOrDefault();
            if (journal == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var journalEdition = new JournalEdition
            {
                PublishDate = item.PublishDate,
                VolumeNumber = item.VolumeNumber,
                Journal = journal,
            };

            _journalEditionRepository.Update(journalEdition);
            _db.SaveChanges();

            item.Id = journalEdition.Id;

            return item;
        }
    }
}