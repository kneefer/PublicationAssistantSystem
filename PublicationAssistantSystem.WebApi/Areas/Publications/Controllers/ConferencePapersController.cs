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

namespace PublicationAssistantSystem.WebApi.Areas.Publications.Controllers
{
    /// <summary>
    /// Provides access for conference papers in publications repository.
    /// </summary>
    public class ConferencePapersController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public ConferencePapersController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are conference papers.
        /// </summary>
        /// <remarks> GET: api/Publications/ConferencePapers </remarks>
        /// <returns> All conference papers. </returns>
        public IEnumerable<PublicationBaseDTO> GetAllConferencePapers()
        {
            var results = _publicationBaseRepository.Get(p => p is ConferencePaper).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns conference paper with given id.
        /// </summary>
        /// <param name="id"> Conference paper id. </param>
        /// <remarks> GET: api/Publications/ConferencePapers/Id </remarks>
        /// <returns> Conference paper with specified id. </returns>
        public PublicationBaseDTO GetConferencePaper(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is ConferencePaper && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}