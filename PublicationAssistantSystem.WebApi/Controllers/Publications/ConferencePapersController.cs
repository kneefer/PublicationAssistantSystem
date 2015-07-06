using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.Core.PostAddJobs;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access for conference papers in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/ConferencePapers")]
    public class ConferencePapersController : ApiController
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
        public ConferencePapersController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are conference papers.
        /// </summary>
        /// <remarks> GET: api/Publications/ConferencePapers </remarks>
        /// <returns> All conference papers. </returns>
        [Route("")]
        public IEnumerable<ConferencePaperDTO> GetAllConferencePapers()
        {
            var conferencePapers = _publicationBaseRepository.GetOfType<ConferencePaper>();

            var mapped = conferencePapers.Select(Mapper.Map<ConferencePaperDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns conference paper with given id.
        /// </summary>
        /// <param name="conferencePaperId"> Conference paper id. </param>
        /// <remarks> GET: api/Publications/ConferencePapers/Id </remarks>
        /// <returns> Conference paper with specified id. </returns>
        [Route("{conferencePaperId:int}")]
        public ConferencePaperDTO GetConferencePaperById(int conferencePaperId)
        {
            var conferencePaper = _publicationBaseRepository.GetOfType<ConferencePaper>(x => x.Id == conferencePaperId).SingleOrDefault();
            if (conferencePaper == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ConferencePaperDTO>(conferencePaper);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are conference papers of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose conference papers will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/ConferencePapers </remarks>
        /// <returns> Conference papers associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/ConferencePapers")]
        [ResponseType(typeof(IEnumerable<ConferencePaperDTO>))]
        public HttpResponseMessage GetConferencePapersOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<ConferencePaper>().Select(Mapper.Map<ConferencePaperDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Adds the given conference paper.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The conference paper to add. </param>
        /// <remarks> POST api/Publications/ConferencePapers </remarks>
        /// <returns> The added conference paper DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ConferencePaperDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, ConferencePaperDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<ConferencePaper>(item);

            dbObject.Employees = new List<Employee>();
            if (item.Employees != null)
            {
                foreach (var employee in item.Employees)
                {
                    var employeeToAdd = _employeeRepository.GetById(employee.Id);
                    if (employeeToAdd == null)
                    {
                        return request.CreateErrorResponse(
                            HttpStatusCode.PreconditionFailed,
                            string.Format("Not found employee with id:{0}", employee.Id));
                    }
                    dbObject.Employees.Add(employeeToAdd);
                }
            }

            _publicationBaseRepository.Insert(dbObject);
            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<ConferencePaperDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the conference paper.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/ConferencePapers </remarks>
        /// <returns> An updated conference paper DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(ConferencePaperDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, ConferencePaperDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<ConferencePaper>(item);
            dbObject.Employees = null;

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            dbObject = (ConferencePaper)_publicationBaseRepository.Get(x => x.Id == dbObject.Id, null, x => x.Employees).SingleOrDefault();

            foreach (var employee in dbObject.Employees.ToList())
                dbObject.Employees.Remove(employee);

            foreach (var employee in item.Employees)
            {
                var employeeToAdd = _employeeRepository.GetById(employee.Id);
                if (employeeToAdd == null)
                {
                    return request.CreateErrorResponse(
                        HttpStatusCode.PreconditionFailed,
                        string.Format("Not found employee with id:{0}", employee.Id));
                }
                dbObject.Employees.Add(employeeToAdd);
            }

            _db.SaveChanges();
            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<ConferencePaperDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the conference paper with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="conferencePaperId"> The id of conference paper to delete. </param>
        /// <remarks> DELETE api/Publications/ConferencePapers </remarks>
        [HttpDelete]
        [Route("{conferencePaperId:int}")]
        public void Delete(int conferencePaperId)
        {
            _publicationBaseRepository.Delete(conferencePaperId);
            _db.SaveChanges();
        }
    }
}