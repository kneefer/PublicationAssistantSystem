using System.Data.Entity;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Text;

namespace PublicationAssistantSystem.DAL.Context
{
    public class PublicationAssistantContext : DbContext, IPublicationAssistantContext
    {
        public PublicationAssistantContext()
            : base("PublicationAssistantContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PublicationAssistantContext>());
        }

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

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
