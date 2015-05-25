using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO;
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
                .Get(null, null, "Faculty")
                .Select((y) => new InstituteDTO(y));

            return results;
        }

        /// <summary> Adds the given institute. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The faculty to add. </param>
        /// <returns> The added faculty. </returns>
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
                Id = item.Id,
                Name = item.Name,
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
        /// <param name="item"> The institute to update. </param>
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

        /// <summary>   Updates the institute. </summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns>   An updated institute. </returns>
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

        [Route("Faculty/{facultyId}/Institutes")]
        public IEnumerable<InstituteDTO> GetInstitutesInFaculty(int facultyId)
        {
            var results = _instituteRepository.Get((x) => x.Faculty.Id == facultyId, null, "Faculty")
                                              .Select((y) => new InstituteDTO(y));
            return results;
        }
    }
}
