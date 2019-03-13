namespace GestorAeropuerto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aerolineas",
                c => new
                    {
                        AerolineaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AerolineaId);
            
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        EmpleadoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Apellidos = c.String(nullable: false, maxLength: 50),
                        Genero = c.String(nullable: false),
                        Pais = c.String(nullable: false, maxLength: 50),
                        Aerolinea_AerolineaId = c.Int(),
                        Cargo_CargoId = c.Int(),
                    })
                .PrimaryKey(t => t.EmpleadoId)
                .ForeignKey("dbo.Aerolineas", t => t.Aerolinea_AerolineaId)
                .ForeignKey("dbo.Cargoes", t => t.Cargo_CargoId)
                .Index(t => t.Aerolinea_AerolineaId)
                .Index(t => t.Cargo_CargoId);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Sueldo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CargoId);
            
            CreateTable(
                "dbo.Vueloes",
                c => new
                    {
                        VueloId = c.Int(nullable: false, identity: true),
                        Origen = c.String(nullable: false),
                        Destino = c.String(nullable: false),
                        Llegada = c.DateTime(nullable: false),
                        Salida = c.DateTime(nullable: false),
                        Aerolinea_AerolineaId = c.Int(),
                    })
                .PrimaryKey(t => t.VueloId)
                .ForeignKey("dbo.Aerolineas", t => t.Aerolinea_AerolineaId)
                .Index(t => t.Aerolinea_AerolineaId);
            
            CreateTable(
                "dbo.Avions",
                c => new
                    {
                        AvionId = c.Int(nullable: false, identity: true),
                        Modelo = c.String(nullable: false),
                        Fabricante = c.String(nullable: false),
                        Capacidad = c.Int(nullable: false),
                        Vuelo_VueloId = c.Int(),
                    })
                .PrimaryKey(t => t.AvionId)
                .ForeignKey("dbo.Vueloes", t => t.Vuelo_VueloId)
                .Index(t => t.Vuelo_VueloId);
            
            CreateTable(
                "dbo.Billetes",
                c => new
                    {
                        PasajeroId = c.Int(nullable: false),
                        BilleteId = c.Int(nullable: false),
                        Precio = c.Single(nullable: false),
                        TipoAsiento = c.String(nullable: false),
                        NumAsiento = c.Int(nullable: false),
                        Vuelo_VueloId = c.Int(),
                    })
                .PrimaryKey(t => t.PasajeroId)
                .ForeignKey("dbo.Pasajeroes", t => t.PasajeroId)
                .ForeignKey("dbo.Vueloes", t => t.Vuelo_VueloId)
                .Index(t => t.PasajeroId)
                .Index(t => t.Vuelo_VueloId);
            
            CreateTable(
                "dbo.Pasajeroes",
                c => new
                    {
                        PasajeroId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        NumPasaporte = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PasajeroId);
            
            CreateTable(
                "dbo.Maletas",
                c => new
                    {
                        MaletaId = c.Int(nullable: false, identity: true),
                        Peso = c.Single(nullable: false),
                        Pasajero_PasajeroId = c.Int(),
                    })
                .PrimaryKey(t => t.MaletaId)
                .ForeignKey("dbo.Pasajeroes", t => t.Pasajero_PasajeroId)
                .Index(t => t.Pasajero_PasajeroId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vueloes", "Aerolinea_AerolineaId", "dbo.Aerolineas");
            DropForeignKey("dbo.Billetes", "Vuelo_VueloId", "dbo.Vueloes");
            DropForeignKey("dbo.Billetes", "PasajeroId", "dbo.Pasajeroes");
            DropForeignKey("dbo.Maletas", "Pasajero_PasajeroId", "dbo.Pasajeroes");
            DropForeignKey("dbo.Avions", "Vuelo_VueloId", "dbo.Vueloes");
            DropForeignKey("dbo.Empleadoes", "Cargo_CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.Empleadoes", "Aerolinea_AerolineaId", "dbo.Aerolineas");
            DropIndex("dbo.Maletas", new[] { "Pasajero_PasajeroId" });
            DropIndex("dbo.Billetes", new[] { "Vuelo_VueloId" });
            DropIndex("dbo.Billetes", new[] { "PasajeroId" });
            DropIndex("dbo.Avions", new[] { "Vuelo_VueloId" });
            DropIndex("dbo.Vueloes", new[] { "Aerolinea_AerolineaId" });
            DropIndex("dbo.Empleadoes", new[] { "Cargo_CargoId" });
            DropIndex("dbo.Empleadoes", new[] { "Aerolinea_AerolineaId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Maletas");
            DropTable("dbo.Pasajeroes");
            DropTable("dbo.Billetes");
            DropTable("dbo.Avions");
            DropTable("dbo.Vueloes");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.Aerolineas");
        }
    }
}
