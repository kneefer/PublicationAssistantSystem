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
    /// Provides access for theses in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Theses")]
    public class ThesesController : ApiController
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
        public ThesesController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are theses.
        /// </summary>
        /// <remarks> GET: api/Publications/Theses </remarks>
        /// <returns> All theses. </returns>
        [Route("")]
        public IEnumerable<ThesisDTO> GetAllTheses()
        {
            var theses = _publicationBaseRepository.GetOfType<Thesis>();

            var mapped = theses.Select(Mapper.Map<ThesisDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns theses with given id.
        /// </summary>
        /// <param name="thesisId"> Thesis id. </param>
        /// <remarks> GET: api/Publications/Theses/Id </remarks>
        /// <returns> Thesis with specified id. </returns>
        [Route("{thesisId:int}")]
        public ThesisDTO GetThesisById(int thesisId)
        {
            var thesis = _publicationBaseRepository.GetOfType<Thesis>(x => x.Id == thesisId).SingleOrDefault();
            if (thesis == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ThesisDTO>(thesis);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are theses of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose theses will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Theses </remarks>
        /// <returns> Theses associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Theses")]
        [ResponseType(typeof(IEnumerable<ThesisDTO>))]
        public HttpResponseMessage GetThesesOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<Thesis>().Select(Mapper.Map<ThesisDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Adds the given thesis.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The thesis to add. </param>
        /// <remarks> POST api/Publications/Theses </remarks>
        /// <returns> The added thesis DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ThesisDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, ThesisDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Thesis>(item);

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

            var mapped = Mapper.Map<ThesisDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the thesis.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Theses </remarks>
        /// <returns> An updated thesis DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(ThesisDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, ThesisDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Thesis>(item);
            dbObject.Employees = null;

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            dbObject = (Thesis)_publicationBaseRepository.Get(x => x.Id == dbObject.Id, null, x => x.Employees).SingleOrDefault();

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

            var mapped = Mapper.Map<ThesisDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the thesis with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="thesisId"> The id of thesis to delete. </param>
        /// <remarks> DELETE api/Publications/Theses </remarks>
        [HttpDelete]
        [Route("{thesisId:int}")]
        public void Delete(int thesisId)
        {
            _publicationBaseRepository.Delete(thesisId);
            _db.SaveChanges();
        }
    }
}