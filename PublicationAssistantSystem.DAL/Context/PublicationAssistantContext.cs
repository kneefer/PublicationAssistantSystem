using System.Data.Entity;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PublicationAssistantSystem.DAL.Context
{
    public class PublicationAssistantContext : DbContext
    {
        public PublicationAssistantContext()
            : base("PublicationAssistantContext")
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<JournalEdition> JournalEditions { get; set; }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Institute> Institutes { get; set; }

        public DbSet<PublicationBase> Publications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
