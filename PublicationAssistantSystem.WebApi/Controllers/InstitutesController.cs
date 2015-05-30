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
            var results =_instituteRepository.Get(null, null, i => i.Faculty);
            
            var mapped = results.Select(Mapper.Map<InstituteDTO>).ToList();
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
            var result = _instituteRepository.Get(i => i.Id == instituteId, null, i => i.Faculty).SingleOrDefault();
            if(result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<InstituteDTO>(result);
            return mapped;
        }

        /// <summary>
        /// Gets the institutes of faculty with specified id.
        /// </summary>
        /// <param name="facultyId"> Identifier of faculty whose institutes will be returned. </param>
        /// <returns> Institutes associated with specified faculty. </returns>
        [Route("~/api/Faculties/{facultyId}/Institutes")]
        public IEnumerable<InstituteDTO> GetInstitutesInFaculty(int facultyId)
        {
            var results = _instituteRepository.Get(x => x.Faculty.Id == facultyId, null, y => y.Faculty);
            
            var mapped = results.Select(Mapper.Map<InstituteDTO>).ToList();
            return mapped;
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
        /// <param name="item"> The institute to add. </param>
        /// <returns> The added institute. </returns>
        [HttpPost]
        public InstituteDTO Add(InstituteDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var faculty = _facultyRepository.Get(x => x.Id == item.FacultyId).FirstOrDefault();
            if (faculty == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var institute = new Institute
            {
                Id      = item.Id,
                Name    = item.Name,
                Faculty = faculty
            };

            _instituteRepository.Insert(institute);
            _db.SaveChanges();

            item.Id = institute.Id;

            return item;
        }

        /// <summary>
        /// Updates the institute.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated institute. </returns>
        [HttpPatch]
        public InstituteDTO Update(InstituteDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var faculty = _facultyRepository.Get(x => x.Id == item.FacultyId).FirstOrDefault();
            if (faculty == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var institute = new Institute
            {
                Name = item.Name,
                Faculty = faculty
            };

            _instituteRepository.Update(institute);
            _db.SaveChanges();

            return item;
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