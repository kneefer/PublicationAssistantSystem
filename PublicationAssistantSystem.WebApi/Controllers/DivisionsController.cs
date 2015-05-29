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
    /// Provides access to divisions repository.
    /// </summary>
    [RoutePrefix("api/Divisions")]
    public class DivisionsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IInstituteRepository _instituteRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="divisionRepository"> Repository of divisions. </param>
        /// <param name="instituteRepository"> Repository of institutes. </param>
        public DivisionsController(
            IPublicationAssistantContext db,
            IDivisionRepository divisionRepository,
            IInstituteRepository instituteRepository)
        {
            _db = db;
            _divisionRepository = divisionRepository;
            _instituteRepository = instituteRepository;
        }

        /// <summary> Gets all divisions. </summary>
        /// <returns> All divisions. </returns>        
        [Route("")]     
        public IEnumerable<DivisionDTO> GetAll()
        {
            var results = _divisionRepository.Get(null, null, x => x.Institute);

            var mapped = results.Select(Mapper.Map<DivisionDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns division with given id.
        /// </summary>
        /// <param name="divisonId"> Division id. </param>
        /// <returns> Division with specified id. </returns>
        [Route("{divisonId:int}")]
        public DivisionDTO GetDivisionById(int divisonId)
        {
            var result = _divisionRepository.Get(d => d.Id == divisonId, null, d => d.Institute).SingleOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<DivisionDTO>(result);
            return mapped;
        }

        /// <summary> Gets the divisions of institute with specified id. </summary>
        /// <param name="instituteId"> Identifier of institute whose divisions will be returned. </param>
        /// <returns> Divisions associated with specified institute </returns>
        [Route("~/api/Institutes/{instituteId}/Divisions")]
        public IEnumerable<DivisionDTO> GetDivisionsInInstitute(int instituteId)
        {
            var results = _divisionRepository.Get(x => x.Institute.Id == instituteId, null, y => y.Institute);

            var mapped = results.Select(Mapper.Map<DivisionDTO>).ToList();
            return mapped;
        }

        /// <summary> Adds the given division. </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The division to add. </param>
        /// <returns> The added division. </returns>
        [HttpPost]
        public DivisionDTO Add(DivisionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var institute = _instituteRepository.Get(x => x.Id == item.InstituteId).FirstOrDefault();
            if (institute == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var division = new Division
            {
                Id        = item.Id,
                Name      = item.Name,
                Institute = institute
            };

            _divisionRepository.Insert(division);
            _db.SaveChanges();

            item.Id = division.Id;

            return item;
        }

        /// <summary>   Deletes the given division. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="divisionId"> The ID of division to delete. </param>
        [HttpDelete]
        [Route("{divisionId:int}")]
        public void Delete(int divisionId)
        {
            _divisionRepository.Delete(divisionId);
            _db.SaveChanges();
        }

        /// <summary> Updates the division. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated division. </returns>
        [HttpPatch]
        public DivisionDTO Update(DivisionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var institute = _instituteRepository.Get(x => x.Id == item.InstituteId).FirstOrDefault();
            if (institute == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var division = new Division
            {
                Name = item.Name,
                Institute = institute,
            };
            
            _divisionRepository.Update(division);
            _db.SaveChanges();

            item.Id = division.Id;

            return item;
        }
    }
}