using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to institutes repository.
    /// </summary>
    [RoutePrefix("api/Institutes")]
    public class InstitutesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IFacultyRepository _facultyRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="instituteRepository"> Repository of institutes. </param>
        /// <param name="facultyRepository"> Repository of faculties. </param>
        public InstitutesController(
            IPublicationAssistantContext db,
            IInstituteRepository instituteRepository, 
            IFacultyRepository facultyRepository)
        {
            _db = db;
            _instituteRepository = instituteRepository;
            _facultyRepository = facultyRepository;
        }

        /// <summary>
        /// Gets all institutes.
        /// </summary>
        /// <returns> All institutes. </returns>        
        [Route("")]    
        public IEnumerable<InstituteDTO> GetAll()
        {
            var institutes =_instituteRepository.Get();
            
            var mapped = institutes.Select(Mapper.Map<InstituteDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns institute with given id.
        /// </summary>
        /// <param name="instituteId"> Institude id. </param>
        /// <returns> Institute with specified id. </returns>
        [Route("{instituteId:int}")]
        public InstituteDTO GetInstituteById(int instituteId)
        {
            var institute = _instituteRepository.GetByID(instituteId);
            if (institute == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<InstituteDTO>(institute);
            return mapped;
        }

        /// <summary>
        /// Gets the institutes of faculty with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="facultyId"> Identifier of faculty whose institutes will be returned. </param>
        /// <returns> Institutes associated with specified faculty. </returns>
        [Route("~/api/Faculties/{facultyId:int}/Institutes")]
        [ResponseType(typeof(IEnumerable<InstituteDTO>))]
        public HttpResponseMessage GetInstitutesInFaculty(HttpRequestMessage request, int facultyId)
        {
            var faculty = _facultyRepository.GetByID(facultyId);
            if (faculty == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found faculty with id:{0}", facultyId));
            }

            var mapped = faculty.Institutes.Select(Mapper.Map<InstituteDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Adds the given institute.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The institute to add. </param>
        /// <returns> The added institute. </returns>
        [Route("")]
        [HttpPut]
        [ResponseType(typeof(InstituteDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, InstituteDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Institute>(item);

            if (_facultyRepository.GetByID(dbObject.FacultyId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found faculty with id: {0}", dbObject.FacultyId));
            }

            _instituteRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<InstituteDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the institute.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated institute. </returns>
        [Route("")]
        [HttpPut]
        [ResponseType(typeof(InstituteDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, InstituteDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Institute>(item);

            if (_facultyRepository.GetByID(dbObject.FacultyId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found faculty with id: {0}", dbObject.FacultyId));
            }

            _instituteRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<InstituteDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the given institute. 
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="instituteId"> The ID of institute to delete. </param>
        [HttpDelete]
        [Route("{instituteId:int}")]
        public void Delete(int instituteId)
        {
            _instituteRepository.Delete(instituteId);
            _db.SaveChanges();
        }
    }
}