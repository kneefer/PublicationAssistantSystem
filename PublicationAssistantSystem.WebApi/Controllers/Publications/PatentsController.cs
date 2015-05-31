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
    /// Provides access for patents in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Patents")]
    public class PatentsController : ApiController
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
        public PatentsController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are patents.
        /// </summary>
        /// <remarks> GET: api/Publications/Patents </remarks>
        /// <returns> All patents. </returns>
        [Route("")]
        public IEnumerable<PatentDTO> GetAllPatents()
        {
            var results = _publicationBaseRepository.GetOfType<Patent>();

            var mapped = results.Select(Mapper.Map<PatentDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns patent with given id.
        /// </summary>
        /// <param name="patentId"> Patent id. </param>
        /// <remarks> GET: api/Publications/Patents/Id </remarks>
        /// <returns> Patent with specified id. </returns>
        [Route("{patentId:int}")]
        public PatentDTO GetPatentById(int patentId)
        {
            var result = _publicationBaseRepository.GetOfType<Patent>(x => x.Id == patentId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<PatentDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are patents of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose patents will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Patents </remarks>
        /// <returns> Patents associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Patents")]
        public IEnumerable<PatentDTO> GetPatentsOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is Patent);

            var mapped = results.Select(Mapper.Map<PatentDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given patent.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The patent to add. </param>
        /// <remarks> POST api/Publications/Patents </remarks>
        /// <returns> The added patent DTO. </returns>
        [HttpPost]
        [Route("")]
        public PatentDTO Add(PatentDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var patent = Mapper.Map<Patent>(item);

            _publicationBaseRepository.Insert(patent);
            _db.SaveChanges();

            item.Id = patent.Id;

            return item;
        }

        /// <summary>
        /// Updates the patent.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Patents </remarks>
        /// <returns> An updated patent DTO. </returns>
        [HttpPatch]
        [Route("")]
        public PatentDTO Update(PatentDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var patent = Mapper.Map<Patent>(item);

            _publicationBaseRepository.Update(patent);
            _db.SaveChanges();

            var mapped = Mapper.Map<PatentDTO>(patent);
            return mapped;
        }

        /// <summary>
        /// Deletes the patent with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="patentId"> The id of patent to delete. </param>
        /// <remarks> DELETE api/Publications/Patents </remarks>
        [HttpDelete]
        [Route("{patentId:int}")]
        public void Delete(int patentId)
        {
            _publicationBaseRepository.Delete(patentId);
            _db.SaveChanges();
        }
    }
}