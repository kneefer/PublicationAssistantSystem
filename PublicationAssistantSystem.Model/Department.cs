using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.Model
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }

    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Faculty Faculty { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
    }

    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Institute Institute { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string AcademicTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Division Division { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }

    public abstract class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }

        public bool IsOnMNISZW { get; set; }
        public bool IsOnWOS { get; set; }
        public bool IsOnJCR { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }

    public class Article : Publication
    {
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public JournalEdition Journal { get; set; }
    }

    public class JournalEdition
    {
        [Key]
        public string ISSN { get; set; }

        public Journal Journal { get; set; }
    }

    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<JournalEdition> JournalEditions { get; set; }
    }

    public class Book : Publication
    {
        [Key]
        public string ISBN { get; set; }

        public string Publisher { get; set; }
    }

    public class Chapter : Publication
    {
    }

    public class Dataset : Publication
    {

    }

    public class ConferencePaper : Publication
    {

    }

    public class Patent : Publication
    {

    }

    public class TechnicalReport : Publication
    {

    }

    public class Thesis : Publication
    {

    }
}
