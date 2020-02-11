namespace Manual_Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeImagebase64ToEnergyConsupmtion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeterReadings", "EnergyConsumption", c => c.String());
            DropColumn("dbo.MeterReadings", "WebId");
            DropColumn("dbo.MeterReadings", "ImageBase64");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MeterReadings", "ImageBase64", c => c.String());
            AddColumn("dbo.MeterReadings", "WebId", c => c.String());
            DropColumn("dbo.MeterReadings", "EnergyConsumption");
        }
    }
}
