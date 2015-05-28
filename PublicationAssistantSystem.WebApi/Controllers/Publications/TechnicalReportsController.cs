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
    /// Provides access for technical reports in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/TechnicalReports")]
    public class TechnicalReportsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public TechnicalReportsController(
            IPublicationAssistantContext db,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are technical reports.
        /// </summary>
        /// <remarks> GET: api/Publications/TechnicalReports </remarks>
        /// <returns> All technical reports. </returns>
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAllTechnicalReports()
        {
            var results = _publicationBaseRepository.Get(p => p is TechnicalReport).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns technical report with given id.
        /// </summary>
        /// <param name="id"> Technical report id. </param>
        /// <remarks> GET: api/Publications/TechnicalReports/Id </remarks>
        /// <returns> Technical report with specified id. </returns>
        [Route("{id:int}")]
        public PublicationBaseDTO GetTechnicalReport(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is TechnicalReport && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}