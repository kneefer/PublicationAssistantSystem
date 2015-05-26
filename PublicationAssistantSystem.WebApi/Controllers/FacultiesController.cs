﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    public class FacultiesController : ApiController
    {
        private readonly IPublicationAssistantContext _db;
        private readonly IFacultyRepository _facultyRepository;

        public FacultiesController(
            IPublicationAssistantContext db,
            IFacultyRepository facultyRepository)
        {
            _db = db;
            _facultyRepository = facultyRepository;
        }

        /// <summary> Gets all faculties. </summary>
        /// <returns> All faculties. </returns>
        
        public IEnumerable<FacultyDTO> GetAll()
        {
            var results = _facultyRepository
                .Get()
                .Select(x => new FacultyDTO(x));

            return results;
        }

        /// <summary> Adds the given faculty. </summary>
        /// <exception cref="ArgumentNullException">    
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="item"> The faculty to add. </param>
        /// <returns> The added faculty. </returns>
        
        [HttpPost]
        public FacultyDTO Add(FacultyDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var faculty = new Faculty
            {
                Id           = item.Id,
                Name         = item.Name,
                Abbreviation = item.Name,
            };

            _facultyRepository.Insert(faculty);
            _db.SaveChanges();

            item.Id = faculty.Id;

            return item;
        }

        /// <summary> Deletes the given faculty. </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The faculty to delete. </param>

        [HttpDelete]
        public void Delete(Faculty item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _facultyRepository.Delete(item);
            _db.SaveChanges();
        }
        /// <summary> Updates the faculty. </summary>
        /// <exception cref="ArgumentNullException"> 
        /// Thrown when one or more required arguments are null. 
        /// </exception>
        /// <param name="item"> The item with updated content. </param>
        /// <returns> An updated Faculty. </returns>
        [HttpPatch]
        public FacultyDTO Update(Faculty item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _facultyRepository.Update(item);
            _db.SaveChanges();

            return new FacultyDTO(item);
        }
    }
}