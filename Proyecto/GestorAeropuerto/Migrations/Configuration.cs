namespace GestorAeropuerto.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GestorAeropuerto.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<GestorAeropuerto.DAL.GestorAeropuertoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GestorAeropuerto.DAL.GestorAeropuertoContext context)
        {
            //  This method will be called after migrating to the latest version.
            Usuario u = new Usuario
            {
                Nombre = "admin",
                Password = "abc123."
            };
            context.Usuario.AddOrUpdate(u);
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
