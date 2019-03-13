namespace GestorAeropuerto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombres : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Empleadoes", newName: "Empleados");
            RenameTable(name: "dbo.Cargoes", newName: "Cargos");
            RenameTable(name: "dbo.Vueloes", newName: "Vuelos");
            RenameTable(name: "dbo.Avions", newName: "Aviones");
            RenameTable(name: "dbo.Pasajeroes", newName: "Pasajeros");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Pasajeros", newName: "Pasajeroes");
            RenameTable(name: "dbo.Aviones", newName: "Avions");
            RenameTable(name: "dbo.Vuelos", newName: "Vueloes");
            RenameTable(name: "dbo.Cargos", newName: "Cargoes");
            RenameTable(name: "dbo.Empleados", newName: "Empleadoes");
        }
    }
}
