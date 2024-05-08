using System.Data.Entity.Migrations;

namespace WebAPI.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NoteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NoteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
