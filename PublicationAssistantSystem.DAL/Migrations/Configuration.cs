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
                        new Division { Name = "Zesp� Oprogramowania", Employees = new[]
                        {
                            new Employee { AcademicTitle = "Doktor", FirstName = "Jacek", LastName = "Widuch", Publications = new[]
                            {
                                new Book { ISBN = "978-2-12-345680-3", Title = "O obrotach sfer niebieskich", PublicationDate = DateTime.Now, Publisher = "Egmont"},
                                new Book { ISBN = "343-5-23-352354-4", Title = "Wied�min", PublicationDate = DateTime.Now - TimeSpan.FromDays(4234), Publisher = "Helion"},
                                new Book { ISBN = "686-3-76-234234-6", Title = "Latarnik", PublicationDate = DateTime.Now, Publisher = "Axel-Springer"},
                            }},
                            new Employee { AcademicTitle = "Profesor", FirstName = "Stanis�aw", LastName = "Kozielski"},
                            new Employee { AcademicTitle = "Magister", FirstName = "Ewa", LastName = "Lach"},
                        }},
                        new Division { Name = "Zesp� Teorii Informatyki" },
                        new Division { Name = "Zesp� Urz�dze� Informatyki" },
                        new Division { Name = "Zesp� Mikroinformatyki i Teorii Automat�w Cyfrowych" },
                        new Division { Name = "Zesp� Teorii i Projektowania System�w Komputerowych" },
                        new Division { Name = "Zak�ad Grafiki, Wizji i Symulacji Komputerowej" }
                    }},
                    new Institute{ Name = "Instytut Automatyki", Divisions = new[]
                    {
                        new Division{ Name = "Zak�ad Sterowania i Robotyki"}, 
                        new Division{ Name = "Zak�ad Pomiar�w i System�w Sterowania"}, 
                        new Division{ Name = "Zak�ad Urz�dze� i Uk�ad�w Automatyki"}, 
                        new Division{ Name = "Zak�ad In�ynierii System�w"}, 
                        new Division{ Name = "Zak�ad Analizy Eksploracyjnej Danych"} 
                    }},
                    new Institute{ Name = "Instytut Elektroniki", Divisions = new[]
                    {
                        new Division{Name="Zak�ad Elektroniki Biomedycznej"}, 
                        new Division{Name="Zak�ad Podstaw Elektroniki i Radiotechniki"}, 
                        new Division{Name="Zak�ad Telekomunikacji"}, 
                        new Division{Name="Zak�ad Teorii Obwod�w i Sygna��w"}, 
                        new Division{Name="Zak�ad Uk�ad�w Cyfrowych i Mikroprocesorowych"}, 
                        new Division{Name="Zak�ad Mikroelektroniki i Nanotechnologii"} 
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
