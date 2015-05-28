using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PublicationAssistantSystem.Core.Mappers.MNISW.Models;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    [RoutePrefix("api/Publications")]
    public class PublicationsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public PublicationsController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }
        
        /// <summary>
        /// Returns all publications.
        /// </summary>
        /// <remarks> GET: api/Publications </remarks>
        /// <returns> All publications. </returns>
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var resultAll = _publicationBaseRepository.Get();
            var resultsArticles = _publicationBaseRepository.GetOfType<Article, JournalEdition>(null, null, x => x.Journal);

            foreach (var record in resultAll.OfType<Article>())
                record.Journal = resultsArticles.Single(x => x.Id == record.Id).Journal;

            var toReturn = resultAll.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            return toReturn;
        }
    }
}