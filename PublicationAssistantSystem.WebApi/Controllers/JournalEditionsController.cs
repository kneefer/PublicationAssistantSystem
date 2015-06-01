﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to journal editions repository.
    /// </summary>
    [RoutePrefix("api/JournalEditions")]
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

        /// <summary>
        /// Gets all journal editions.
        /// </summary>
        /// <returns> All journal editions. </returns>        
        [Route("")]        
        public IEnumerable<JournalEditionDTO> GetAll()
        {
            var journalEditions = _journalEditionRepository.Get();

            var mapped = journalEditions.Select(Mapper.Map<JournalEditionDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns journal edition with given id.
        /// </summary>
        /// <param name="journalEditionId"> Journal edition id. </param>
        /// <returns> JournalEdition DTO with specified id. </returns>
        [Route("{journalEditionId:int}")]
        public JournalEditionDTO GetJournalEditionById(int journalEditionId)
        {
            var result = _journalEditionRepository.GetByID(journalEditionId);
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<JournalEditionDTO>(result);
            return mapped;
        }

        /// <summary>
        /// Returns all journal editions of journal.
        /// </summary>
        /// <param name="journalId"> Journal identificator. </param>
        /// <returns> Journal editions of journal. </returns>
        [Route("~/api/Journals/{journalId:int}/JournalEditions")]
        public IEnumerable<JournalEditionDTO> GetJournalEditionsInJournal(int journalId)
        {
            var journalEditions = _journalEditionRepository.Get(x => x.JournalId == journalId);
            
            var mapped = journalEditions.Select(Mapper.Map<JournalEditionDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given journal edition.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The journal edition to add. </param>
        /// <returns> Added journal edition. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(JournalEditionDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, JournalEditionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<JournalEdition>(item);

            if (_journalRepository.GetByID(dbObject.JournalId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found journal with id: {0}", dbObject.JournalId));
            }

            _journalEditionRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<JournalEditionDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the journal edition.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated journal edition. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(JournalEditionDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, JournalEditionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<JournalEdition>(item);

            if (_journalRepository.GetByID(dbObject.JournalId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found journal with id: {0}", dbObject.JournalId));
            }

            _journalEditionRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<JournalEditionDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the given journal edition.
        /// </summary>
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
    }
}