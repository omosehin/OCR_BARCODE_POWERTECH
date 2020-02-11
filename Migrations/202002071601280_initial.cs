namespace Manual_Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeterReadings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MeterNumber = c.String(),
                        ImageBase64 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MeterReadings");
        }
    }
}
