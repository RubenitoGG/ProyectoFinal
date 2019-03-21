using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;
using GestorAeropuerto.Migrations;
using System.Data.Entity.Migrations;

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
                List<Aerolinea> aerolineas = new List<Aerolinea>()
                {
                    new Aerolinea { Nombre = "Vueling", Telefono = "655993665"},
                    new Aerolinea { Nombre = "Air Europa", Telefono = "785556552"},
                    new Aerolinea { Nombre = "Ryanair", Telefono = "663225633"},
                    new Aerolinea { Nombre = "Iberia", Telefono = "699873226"},
                    new Aerolinea { Nombre = "Volotea", Telefono = "623445124"},
                };

                foreach (Aerolinea aerolinea in aerolineas)
                {
                    context.Aerolinea.AddOrUpdate(aerolinea);
                    context.SaveChanges();
                }

                List<Cargo> cargos = new List<Cargo>()
                {
                    new Cargo { Nombre = "Piloto", Sueldo = 3500},
                    new Cargo { Nombre = "Cocina", Sueldo = 1500},
                    new Cargo { Nombre = "Azafato", Sueldo = 900},
                    new Cargo { Nombre = "Jefe de Aeropuerto", Sueldo = 1500},
                    new Cargo { Nombre = "Recepcionista", Sueldo = 850},
                };

                foreach (Cargo cargo in cargos)
                {
                    context.Cargo.AddOrUpdate(cargo);
                    context.SaveChanges();
                }

                List<Empleado> empleados = new List<Empleado>()
                {
                    // Empleados Aerolíne 1:
                    new Empleado { Nombre = "Marcos", Apellidos = "Peña Martínez", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Érik", Apellidos = "Sanchez Díaz", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Alejandro", Apellidos = "Martínez Gutierrez", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(1), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Rubén", Apellidos = "Mas Moreno", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Lidia", Apellidos = "Roca Martínez", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Olivia", Apellidos = "Marti Gallego", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(3), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Nuria", Apellidos = "Sole Marín", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(0) },
                    new Empleado { Nombre = "Manuel", Apellidos = "Gil Blanco", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(0) },

                    // Empleados Aerolínea 2:
                    new Empleado { Nombre = "Claudia", Apellidos = "Caro Mora", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Nuria", Apellidos = "Lozano Martínez", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Alejandro", Apellidos = "Reyes Fernández", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(1), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Saúl", Apellidos = "Tomás Morales", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Víctor", Apellidos = "Gómez Ortega", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Aina", Apellidos = "Pascual Gil", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(3), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Candela", Apellidos = "Rovira Esteban", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea =  aerolineas.ElementAt(1) },
                    new Empleado { Nombre = "Saray", Apellidos = "Sánchez Iglesias", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea =  aerolineas.ElementAt(1) },

                    // Empleados Aerolínea 3:
                    new Empleado { Nombre = "Teresa", Apellidos = "Diaz Pastor", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Pol", Apellidos = "Puig Bravo", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Sara", Apellidos = "Bosch Crespo", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(1), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Lidia", Apellidos = "Domenech Cano", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Marina", Apellidos = "Carmona Esteban", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Isaac", Apellidos = "García Vega", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(3), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Gonzalo", Apellidos = "Navarro Pastor", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(2) },
                    new Empleado { Nombre = "Abril", Apellidos = "Muñoz González", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(2) },

                    // Empleados Aerolínea 4:
                    new Empleado { Nombre = "Omar", Apellidos = "Martí Ramírez", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Inés", Apellidos = "Carmona Gallardo", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Aroa", Apellidos = "Pons Gil", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(1), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Noelia", Apellidos = "Márquez Carmona", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Juan José", Apellidos = "Martí Ortiz", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Alex", Apellidos = "Ramos Soto", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(3), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Patricia", Apellidos = "Fuentes Martín", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(3) },
                    new Empleado { Nombre = "Anna", Apellidos = "Martínez Delgado", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(3) },

                    // Empleados Aerolínea 5:
                    new Empleado { Nombre = "Celia", Apellidos = "León Herrera", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Victora", Apellidos = "Vargas Garrido", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(0), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Irene", Apellidos = "Blanco Martínez", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(1), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Biel", Apellidos = "Reyes Garrido", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Ismael", Apellidos = "Castro Gutierrez", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(2), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Héctor", Apellidos = "Mas Cruz", Genero = "Masculino", Pais = "España", Cargo = cargos.ElementAt(3), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Sandra", Apellidos = "Aguilar Benitez", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(4) },
                    new Empleado { Nombre = "Leire", Apellidos = "Costa Navarro", Genero = "Femenino", Pais = "España", Cargo = cargos.ElementAt(4), Aerolinea = aerolineas.ElementAt(4) },
                };

                foreach (Empleado empleado in empleados)
                {
                    context.Empleado.AddOrUpdate(empleado);
                    context.SaveChanges();
                }

                List<Vuelo> vuelos = new List<Vuelo>()
                {
                    // Vuelos Aerolínea 1:
                    new Vuelo { Origen="Madrid", Destino="Barcelona", Salida="6:30", Llegada="9:30", Aerolinea = aerolineas.ElementAt(0) },
                    new Vuelo { Origen="Barcelona", Destino="Madrid", Salida="10:00", Llegada="13:00", Aerolinea = aerolineas.ElementAt(0) },
                    new Vuelo { Origen="Madrid", Destino="Las Palmas", Salida="7:30", Llegada="11:30", Aerolinea = aerolineas.ElementAt(0) },
                    new Vuelo { Origen="Las Palmas", Destino="Madrid", Salida="13:00", Llegada="17:00", Aerolinea = aerolineas.ElementAt(0) },
                    new Vuelo { Origen="Vigo", Destino="Madrid", Salida="8:30", Llegada="10:30", Aerolinea = aerolineas.ElementAt(0) },

                    // Vuelos Aerolínea 2:
                    new Vuelo { Origen="Madrid", Destino="Barcelona", Salida="7:00", Llegada="10:00", Aerolinea = aerolineas.ElementAt(1) },
                    new Vuelo { Origen="Barcelona", Destino="Madrid", Salida="3:00", Llegada="6:00", Aerolinea = aerolineas.ElementAt(1) },
                    new Vuelo { Origen="Vigo", Destino="Barcelona", Salida="4:00", Llegada="10:00", Aerolinea = aerolineas.ElementAt(1) },
                    new Vuelo { Origen="Barcelona", Destino="Vigo", Salida="17:00", Llegada="23:00", Aerolinea = aerolineas.ElementAt(1) },
                    new Vuelo { Origen="Barcelona", Destino="Ibiza", Salida="8:30", Llegada="10:30", Aerolinea = aerolineas.ElementAt(1) },
                    
                    // Vuelos Aerolínea 3:
                    new Vuelo { Origen="Madrid", Destino="Barcelona", Salida="10:00", Llegada="13:00", Aerolinea = aerolineas.ElementAt(2) },
                    new Vuelo { Origen="Barcelona", Destino="Madrid", Salida="15:00", Llegada="18:00", Aerolinea = aerolineas.ElementAt(2) },
                    new Vuelo { Origen="Tenerife", Destino="Madrid", Salida="7:30", Llegada="11:30", Aerolinea = aerolineas.ElementAt(2) },
                    new Vuelo { Origen="Madrid", Destino="Tenerife", Salida="9:00", Llegada="13:00", Aerolinea = aerolineas.ElementAt(2) },
                    new Vuelo { Origen="Málaga", Destino="Barcelona", Salida="17:30", Llegada="19:00", Aerolinea = aerolineas.ElementAt(2) },
                    
                    // Vuelos Aerolínea 4:
                    new Vuelo { Origen="Madrid", Destino="Barcelona", Salida="11:00", Llegada="14:00", Aerolinea = aerolineas.ElementAt(3) },
                    new Vuelo { Origen="Barcelona", Destino="Madrid", Salida="13:00", Llegada="16:00", Aerolinea = aerolineas.ElementAt(3) },
                    new Vuelo { Origen="Málaga", Destino="Barcelona", Salida="21:30", Llegada="23:00", Aerolinea = aerolineas.ElementAt(3) },
                    new Vuelo { Origen="Barcelona", Destino="Málaga", Salida="1:00", Llegada="2:30", Aerolinea = aerolineas.ElementAt(3) },
                    new Vuelo { Origen="Vigo", Destino="Málaga", Salida="5:00", Llegada="9:30", Aerolinea = aerolineas.ElementAt(3) },
                    
                    // Vuelos Aerolínea 5:
                    new Vuelo { Origen="Madrid", Destino="Barcelona", Salida="7:00", Llegada="10:00", Aerolinea = aerolineas.ElementAt(4) },
                    new Vuelo { Origen="Barcelona", Destino="Madrid", Salida="6:30", Llegada="9:30", Aerolinea = aerolineas.ElementAt(4) },
                    new Vuelo { Origen="Madrid", Destino="Vigo", Salida="8:30", Llegada="11:00", Aerolinea = aerolineas.ElementAt(4) },
                    new Vuelo { Origen="Madrid", Destino="Málaga", Salida="9:30", Llegada="11:30", Aerolinea = aerolineas.ElementAt(4) },
                    new Vuelo { Origen="Madrid", Destino="Ibiza", Salida="7:00", Llegada="11:00", Aerolinea = aerolineas.ElementAt(4) }
                };

                foreach (Vuelo vuelo in vuelos)
                {
                    context.Vuelo.AddOrUpdate(vuelo);
                    context.SaveChanges();
                }

                List<Usuario> usuarios = new List<Usuario>()
                {
                    new Usuario { Nombre = "admin", Password = "abc123." },
                    new Usuario { Nombre = "Ruben", Password = "hola" },
                    new Usuario { Nombre = "Ivan", Password = "hola2" },
                    new Usuario { Nombre = "Pepe", Password = "juju." },
                };
                
                foreach (Usuario usuario in usuarios)
                {
                    context.Usuario.AddOrUpdate(usuario);
                    context.SaveChanges();
                }

                base.Seed(context);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
