namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        RentUserId = c.Int(nullable: false),
                        AdvertismentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.AdvertismentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.RentUserId, cascadeDelete: true)
                .Index(t => t.RentUserId)
                .Index(t => t.AdvertismentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "RentUserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "AdvertismentId", "dbo.Advertisements");
            DropIndex("dbo.Bookings", new[] { "AdvertismentId" });
            DropIndex("dbo.Bookings", new[] { "RentUserId" });
            DropTable("dbo.Bookings");
        }
    }
}
