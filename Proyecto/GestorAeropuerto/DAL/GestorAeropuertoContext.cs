using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.DAL
{
    public class GestorAeropuertoContext : DbContext
    {
        public GestorAeropuertoContext() : base("name=GestorAeropuertoEntities") { }

        public virtual DbSet<Aerolinea> Aerolinea { get; set; }
        public virtual DbSet<Avion> Avion { get; set; }
        public virtual DbSet<Billete> Billete { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Maleta> Maleta { get; set; }
        public virtual DbSet<Pasajero> Pasajero {get;set;}
        public virtual DbSet<Vuelo> Vuelo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
