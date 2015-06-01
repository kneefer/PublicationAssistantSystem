using System;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public TechnicalReportsController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
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
        public TechnicalReportDTO GetTechnicalReportById(int technicalReportId)
        {
            var result = _publicationBaseRepository.GetOfType<TechnicalReport>(x => x.Id == technicalReportId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<TechnicalReportDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are technical reports of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose technical reports will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/TechnicalReports </remarks>
        /// <returns> Technical reports associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/TechnicalReports")]
        public IEnumerable<TechnicalReportDTO> GetBooksOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is TechnicalReport);

            var mapped = results.Select(Mapper.Map<TechnicalReportDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given technical report.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The technical report to add. </param>
        /// <remarks> POST api/Publications/TechnicalReports </remarks>
        /// <returns> The added technical report DTO. </returns>
        [HttpPost]
        [Route("")]
        public TechnicalReportDTO Add(TechnicalReportDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var technicalReport = Mapper.Map<TechnicalReport>(item);

            _publicationBaseRepository.Insert(technicalReport);
            _db.SaveChanges();

            item.Id = technicalReport.Id;

            return item;
        }

        /// <summary>
        /// Updates the technical report.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/TechnicalReports </remarks>
        /// <returns> An updated technical report DTO. </returns>
        [HttpPut]
        [Route("")]
        public TechnicalReportDTO Update(TechnicalReportDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var technicalReport = Mapper.Map<TechnicalReport>(item);

            _publicationBaseRepository.Update(technicalReport);
            _db.SaveChanges();

            var mapped = Mapper.Map<TechnicalReportDTO>(technicalReport);
            return mapped;
        }

        /// <summary>
        /// Deletes the technical report with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="technicalReportId"> The id of technical report to delete. </param>
        /// <remarks> DELETE api/Publications/TechnicalReports </remarks>
        [HttpDelete]
        [Route("{technicalReportId:int}")]
        public void Delete(int technicalReportId)
        {
            _publicationBaseRepository.Delete(technicalReportId);
            _db.SaveChanges();
        }
    }
}