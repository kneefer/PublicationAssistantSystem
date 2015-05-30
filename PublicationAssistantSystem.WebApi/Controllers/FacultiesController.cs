using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
            var results = _facultyRepository.Get();
            
            var mapped = results.Select(Mapper.Map<FacultyDTO>).ToList();
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
            var result = _facultyRepository.Get(f => f.Id == facultyId).SingleOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<FacultyDTO>(result);
            return mapped;
        }

        /// <summary>
        /// Adds the given faculty.
        /// </summary>
        /// <exception cref="ArgumentNullException">    
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="item"> The faculty to add. </param>
        /// <returns> The added faculty. </returns>
        [HttpPost]
        public FacultyDTO Add(FacultyDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var faculty = new Faculty
            {
                Id           = item.Id,
                Name         = item.Name,
                Abbreviation = item.Name,
            };

            _facultyRepository.Insert(faculty);
            _db.SaveChanges();

            item.Id = faculty.Id;

            return item;
        }

        /// <summary>
        /// Updates the faculty.
        /// </summary>
        /// <exception cref="ArgumentNullException"> 
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated Faculty. </returns>
        [HttpPatch]
        public FacultyDTO Update(FacultyDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var faculty = new Faculty
            {
                Name = item.Name,
                Abbreviation = item.Abbreviation,
            };

            _facultyRepository.Update(faculty);
            _db.SaveChanges();

            return item;
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