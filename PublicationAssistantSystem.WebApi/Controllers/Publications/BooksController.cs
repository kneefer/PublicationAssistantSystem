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
        [Route("")]
        public IEnumerable<BookDTO> GetAllBooks()
        {
            var results = _publicationBaseRepository.GetOfType<Book>();

            var mapped = results.Select(Mapper.Map<BookDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Returns book with given id.
        /// </summary>
        /// <param name="bookId"> Book id. </param>
        /// <remarks> GET: api/Publications/Books/Id </remarks>
        /// <returns> Book with specified id. </returns>
        [Route("{bookId:int}")]
        public BookDTO GetBook(int bookId)
        {
            var result = _publicationBaseRepository.GetOfType<Book>(x => x.Id == bookId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<BookDTO>(result);
            return mapped;
        }
    }
}