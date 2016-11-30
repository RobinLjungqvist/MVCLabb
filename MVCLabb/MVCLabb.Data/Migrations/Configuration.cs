namespace MVCLabb.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCLabb.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVCLabb.Data.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Users.AddOrUpdate(
              p => p,
              new Models.UserEntityModel { id = 1, FirstName = "Robin", LastName = "Ljungqvist", Password = "Testtest1", Email = "Robin@Email.com", guid = Guid.Parse("{3027308A-5C93-4694-869A-BA40F042F94C}") }
            );

        }
    }
}
