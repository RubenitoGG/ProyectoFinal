namespace GestorAeropuerto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hora : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vuelos", "Llegada", c => c.String(nullable: false));
            AlterColumn("dbo.Vuelos", "Salida", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vuelos", "Salida", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vuelos", "Llegada", c => c.DateTime(nullable: false));
        }
    }
}
