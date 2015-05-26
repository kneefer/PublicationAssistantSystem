using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class InstitutesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IFacultyRepository _facultyRepository;

        public InstitutesController(
            IPublicationAssistantContext db,
            IInstituteRepository instituteRepository, 
            IFacultyRepository facultyRepository)
        {
            _db = db;
            _instituteRepository = instituteRepository;
            _facultyRepository = facultyRepository;
        }

        /// <summary> Gets all institutes. </summary>
        /// <returns> All institutes. </returns>        
        public IEnumerable<InstituteDTO> GetAll()
        {
            var results =_instituteRepository
                .Get(null, null, x => x.Faculty)
                .Select((y) => new InstituteDTO(y));

            return results;
        }

        /// <summary> Adds the given institute. </summary>
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
            {
                throw new ArgumentNullException("item");
            }

            var faculty = _facultyRepository.Get((x) => x.Id == item.FacultyId).FirstOrDefault();
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

        /// <summary>   Deletes the given institute. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="item"> The institute to delete. </param>
        [HttpDelete]
        public void Delete(Institute item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _instituteRepository.Delete(item);
            _db.SaveChanges();
        }

        /// <summary> Updates the institute. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated institute. </returns>
        [HttpPatch]
        public InstituteDTO Update(Institute item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _instituteRepository.Update(item);
            _db.SaveChanges();

            return new InstituteDTO(item);
        }

        /// <summary> Gets the institutes of faculty with specified id. </summary>
        /// <param name="facultyId"> Identifier of faculty whose institutes will be returned. </param>
        /// <returns> Institutes associated with specified faculty </returns>
        [Route("Faculties/{facultyId}/Institutes")]
        public IEnumerable<InstituteDTO> GetInstitutesInFaculty(int facultyId)
        {
            var results = _instituteRepository
                .Get(x => x.Faculty.Id == facultyId, null, y => y.Faculty)
                .Select(y => new InstituteDTO(y));

            return results;
        }
    }
}
