using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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
            var technicalReports = _publicationBaseRepository.GetOfType<TechnicalReport>();

            var mapped = technicalReports.Select(Mapper.Map<TechnicalReportDTO>).ToList();
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
            var technicalReport = _publicationBaseRepository.GetOfType<TechnicalReport>(x => x.Id == technicalReportId).SingleOrDefault();
            if (technicalReport == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<TechnicalReportDTO>(technicalReport);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are technical reports of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose technical reports will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/TechnicalReports </remarks>
        /// <returns> Technical reports associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/TechnicalReports")]
        [ResponseType(typeof(IEnumerable<TechnicalReportDTO>))]
        public HttpResponseMessage GetTechnicalReportsOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<TechnicalReport>().Select(Mapper.Map<TechnicalReportDTO>).ToList();
            return request.CreateResponse(mapped);
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
        /// <param name="request">Http request</param>
        /// <param name="item"> The technical report to add. </param>
        /// <remarks> POST api/Publications/TechnicalReports </remarks>
        /// <returns> The added technical report DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(TechnicalReportDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, TechnicalReportDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<TechnicalReport>(item);

            _publicationBaseRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<TechnicalReportDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the technical report.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/TechnicalReports </remarks>
        /// <returns> An updated technical report DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(TechnicalReportDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, TechnicalReportDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<TechnicalReport>(item);

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<TechnicalReportDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
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