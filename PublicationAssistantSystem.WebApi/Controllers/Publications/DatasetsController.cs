﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.Core.PostAddJobs;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;
using WebGrease.Css.Extensions;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access for books in publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/Datasets")]
    public class DatasetsController : ApiController
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
            var datasets = _publicationBaseRepository.GetOfType<Dataset>();

            var mapped = datasets.Select(Mapper.Map<DatasetDTO>).ToList();
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
            var dataset = _publicationBaseRepository.GetOfType<Dataset>(x => x.Id == datasetId).SingleOrDefault();
            if (dataset == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<DatasetDTO>(dataset);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are datasets of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose datasets will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Datasets </remarks>
        /// <returns> Datasets associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Datasets")]
        [ResponseType(typeof(IEnumerable<DatasetDTO>))]
        public HttpResponseMessage GetDatasetsOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<Dataset>().Select(Mapper.Map<DatasetDTO>).ToList();
            return request.CreateResponse(mapped);
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
        /// <param name="request">Http request</param>
        /// <param name="item"> The dataset to add. </param>
        /// <remarks> POST api/Publications/Datasets </remarks>
        /// <returns> The added dataset DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(DatasetDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, DatasetDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Dataset>(item);

            dbObject.Employees = new List<Employee>();
            if (item.Employees != null)
            {
                foreach (var employee in item.Employees)
                {
                    var employeeToAdd = _employeeRepository.GetById(employee.Id);
                    if (employeeToAdd == null)
                    {
                        return request.CreateErrorResponse(
                            HttpStatusCode.PreconditionFailed,
                            string.Format("Not found employee with id:{0}", employee.Id));
                    }
                    dbObject.Employees.Add(employeeToAdd);
                }
            }

            _publicationBaseRepository.Insert(dbObject);
            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<DatasetDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the dataset.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Datasets </remarks>
        /// <returns> An updated dataset DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(DatasetDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, DatasetDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Dataset>(item);
            dbObject.Employees = null;

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            dbObject = (Dataset)_publicationBaseRepository.Get(x => x.Id == dbObject.Id, null, x => x.Employees).SingleOrDefault();

            foreach (var employee in dbObject.Employees.ToList())
                dbObject.Employees.Remove(employee);

            foreach (var employee in item.Employees)
            {
                var employeeToAdd = _employeeRepository.GetById(employee.Id);
                if (employeeToAdd == null)
                {
                    return request.CreateErrorResponse(
                        HttpStatusCode.PreconditionFailed,
                        string.Format("Not found employee with id:{0}", employee.Id));
                }
                dbObject.Employees.Add(employeeToAdd);
            }

            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<DatasetDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
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