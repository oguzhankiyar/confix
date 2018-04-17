using OK.Confix.SqlServer.DataContexts;
using System.Data.Entity.Migrations;

namespace OK.Confix.SqlServer.Migrations
{
    public sealed class MigrationConfiguration : DbMigrationsConfiguration<ConfixDataContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ConfixDataContext context)
        {
        }
    }
}