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
    /// Provides access for conference papers in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/ConferencePapers")]
    public class ConferencePapersController : ApiController
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
        public ConferencePapersController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are conference papers.
        /// </summary>
        /// <remarks> GET: api/Publications/ConferencePapers </remarks>
        /// <returns> All conference papers. </returns>
        [Route("")]
        public IEnumerable<ConferencePaperDTO> GetAllConferencePapers()
        {
            var results = _publicationBaseRepository.GetOfType<ConferencePaper>();

            var mapped = results.Select(Mapper.Map<ConferencePaperDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns conference paper with given id.
        /// </summary>
        /// <param name="conferencePaperId"> Conference paper id. </param>
        /// <remarks> GET: api/Publications/ConferencePapers/Id </remarks>
        /// <returns> Conference paper with specified id. </returns>
        [Route("{conferencePaperId:int}")]
        public ConferencePaperDTO GetConferencePaperById(int conferencePaperId)
        {
            var result = _publicationBaseRepository.GetOfType<ConferencePaper>(x => x.Id == conferencePaperId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<ConferencePaperDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are conference papers of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose conference papers will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/ConferencePapers </remarks>
        /// <returns> Conference papers associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/ConferencePapers")]
        public IEnumerable<ConferencePaperDTO> GetConferencePapersOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is ConferencePaper);

            var mapped = results.Select(Mapper.Map<ConferencePaperDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given conference paper.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The conference paper to add. </param>
        /// <remarks> POST api/Publications/ConferencePapers </remarks>
        /// <returns> The added conference paper DTO. </returns>
        [HttpPost]
        [Route("")]
        public ConferencePaperDTO Add(ConferencePaperDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var conferencePaper = Mapper.Map<ConferencePaper>(item);

            _publicationBaseRepository.Insert(conferencePaper);
            _db.SaveChanges();

            item.Id = conferencePaper.Id;

            return item;
        }

        /// <summary>
        /// Updates the conference paper.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/ConferencePapers </remarks>
        /// <returns> An updated conference paper DTO. </returns>
        [HttpPut]
        [Route("")]
        public ConferencePaperDTO Update(ConferencePaperDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var conferencePaper = Mapper.Map<ConferencePaper>(item);

            _publicationBaseRepository.Update(conferencePaper);
            _db.SaveChanges();

            var mapped = Mapper.Map<ConferencePaperDTO>(conferencePaper);
            return mapped;
        }

        /// <summary>
        /// Deletes the conference paper with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="conferencePaperId"> The id of conference paper to delete. </param>
        /// <remarks> DELETE api/Publications/ConferencePapers </remarks>
        [HttpDelete]
        [Route("{conferencePaperId:int}")]
        public void Delete(int conferencePaperId)
        {
            _publicationBaseRepository.Delete(conferencePaperId);
            _db.SaveChanges();
        }
    }
}