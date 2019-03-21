using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;
using GestorAeropuerto.Migrations;

namespace GestorAeropuerto.DAL
{
    public class GestorAeropuertoContext : DbContext
    {
        public GestorAeropuertoContext() : base("name=GestorAeropuertoEntities")
        {
            if (Database.Exists())
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<GestorAeropuertoContext, Configuration>());
            else
                Database.SetInitializer(new CrearDB());
        }

        public virtual DbSet<Aerolinea> Aerolinea { get; set; }
        public virtual DbSet<Avion> Avion { get; set; }
        public virtual DbSet<Billete> Billete { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Maleta> Maleta { get; set; }
        public virtual DbSet<Pasajero> Pasajero { get; set; }
        public virtual DbSet<Vuelo> Vuelo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        class CrearDB : CreateDatabaseIfNotExists<GestorAeropuertoContext>
        {
            protected override void Seed(GestorAeropuertoContext context)
            {


                base.Seed(context);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
