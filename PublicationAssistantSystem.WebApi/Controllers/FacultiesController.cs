using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Routing;
using AutoMapper;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Provides access to faculties repository.
    /// </summary>
    [RoutePrefix("api/Faculties")]
    public class FacultiesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IFacultyRepository _facultyRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="facultyRepository"> Repository of faculties. </param>
        public FacultiesController(
            IPublicationAssistantContext db,
            IFacultyRepository facultyRepository)
        {
            _db = db;
            _facultyRepository = facultyRepository;
        }

        /// <summary>
        /// Gets all faculties.
        /// </summary>
        /// <returns> All faculties. </returns>
        [Route("")]
        public IEnumerable<FacultyDTO> GetAll()
        {
            var faculties = _facultyRepository.Get();
            
            var mapped = faculties.Select(Mapper.Map<FacultyDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns faculty with given id.
        /// </summary>
        /// <param name="facultyId"> Faculy id. </param>
        /// <returns> Faculty with specified id. </returns>
        [Route("{facultyId:int}")]
        public FacultyDTO GetFacultyById(int facultyId)
        {
            var faculty = _facultyRepository.GetByID(facultyId);
            if (faculty == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<FacultyDTO>(faculty);
            return mapped;
        }

        /// <summary>
        /// Adds the given faculty.
        /// </summary>
        /// <exception cref="ArgumentNullException">    
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The faculty to add. </param>
        /// <returns> The added faculty. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(FacultyDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, FacultyDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Faculty>(item);

            _facultyRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<FacultyDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the faculty.
        /// </summary>
        /// <exception cref="ArgumentNullException"> 
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated Faculty. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(FacultyDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, FacultyDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Faculty>(item);

            _facultyRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<FacultyDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the given faculty.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="facultyId"> The ID of faculty to delete. </param>
        [HttpDelete]
        [Route("{facultyId:int}")]
        public void Delete(int facultyId)
        {
            _facultyRepository.Delete(facultyId);
            _db.SaveChanges();
        }
    }
}