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
                new Faculty { Name = "Automatyki, Elektroniki i Informatyki", Institutes = new[]
                {
                    new Institute{ Name = "Instytut Informatyki", Divisions = new[]
                    {
                        new Division { Name = "Zespó³ Oprogramowania", Employees = new[]
                        {
                            new Employee { AcademicTitle = "Doktor", FirstName = "Jacek", LastName = "Widuch", Publications = new[]
                            {
                                new Book { ISBN = "978-2-12-345680-3", Title = "O obrotach sfer niebieskich", PublicationDate = DateTime.Now, Publisher = "Egmont"},
                                new Book { ISBN = "343-5-23-352354-4", Title = "WiedŸmin", PublicationDate = DateTime.Now - TimeSpan.FromDays(4234), Publisher = "Helion"},
                                new Book { ISBN = "686-3-76-234234-6", Title = "Latarnik", PublicationDate = DateTime.Now, Publisher = "Axel-Springer"},
                            }},
                            new Employee { AcademicTitle = "Profesor", FirstName = "Stanis³aw", LastName = "Kozielski"},
                            new Employee { AcademicTitle = "Magister", FirstName = "Ewa", LastName = "Lach"},
                        }},
                        new Division { Name = "Zespó³ Teorii Informatyki" },
                        new Division { Name = "Zespó³ Urz¹dzeñ Informatyki" },
                        new Division { Name = "Zespó³ Mikroinformatyki i Teorii Automatów Cyfrowych" },
                        new Division { Name = "Zespó³ Teorii i Projektowania Systemów Komputerowych" },
                        new Division { Name = "Zak³ad Grafiki, Wizji i Symulacji Komputerowej" }
                    }},
                    new Institute{ Name = "Instytut Automatyki", Divisions = new[]
                    {
                        new Division{ Name = "Zak³ad Sterowania i Robotyki"}, 
                        new Division{ Name = "Zak³ad Pomiarów i Systemów Sterowania"}, 
                        new Division{ Name = "Zak³ad Urz¹dzeñ i Uk³adów Automatyki"}, 
                        new Division{ Name = "Zak³ad In¿ynierii Systemów"}, 
                        new Division{ Name = "Zak³ad Analizy Eksploracyjnej Danych"} 
                    }},
                    new Institute{ Name = "Instytut Elektroniki", Divisions = new[]
                    {
                        new Division{Name="Zak³ad Elektroniki Biomedycznej"}, 
                        new Division{Name="Zak³ad Podstaw Elektroniki i Radiotechniki"}, 
                        new Division{Name="Zak³ad Telekomunikacji"}, 
                        new Division{Name="Zak³ad Teorii Obwodów i Sygna³ów"}, 
                        new Division{Name="Zak³ad Uk³adów Cyfrowych i Mikroprocesorowych"}, 
                        new Division{Name="Zak³ad Mikroelektroniki i Nanotechnologii"} 
                    }},
                }},
                new Faculty { Name = "MT"},
                new Faculty { Name = "GiG"},
                new Faculty { Name = "BUD"},
            });

            context.SaveChanges();
        }
    }
}
