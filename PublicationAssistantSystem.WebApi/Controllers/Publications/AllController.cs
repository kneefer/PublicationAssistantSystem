﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using PublicationAssistantSystem.Core.Exports;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers.Publications
{
    /// <summary>
    /// Provides access to publications repository.
    /// </summary>
    [RoutePrefix("api/Publications/All")]
    public class AllController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IJournalRepository _journalRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJournalEditionRepository _journalEditionRepository;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="journalRepository"> Repository of journals. </param>
        /// <param name="employeeRepository"> Repository of employees. </param>
        /// <param name="journalEditionRepository"> Repository of journal editions. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public AllController(
            IPublicationAssistantContext db,
            IJournalRepository journalRepository, 
            IEmployeeRepository employeeRepository,
            IJournalEditionRepository journalEditionRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
            _publicationBaseRepository = publicationBaseRepository;
            _journalRepository = journalRepository;
            _journalEditionRepository = journalEditionRepository;
        }
        
        /// <summary>
        /// Returns all publications.
        /// </summary>
        /// <remarks> GET: api/Publications/All </remarks>
        /// <returns> All publications. </returns>
        [Route("")]
        public IEnumerable<PublicationBaseDTO> GetAll()
        {
            var publications = _publicationBaseRepository.Get();

            var mapped = publications.Select(Mapper.Map<PublicationBaseDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns publication with given id.
        /// </summary>
        /// <param name="publicationId"> Publication id. </param>
        /// /// <remarks> GET: api/Publications/All/Id </remarks>
        /// <returns> Publication with specified id. </returns>
        [Route("{publicationId:int}")]
        public PublicationBaseDTO GetPublicationById(int publicationId)
        {
            var publication = _publicationBaseRepository.GetById(publicationId);
            if (publication == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<PublicationBaseDTO>(publication);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose publications will be returned. </param>
        /// <remarks> GET: api/Employees/Id/Publications </remarks>
        /// <returns> Publications associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Publications")]
        [ResponseType(typeof(IEnumerable<PublicationBaseDTO>))]
        public HttpResponseMessage GetPublicationsOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.Select(Mapper.Map<PublicationBaseDTO>).ToList();
            return request.CreateResponse(mapped);
        }

        /// <summary>
        /// Returns publications serialized to XML document.
        /// </summary>
        /// <returns> XML document with publications. </returns>
        [Route("AsXml")]
        public HttpResponseMessage GetAllAsXML()
        {
            var publications = _publicationBaseRepository.Get();
            var mapped = publications.Select(Mapper.Map<PublicationBaseDTO>).ToArray();

            using (var stream = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    Encoding = Encoding.UTF8
                };

                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    var xml = new XmlSerializer(typeof(PublicationBaseDTO[]));
                    xml.Serialize(xmlWriter, mapped);

                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(stream.ToArray());
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("text/xml");
                    result.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = string.Format("DumpAll_{0}.xml", DateTime.Now.ToString("yyyyMMddHHmmss")),
                        };
                    return result; 
                }
            }
        }

        /// <summary>
        /// Returns publications serialized to BIB document.
        /// </summary>
        /// <returns> BIB document with publications. </returns>
        [Route("AsBib")]
        public HttpResponseMessage GetAllAsBIB()
        {
            var publications = _publicationBaseRepository.Get();
            AppendJournalInformations(publications);

            using (var stream = new MemoryStream())
            {
                var creator = new BIBCreator(stream);
                creator.CreateToStream(publications);

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(stream.ToArray());
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("text/bib");
                result.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = string.Format("DumpAll_{0}.bib", DateTime.Now.ToString("yyyyMMddHHmmss")),
                    };
                return result;
            }
        }

        /// <summary>
        /// Returns publications serialized to CSV document.
        /// </summary>
        /// <returns> CSV document with publications. </returns>
        [Route("AsCSV")]
        public HttpResponseMessage GetAllAsCSV()
        {
            var publications = _publicationBaseRepository.Get();
            AppendJournalInformations(publications);

            using (var stream = new MemoryStream())
            {
                var creator = new CSVCreator(stream);
                creator.CreateToStream(publications);

                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(stream.ToArray());
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("text/csv");
                result.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = string.Format("DumpAll_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")),
                    };
                return result;
            }
        }

        /// <summary>
        /// Deletes the publication with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="publicationId"> The id of publication to delete. </param>
        /// <remarks> DELETE api/Publications/Id </remarks>
        [HttpDelete]
        [Route("~/api/Publications/{publicationId:int}")]
        public void Delete(int publicationId)
        {
            _publicationBaseRepository.Delete(publicationId);
            _db.SaveChanges();
        }

        #region Helpers

        /// <summary>
        /// Adds informations about journal to all articles in publications
        /// </summary>
        /// <param name="publications">Publications collection</param>
        private void AppendJournalInformations(IEnumerable<PublicationBase> publications)
        {
            var publicationBases = publications as IList<PublicationBase> ?? publications.ToList();
            if (publicationBases.Count() < 0) return;

            foreach (var publication in publicationBases)
            {
                var article = publication as Article;
                if (article == null) continue;

                var journalEdition = _journalEditionRepository.Get().SingleOrDefault(x => x.Id == article.JournalEditionId);
                if (journalEdition == null) continue;

                var journal = _journalRepository.Get().SingleOrDefault(x => x.Id == journalEdition.JournalId);
                if (journal == null) continue;

                article.JournalEdition = journalEdition;
                article.JournalEdition.Journal = journal;
            }
        }

        #endregion Helpers
    }
}