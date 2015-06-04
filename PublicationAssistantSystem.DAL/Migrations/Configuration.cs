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
                new Faculty { 
                    Name = "Automatyki, Elektroniki i Informatyki", 
                    Abbreviation = "AEiI", 
                    Institutes = new[]
                {
                    new Institute{ Name = "Instytut Informatyki", Divisions = new[]
                    {
                        new Division { Name = "Zespó³ Oprogramowania", Employees = new[]
                        {
                            new Employee { AcademicTitle = "Doktor", FirstName = "Jacek", LastName = "Widuch", Publications = new PublicationBase[]
                            {
                                new Article{Title = "Mój pierwszy artyku³ naukowy.", PublicationDate = DateTime.Now - TimeSpan.FromDays(100), PageFrom = 2, PageTo = 3, JournalEditionId = 1}, 
                                new Book { ISBN = "978-2-12-345680-3", Title = "O obrotach sfer niebieskich", PublicationDate = DateTime.Now, Publisher = "Egmont"},
                                new Book { ISBN = "343-5-23-352354-4", Title = "WiedŸmin", PublicationDate = DateTime.Now - TimeSpan.FromDays(4234), Publisher = "Helion"},
                                new Book { ISBN = "686-3-76-234234-6", Title = "Latarnik", PublicationDate = DateTime.Now, Publisher = "Axel-Springer"},
                            }},
                            new Employee
                            {
                                AcademicTitle = "Profesor", FirstName = "Stanis³aw", LastName = "Kozielski", Publications = new PublicationBase[]
                                {
                                    new Book { ISBN = "978-83-7508-556-3", Title = "Piêædziesi¹t twarzy Greya", PublicationDate = DateTime.Now -TimeSpan.FromDays(5), Publisher = "Adult Szpringer"},
                                    new Thesis {Title = "Teza o powstawaniu tez naukowych.", PublicationDate = DateTime.Now - TimeSpan.FromDays(200)}, 
                                    new Article { Title = "Jak pisaæ artyku³y naukowe.", PublicationDate = DateTime.Now - TimeSpan.FromDays(100), PageFrom = 10, PageTo = 50, JournalEditionId = 1},
                                    new Article { Title = "Jak niew pisaæ artyku³ów naukowych.", PublicationDate = DateTime.Now - TimeSpan.FromDays(200), PageFrom = 15, PageTo = 20, JournalEditionId = 1},
                                }
                            },
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
                new Faculty { Abbreviation = "MT", Name = "Mechaniczno Technologiczny"},
                new Faculty { Abbreviation = "GiG", Name = "Górnictwa i Geologii"},
                new Faculty { Abbreviation = "BUD", Name = "Budownictwa"},
            });
            SeedOrganisationUnits(context);
            SeedMiscellaneous(context);
            SeedPublications(context);

            context.SaveChanges();
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