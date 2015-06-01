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

        /// <summary>
        /// Gets all divisions.
        /// </summary>
        /// <remarks> GET: api/Divisions </remarks>
        /// <returns> All divisions. </returns>        
        [Route("")]     
        public IEnumerable<DivisionDTO> GetAll()
        {
            var divisions = _divisionRepository.Get();

            var mapped = divisions.Select(Mapper.Map<DivisionDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns division with given id.
        /// </summary>
        /// <param name="divisonId"> Division id. </param>
        /// <remarks> GET: api/Divisions/Id </remarks>
        /// <returns> Division with specified id. </returns>
        [Route("{divisonId:int}")]
        public DivisionDTO GetDivisionById(int divisonId)
        {
            var division = _divisionRepository.GetByID(divisonId);
            if (division == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<DivisionDTO>(division);
            return mapped;
        }

        /// <summary>
        /// Gets the divisions of institute with specified id.
        /// </summary>
        /// <param name="request">Http response</param>
        /// <param name="instituteId"> Identifier of institute whose divisions will be returned. </param>
        /// <remarks>GET api/Institutes/Id/Divisions </remarks>
        /// <returns> Divisions associated with specified institute </returns>
        [Route("~/api/Institutes/{instituteId:int}/Divisions")]
        [ResponseType(typeof(IEnumerable<DivisionDTO>))]
        public HttpResponseMessage GetDivisionsInInstitute(HttpRequestMessage request, int instituteId)
        {
            var institute = _instituteRepository.GetByID(instituteId);
            if (institute == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found institute with id:{0}", instituteId));
            }

            var mapped = institute.Divisions.Select(Mapper.Map<DivisionDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Adds the given division.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The division to add. </param>
        /// <remarks> POST api/Divisions </remarks>
        /// <returns> The added division DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DivisionDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, DivisionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Division>(item);

            if (_instituteRepository.GetByID(dbObject.InstituteId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found institute with id: {0}", dbObject.InstituteId));
            }

            _divisionRepository.Insert(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<DivisionDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the division.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http response</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Divisions </remarks>
        /// <returns> An updated division DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(DivisionDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, DivisionDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Division>(item);

            if (_instituteRepository.GetByID(dbObject.InstituteId) == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.PreconditionFailed,
                    string.Format("Not found institute with id: {0}", dbObject.InstituteId));
            }

            _divisionRepository.Update(dbObject);
            _db.SaveChanges();

            var mapped = Mapper.Map<DivisionDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
        }

        /// <summary>
        /// Deletes the division with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="divisionId"> The id of division to delete. </param>
        /// <remarks> DELETE api/Divisions </remarks>
        [HttpDelete]
        [Route("{divisionId:int}")]
        public void Delete(int divisionId)
        {
            _divisionRepository.Delete(divisionId);
            _db.SaveChanges();
        }
    }
}