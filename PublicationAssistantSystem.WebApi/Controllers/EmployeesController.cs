using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to employees repository.
    /// </summary>
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IPublicationBaseRepository _publicationRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        /// <param name="divisionRepository"> Repository of divisions. </param>
        /// <param name="publicationRepository"> Repository of publications. </param>
        public EmployeesController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IDivisionRepository divisionRepository, 
            IPublicationBaseRepository publicationRepository)
        {
            _db = db;
            _divisionRepository = divisionRepository;
            _employeeRepository = employeeRepository;
            _publicationRepository = publicationRepository;
        }

        /// <summary> 
        /// Gets all employees.
        /// </summary>
        /// <returns> All employees. </returns>        
        [Route("")]
        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employees = _employeeRepository.Get();

            var mapped = employees.Select(Mapper.Map<EmployeeDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns employee with given id.
        /// </summary>
        /// <param name="employeeId"> Employee id. </param>
        /// <returns> Employee with specified id. </returns>
        [Route("{employeeId:int}")]
        public EmployeeDTO GetEmployeeById(int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<EmployeeDTO>(employee);
            return mapped;
        }

        /// <summary> 
        /// Gets the employees of division with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="divisionId"> Identifier of division whose employees will be returned. </param>
        /// <returns> Employees associated with specified division. </returns>
        [Route("~/api/Divisions/{divisionId:int}/Employees")]
        [ResponseType(typeof(IEnumerable<EmployeeDTO>))]
        public HttpResponseMessage GetEmployeesInDivision(HttpRequestMessage request, int divisionId)
        {
            var division = _divisionRepository.GetById(divisionId);
            if (division == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found division with id:{0}", divisionId));
            }

            var mapped = division.Employees.Select(Mapper.Map<EmployeeDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary> 
        /// Gets the employees of publication with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="publicationId"> Identifier of division whose employees will be returned. </param>
        /// <returns> Employees associated with specified division. </returns>
        [Route("~/api/Publications/{publicationId:int}/Employees")]
        [ResponseType(typeof(IEnumerable<EmployeeDTO>))]
        public HttpResponseMessage GetEmployeesInPublication(HttpRequestMessage request, int publicationId)
        {
            var publication = _publicationRepository.GetById(publicationId);
            if(publication == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found publication with id:{0}", publicationId));
            }

            var mapped = publication.Employees.Select(Mapper.Map<EmployeeDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Adds the given employee.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The employee to add. </param>
        /// <returns> The added employee. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(EmployeeDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, EmployeeDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Employee>(item);

            if (_divisionRepository.GetById(dbObject.DivisionId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found division with id: {0}", dbObject.DivisionId));
            }

            _employeeRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<EmployeeDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary> 
        /// Updates the employee.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated employee. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(EmployeeDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, EmployeeDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Employee>(item);

            if (_divisionRepository.GetById(dbObject.DivisionId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found division with id: {0}", dbObject.DivisionId));
            }

            _employeeRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<EmployeeDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary> 
        /// Deletes the given employee. 
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="employeeId"> The ID of employee to delete. </param>
        [HttpDelete]
        [Route("{employeeId:int}")]
        public void Delete(int employeeId)
        {
            _employeeRepository.Delete(employeeId);
            _db.SaveChanges();
        }
    }
}