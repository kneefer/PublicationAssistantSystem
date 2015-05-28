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
        public IEnumerable<DatasetDTO> GetAllDatasets()
        {
            var results = _publicationBaseRepository.GetOfType<Dataset>();

            var mapped = results.Select(Mapper.Map<DatasetDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns dataset with given id.
        /// </summary>
        /// <param name="datasetId"> Dataset id. </param>
        /// <remarks> GET: api/Publications/Datasets/Id </remarks>
        /// <returns> Dataset with specified id. </returns>
        [Route("{datasetId:int}")]
        public DatasetDTO GetDataset(int datasetId)
        {
            var result = _publicationBaseRepository.GetOfType<Dataset>(x => x.Id == datasetId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<DatasetDTO>(result);
            return mapped;
        }
    }
}