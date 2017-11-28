namespace Serve_Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeptemberMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.TemperatureSalons",
                    c => new
                    {
                        TemperatureID = c.Int(nullable: false, identity: true),
                        TemperatureValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TemperatureID);
            CreateTable(
                "dbo.AirPressures",
                c => new
                    {
                        AirPressureID = c.Int(nullable: false, identity: true),
                        AirPressureValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.AirPressureID);
            
            CreateTable(
                "dbo.Humidities",
                c => new
                    {
                        HumidityID = c.Int(nullable: false, identity: true),
                        HumidityValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.HumidityID);
            
            CreateTable(
                "dbo.TemperatureDailyRooms",
                c => new
                    {
                        TemperatureID = c.Int(nullable: false, identity: true),
                        TemperatureValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TemperatureID);
            
            CreateTable(
                "dbo.TemperatureFirstRooms",
                c => new
                    {
                        TemperatureID = c.Int(nullable: false, identity: true),
                        TemperatureValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TemperatureID);
            
            CreateTable(
                "dbo.TemperatureOutsides",
                c => new
                    {
                        TemperatureID = c.Int(nullable: false, identity: true),
                        TemperatureValue = c.Double(nullable: false),
                        MeasureTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TemperatureID);
            
            
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TemperatureSalons");
            DropTable("dbo.TemperatureOutsides");
            DropTable("dbo.TemperatureFirstRooms");
            DropTable("dbo.TemperatureDailyRooms");
            DropTable("dbo.Humidities");
            DropTable("dbo.AirPressures");
        }
    }
}
