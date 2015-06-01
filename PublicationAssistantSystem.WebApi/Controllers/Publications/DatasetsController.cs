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
    /// Provides access for books in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Datasets")]
    public class DatasetsController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public DatasetsController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are datasets.
        /// </summary>
        /// <remarks> GET: api/Publications/Datasets </remarks>
        /// <returns> All datasets. </returns>
        [Route("")]
        public IEnumerable<DatasetDTO> GetAllDatasets()
        {
            var results = _publicationBaseRepository.GetOfType<Dataset>();

            var mapped = results.Select(Mapper.Map<DatasetDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns dataset with given id.
        /// </summary>
        /// <param name="datasetId"> Dataset id. </param>
        /// <remarks> GET: api/Publications/Datasets/Id </remarks>
        /// <returns> Dataset with specified id. </returns>
        [Route("{datasetId:int}")]
        public DatasetDTO GetDatasetById(int datasetId)
        {
            var result = _publicationBaseRepository.GetOfType<Dataset>(x => x.Id == datasetId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<DatasetDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are datasets of employee with specified id.
        /// </summary>
        /// <param name="employeeId"> Identifier of employee whose datasets will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Datasets </remarks>
        /// <returns> Datasets associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Datasets")]
        public IEnumerable<DatasetDTO> GetDatasetsOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is Dataset);

            var mapped = results.Select(Mapper.Map<DatasetDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given dataset.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The dataset to add. </param>
        /// <remarks> POST api/Publications/Datasets </remarks>
        /// <returns> The added dataset DTO. </returns>
        [HttpPut]
        [Route("")]
        public DatasetDTO Add(DatasetDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dataset = Mapper.Map<Dataset>(item);

            _publicationBaseRepository.Insert(dataset);
            _db.SaveChanges();

            item.Id = dataset.Id;

            return item;
        }

        /// <summary>
        /// Updates the dataset.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Datasets </remarks>
        /// <returns> An updated dataset DTO. </returns>
        [HttpPatch]
        [Route("")]
        public DatasetDTO Update(DatasetDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dataset = Mapper.Map<Dataset>(item);

            _publicationBaseRepository.Update(dataset);
            _db.SaveChanges();

            var mapped = Mapper.Map<DatasetDTO>(dataset);
            return mapped;
        }

        /// <summary>
        /// Deletes the dataset with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="datasetId"> The id of dataset to delete. </param>
        /// <remarks> DELETE api/Publications/Datasets </remarks>
        [HttpDelete]
        [Route("{datasetId:int}")]
        public void Delete(int datasetId)
        {
            _publicationBaseRepository.Delete(datasetId);
            _db.SaveChanges();
        }
    }
}