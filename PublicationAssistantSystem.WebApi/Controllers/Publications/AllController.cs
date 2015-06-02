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
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/All")]
    public class AllController : ApiController
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
        public AllController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }
        
        /// <summary>
        /// Returns all publications.
        /// </summary>
        /// <remarks> GET: api/Publications/All </remarks>
        /// <returns> All publications. </returns>
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var publications = _publicationBaseRepository.Get();

            var mapped = publications.Select(Mapper.Map<PublicationBaseDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns publication with given id.
        /// </summary>
        /// <param name="publicationId"> Publication id. </param>
        /// /// <remarks> GET: api/Publications/All/Id </remarks>
        /// <returns> Publication with specified id. </returns>
        [Route("{publicationId:int}")]
        public PublicationBaseDTO GetPublicationById(int publicationId)
        {
            var publication = _publicationBaseRepository.GetByID(publicationId);
            if (publication == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<PublicationBaseDTO>(publication);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose publications will be returned. </param>
        /// <remarks> GET: api/Employees/Id/Publications </remarks>
        /// <returns> Publications associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Publications")]
        [ResponseType(typeof(IEnumerable<PublicationBaseDTO>))]
        public HttpResponseMessage GetPublicationsOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetByID(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.Select(Mapper.Map<PublicationBaseDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Deletes the publication with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="publicationId"> The id of publication to delete. </param>
        /// <remarks> DELETE api/Publications/Id </remarks>
        [HttpDelete]
        [Route("~/api/Publications/{publicationId:int}")]
        public void Delete(int publicationId)
        {
            _publicationBaseRepository.Delete(publicationId);
            _db.SaveChanges();
        }
    }
}