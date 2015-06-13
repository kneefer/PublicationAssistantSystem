using System;
using PublicationAssistantSystem.DAL.Helpers;
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
            var journalEdition = new JournalEdition
            {
                PublishDate = DateTime.Now - TimeSpan.FromDays(2342),
                VolumeNumber = 3,
            };

            context.Journals.AddOrUpdate(new Journal
            {
                ISSN = "2434-561X",
                JournalEditions = new[] { journalEdition },
                Title = "Zbi�r artyku��w",
            });

            context.Faculties.AddOrUpdate(new[]
            {
                new Faculty { 
                    Name = "Automatyki, Elektroniki i Informatyki", 
                    Abbreviation = "AEiI", 
                    Institutes = new[]
                {
                    new Institute{ Name = "Instytut Informatyki", Divisions = new[]
                    {
                        new Division { Name = "Zesp� Oprogramowania", Employees = new[]
                        {
                            new Employee { AcademicTitle = "Doktor", FirstName = "Jacek", LastName = "Widuch", Publications = new PublicationBase[]
                            {
                                new Article{Title = "M�j pierwszy artyku� naukowy.", PublicationDate = DateTime.Now - TimeSpan.FromDays(100), PageFrom = 2, PageTo = 3, JournalEdition = journalEdition}, 
                                new Book { ISBN = "978-2-12-345680-3", Title = "O obrotach sfer niebieskich", PublicationDate = DateTime.Now, Publisher = "Egmont"},
                                new Book { ISBN = "343-5-23-352354-4", Title = "Wied�min", PublicationDate = DateTime.Now - TimeSpan.FromDays(4234), Publisher = "Helion"},
                                new Book { ISBN = "686-3-76-234234-6", Title = "Latarnik", PublicationDate = DateTime.Now, Publisher = "Axel-Springer"},
                            }},
                            new Employee
                            {
                                AcademicTitle = "Profesor", FirstName = "Stanis�aw", LastName = "Kozielski", Publications = new PublicationBase[]
                                {
                                    new Book { ISBN = "978-83-7508-556-3", Title = "Pi��dziesi�t twarzy Greya", PublicationDate = DateTime.Now -TimeSpan.FromDays(5), Publisher = "Adult Szpringer"},
                                    new Thesis {Title = "Teza o powstawaniu tez naukowych.", PublicationDate = DateTime.Now - TimeSpan.FromDays(200)}, 
                                    new Article { Title = "Jak pisa� artyku�y naukowe.", PublicationDate = DateTime.Now - TimeSpan.FromDays(100), PageFrom = 10, PageTo = 50, JournalEdition = journalEdition },
                                    new Article { Title = "Jak niew pisa� artyku��w naukowych.", PublicationDate = DateTime.Now - TimeSpan.FromDays(200), PageFrom = 15, PageTo = 20, JournalEdition = journalEdition},
                                }
                            },
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
                        new Division{Name="Zak�ad Mikroelektroniki i Nanotechnologii"},
                    }},
                }},
                new Faculty { Abbreviation = "MT", Name = "Mechaniczno Technologiczny"},
                new Faculty { Abbreviation = "GiG", Name = "G�rnictwa i Geologii"},
                new Faculty { Abbreviation = "BUD", Name = "Budownictwa"},
            });
            SeedOrganisationUnits(context);
            SeedMiscellaneous(context);
            SeedPublications(context);

            context.SaveChangesWithErrors();
        }

        private void SeedOrganisationUnits(Context.PublicationAssistantContext context)
        {
            
        }

        private void SeedMiscellaneous(Context.PublicationAssistantContext context)
        {

        }

        private void SeedPublications(Context.PublicationAssistantContext context)
        {

        }
    }
}