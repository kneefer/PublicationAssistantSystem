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
            var results = _publicationBaseRepository.GetOfType<Thesis>();

            var mapped = results.Select(Mapper.Map<ThesisDTO>).ToList();
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
            var result = _publicationBaseRepository.GetOfType<Thesis>(x => x.Id == thesisId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ThesisDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are theses of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose theses will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Theses </remarks>
        /// <returns> Theses associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Theses")]
        public IEnumerable<ThesisDTO> GetThesesOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is Thesis);

            var mapped = results.Select(Mapper.Map<ThesisDTO>).ToList();
            return mapped;
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
        /// <param name="item"> The thesis to add. </param>
        /// <remarks> POST api/Publications/Theses </remarks>
        /// <returns> The added thesis DTO. </returns>
        [HttpPost]
        [Route("")]
        public ThesisDTO Add(ThesisDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var thesis = Mapper.Map<Thesis>(item);

            _publicationBaseRepository.Insert(thesis);
            _db.SaveChanges();

            item.Id = thesis.Id;

            return item;
        }

        /// <summary>
        /// Updates the thesis.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Theses </remarks>
        /// <returns> An updated thesis DTO. </returns>
        [HttpPut]
        [Route("")]
        public ThesisDTO Update(ThesisDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var thesis = Mapper.Map<Thesis>(item);

            _publicationBaseRepository.Update(thesis);
            _db.SaveChanges();

            var mapped = Mapper.Map<ThesisDTO>(thesis);
            return mapped;
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