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
    [RoutePrefix("api/Publications/Books")]
    public class BooksController : ApiController
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
        public BooksController(
            IPublicationAssistantContext db,
            IEmployeeRepository employeeRepository,
            IPublicationBaseRepository publicationBaseRepository)
        {
            _db = db;
            _employeeRepository = employeeRepository;
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
        public BookDTO GetBookById(int bookId)
        {
            var result = _publicationBaseRepository.GetOfType<Book>(x => x.Id == bookId).FirstOrDefault();
            if (result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<BookDTO>(result);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are books of employee with specified id.
        ///  </summary>
        /// <param name="employeeId"> Identifier of employee whose books will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Books </remarks>
        /// <returns> Books associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId}/Books")]
        public IEnumerable<BookDTO> GetBooksOfEmployee(int employeeId)
        {
            var employee = _employeeRepository.Get(x => x.Id == employeeId, null, x => x.Publications).SingleOrDefault();
            if (employee == null)
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);

            var results = employee.Publications.Where(x => x is Book);

            var mapped = results.Select(Mapper.Map<BookDTO>).ToList();
            return mapped;
        }

        /// <summary>
        /// Adds the given book.
        /// </summary>
        /// <exception cref="ArgumentNullException">   
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <exception cref="HttpResponseException">
        /// Thrown when a HTTP Response error condition occurs. 
        /// </exception>
        /// <param name="item"> The book to add. </param>
        /// <remarks> POST api/Publications/Books </remarks>
        /// <returns> The added book DTO. </returns>
        [HttpPost]
        [Route("")]
        public BookDTO Add(BookDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var book = Mapper.Map<Book>(item);

            _publicationBaseRepository.Insert(book);
            _db.SaveChanges();

            item.Id = book.Id;

            return item;
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Books </remarks>
        /// <returns> An updated book DTO. </returns>
        [HttpPatch]
        [Route("")]
        public BookDTO Update(BookDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var book = Mapper.Map<Book>(item);

            _publicationBaseRepository.Update(book);
            _db.SaveChanges();

            return item;
        }

        /// <summary>
        /// Deletes the book with given id.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="bookId"> The id of book to delete. </param>
        /// <remarks> DELETE api/Publications/Books </remarks>
        [HttpDelete]
        [Route("{bookId:int}")]
        public void Delete(int bookId)
        {
            _publicationBaseRepository.Delete(bookId);
            _db.SaveChanges();
        }
    }
}