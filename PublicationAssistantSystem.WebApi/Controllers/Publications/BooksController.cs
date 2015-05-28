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
    [RoutePrefix("api/Publications/Books")]
    public class BooksController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IPublicationBaseRepository _publicationBaseRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="db"> Db context. </param>
        /// <param name="publicationBaseRepository"> Repository of publications. </param>
        public BooksController(
            IPublicationAssistantContext db, 
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _publicationBaseRepository = publicationBaseRepository;
        }

        /// <summary>
        /// Returns all publications that are books.
        /// </summary>
        /// <remarks> GET: api/Publications/Books </remarks>
        /// <returns> All books. </returns>
        public IEnumerable<PublicationBaseDTO> GetAllBooks()
        {
            var results = _publicationBaseRepository.Get(p => p is Book).ToList();
            var mapped = results.Select(Mapper.DynamicMap<PublicationBaseDTO>);
            var toReturn = mapped.ToList();
            return toReturn;
        }

        /// <summary>
        /// Returns book with given id.
        /// </summary>
        /// <param name="id"> Book id. </param>
        /// <remarks> GET: api/Publications/Books/Id </remarks>
        /// <returns> Book with specified id. </returns>
        public PublicationBaseDTO GetBook(int id)
        {
            var result = _publicationBaseRepository.Get(p => p is Book && p.Id == id).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.DynamicMap<PublicationBaseDTO>(result);
            return mapped;
        }
    }
}