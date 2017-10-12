namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        BikeId = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bicycles", t => t.BikeId)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.BikeId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Bicycles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        BrandId = c.Int(),
                        TypeId = c.Int(),
                        WheelSizeId = c.Int(),
                        FrameSizeId = c.Int(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Frames", t => t.FrameSizeId)
                .ForeignKey("dbo.BicycleTypes", t => t.TypeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Wheels", t => t.WheelSizeId)
                .Index(t => t.BrandId)
                .Index(t => t.TypeId)
                .Index(t => t.WheelSizeId)
                .Index(t => t.FrameSizeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Frames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BicycleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PWord = c.String(),
                        Salt = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Zipcode = c.String(),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wheels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Advertisements", "UserID", "dbo.Users");
            DropForeignKey("dbo.Bookings", "RentUserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "AdvertismentId", "dbo.Advertisements");
            DropForeignKey("dbo.Advertisements", "BikeId", "dbo.Bicycles");
            DropForeignKey("dbo.Bicycles", "WheelSizeId", "dbo.Wheels");
            DropForeignKey("dbo.Bicycles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bicycles", "TypeId", "dbo.BicycleTypes");
            DropForeignKey("dbo.Bicycles", "FrameSizeId", "dbo.Frames");
            DropForeignKey("dbo.Bicycles", "BrandId", "dbo.Brands");
            DropIndex("dbo.Bookings", new[] { "AdvertismentId" });
            DropIndex("dbo.Bookings", new[] { "RentUserId" });
            DropIndex("dbo.Bicycles", new[] { "UserId" });
            DropIndex("dbo.Bicycles", new[] { "FrameSizeId" });
            DropIndex("dbo.Bicycles", new[] { "WheelSizeId" });
            DropIndex("dbo.Bicycles", new[] { "TypeId" });
            DropIndex("dbo.Bicycles", new[] { "BrandId" });
            DropIndex("dbo.Advertisements", new[] { "UserID" });
            DropIndex("dbo.Advertisements", new[] { "BikeId" });
            DropTable("dbo.Bookings");
            DropTable("dbo.Wheels");
            DropTable("dbo.Users");
            DropTable("dbo.BicycleTypes");
            DropTable("dbo.Frames");
            DropTable("dbo.Brands");
            DropTable("dbo.Bicycles");
            DropTable("dbo.Advertisements");
        }
    }
}
