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
        public IEnumerable<TechnicalReportDTO> GetAllTechnicalReports()
        {
            var results = _publicationBaseRepository.GetOfType<TechnicalReport>();

            var mapped = results.Select(Mapper.Map<TechnicalReportDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns technical report with given id.
        /// </summary>
        /// <param name="technicalReportId"> Technical report id. </param>
        /// <remarks> GET: api/Publications/TechnicalReports/Id </remarks>
        /// <returns> Technical report with specified id. </returns>
        [Route("{technicalReportId:int}")]
        public TechnicalReportDTO GetTechnicalReport(int technicalReportId)
        {
            var result = _publicationBaseRepository.GetOfType<TechnicalReport>(x => x.Id == technicalReportId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<TechnicalReportDTO>(result);
            return mapped;
        }
    }
}