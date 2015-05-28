using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access for books in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Datasets")]
    public class DatasetsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public DatasetsController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are datasets.
        /// </summary>
        /// <remarks> GET: api/Publications/Datasets </remarks>
        /// <returns> All datasets. </returns>
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAllDatasets()
        {
            var results = _publicationBaseRepository.Get(p => p is Dataset).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns dataset with given id.
        /// </summary>
        /// <param name="id"> Dataset id. </param>
        /// <remarks> GET: api/Publications/Datasets/Id </remarks>
        /// <returns> Dataset with specified id. </returns>
        [Route("{id:int}")]
        public PublicationBaseDTO GetDataset(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is Dataset && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}