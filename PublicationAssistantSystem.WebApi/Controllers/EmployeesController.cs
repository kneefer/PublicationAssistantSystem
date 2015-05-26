using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class EmployeesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDivisionRepository _divisionRepository;

        public EmployeesController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IDivisionRepository divisionRepository)
        {
            _db = db;
            _divisionRepository = divisionRepository;
            _employeeRepository = employeeRepository;
        }

        /// <summary> Gets all employees. </summary>
        /// <returns> All employees. </returns>        
        public IEnumerable<EmployeeDTO> GetAll()
        {
            var results = _employeeRepository
                .Get(null, null, x => x.Division)
                .Select(y => new EmployeeDTO(y));

            return results;
        }
        /// <summary> Adds the given employee. </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The employee to add. </param>
        /// <returns> The added employee. </returns>
        [HttpPost]
        public EmployeeDTO Add(EmployeeDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var division = _divisionRepository.Get(x => x.Id == item.DivisionId).FirstOrDefault();
            if (division == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var employee = new Employee
            {
                Id            = item.Id,
                AcademicTitle = item.AcademicTitle,
                FirstName     = item.FirstName,
                LastName      = item.LastName,
                Division      = division,
            };

            _employeeRepository.Insert(employee);
            _db.SaveChanges();

            item.Id = employee.Id;

            return item;
        }

        /// <summary>   Deletes the given employee. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="item"> The employee to delete. </param>
        [HttpDelete]
        public void Delete(Employee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _employeeRepository.Delete(item);
            _db.SaveChanges();
        }

        /// <summary> Updates the employee. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated employee. </returns>
        [HttpPatch]
        public EmployeeDTO Update(Employee item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _employeeRepository.Update(item);
            _db.SaveChanges();

            return new EmployeeDTO(item);
        }
        /// <summary> Gets the employees of division with specified id. </summary>
        /// <param name="divisionId"> Identifier of division whose employees will be returned. </param>
        /// <returns> Employees associated with specified division </returns>
        [Route("Division/{divisionId}/Employees")]
        public IEnumerable<EmployeeDTO> GetEmployeesInDivision(int divisionId)
        {
            var results = _employeeRepository
                .Get(x => x.Division.Id == divisionId, null, y => y.Division)
                .Select(y => new EmployeeDTO(y));

            return results;
        }
}
}
