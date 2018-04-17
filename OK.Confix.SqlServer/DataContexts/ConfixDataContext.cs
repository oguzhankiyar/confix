using OK.Confix.SqlServer.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OK.Confix.SqlServer.DataContexts
{
    public class ConfixDataContext : DbContext
    {
        public ConfixDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<ApplicationEntity> Applications { get; set; }

        public DbSet<EnvironmentEntity> Environments { get; set; }

        public DbSet<ConfigurationEntity> Configurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<EnvironmentEntity>()
                        .HasRequired(x => x.Application)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConfigurationEntity>()
                        .HasRequired(x => x.Application)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConfigurationEntity>()
                        .HasOptional(x => x.Environment)
                        .WithMany()
                        .WillCascadeOnDelete(false);
        }
    }
}