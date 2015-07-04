using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PublicationAssistantSystem.Core.PostAddJobs;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
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
            var books = _publicationBaseRepository.GetOfType<Book>();

            var mapped = books.Select(Mapper.Map<BookDTO>).ToList();
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
            var book = _publicationBaseRepository.GetOfType<Book>(x => x.Id == bookId).SingleOrDefault();
            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var mapped = Mapper.Map<BookDTO>(book);
            return mapped;
        }

        /// <summary> 
        /// Gets the publications that are books of employee with specified id.
        /// </summary>
        /// <param name="request">Http request</param>
        /// <param name="employeeId"> Identifier of employee whose books will be returned. </param>
        /// /// <remarks> GET: api/Employees/Id/Books </remarks>
        /// <returns> Books associated with specified employee. </returns>
        [Route("~/api/Employees/{employeeId:int}/Books")]
        [ResponseType(typeof(IEnumerable<BookDTO>))]
        public HttpResponseMessage GetBooksOfEmployee(HttpRequestMessage request, int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee == null)
            {
                return request.CreateErrorResponse(
                    HttpStatusCode.NotFound,
                    string.Format("Not found employee with id:{0}", employeeId));
            }

            var mapped = employee.Publications.OfType<Book>().Select(Mapper.Map<BookDTO>).ToList();
            return request.CreateResponse(mapped);
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
        /// <param name="request">Http request</param>
        /// <param name="item"> The book to add. </param>
        /// <remarks> POST api/Publications/Books </remarks>
        /// <returns> The added book DTO. </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(BookDTO))]
        public HttpResponseMessage Add(HttpRequestMessage request, BookDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Book>(item);

            dbObject.Employees = new List<Employee>();
            if (item.Employees != null)
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

            _publicationBaseRepository.Insert(dbObject);
            _db.SaveChanges();

            var jobs = new PublicationsJobs(dbObject);
            jobs.Start();

            var mapped = Mapper.Map<BookDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.Created, mapped);
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="request">Http request</param>
        /// <param name="item"> The item with updated content. </param>
        /// <remarks> PATCH api/Publications/Books </remarks>
        /// <returns> An updated book DTO. </returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(BookDTO))]
        public HttpResponseMessage Update(HttpRequestMessage request, BookDTO item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var dbObject = Mapper.Map<Book>(item);
            dbObject.Employees = null;

            _publicationBaseRepository.Update(dbObject);
            _db.SaveChanges();

            dbObject = (Book)_publicationBaseRepository.Get(x => x.Id == dbObject.Id, null, x => x.Employees).SingleOrDefault();

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

            var mapped = Mapper.Map<BookDTO>(dbObject);
            return request.CreateResponse(HttpStatusCode.NoContent, mapped);
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