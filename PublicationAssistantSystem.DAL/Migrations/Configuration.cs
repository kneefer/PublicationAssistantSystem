using System;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.PublicationAssistantContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PublicationAssistantSystem.DAL.Context.PublicationAssistantContext";
        }

        protected override void Seed(Context.PublicationAssistantContext context)
        {
            context.Faculties.AddOrUpdate(new[]
            {
                new Faculty { Name = "AEiI", Institutes = new[]
                {
                    new Institute{ Name = "Instytut Informatyki", Divisions = new[]
                    {
                        new Division { Name = "Zak≥ad Oprogramowania", Employees = new[]
                        {
                            new Employee { AcademicTitle = "Doktor", FirstName = "Jacek", LastName = "Widuch", Publications = new[]
                            {
                                new Book { ISBN = "978-2-12-345680-3", Title = "O obrotach sfer niebieskich", PublicationDate = DateTime.Now, Publisher = "Egmont"},
                                new Book { ISBN = "343-5-23-352354-4", Title = "Wiedümin", PublicationDate = DateTime.Now - TimeSpan.FromDays(4234), Publisher = "Helion"},
                                new Book { ISBN = "686-3-76-234234-6", Title = "Latarnik", PublicationDate = DateTime.Now, Publisher = "Axel-Springer"},
                            }},
                            new Employee { AcademicTitle = "Profesor", FirstName = "Stanis≥aw", LastName = "Kozielski"},
                            new Employee { AcademicTitle = "Magister", FirstName = "Ewa", LastName = "Lach"},
                        }},
                    }},
                    new Institute{ Name = "Instytut Automatyki"},
                    new Institute{ Name = "Instytut Elektroniki"},
                }},
                new Faculty { Name = "MT"},
                new Faculty { Name = "GiG"},
                new Faculty { Name = "BUD"},
            });

            context.SaveChanges();
        }
    }
}
