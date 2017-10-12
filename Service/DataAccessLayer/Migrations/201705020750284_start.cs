namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bicycles", "UserId", "dbo.Users");
            DropIndex("dbo.Advertisements", new[] { "UserId" });
            DropIndex("dbo.Bicycles", new[] { "UserId" });
            RenameColumn(table: "dbo.Advertisements", name: "Bike_ID", newName: "BikeId");
            RenameColumn(table: "dbo.Bicycles", name: "Brand_Id", newName: "BrandId");
            RenameColumn(table: "dbo.Bicycles", name: "FrameSize_Id", newName: "FrameSizeId");
            RenameColumn(table: "dbo.Bicycles", name: "Type_Id", newName: "TypeId");
            RenameColumn(table: "dbo.Bicycles", name: "WheelSize_Id", newName: "WheelSizeId");
            RenameIndex(table: "dbo.Advertisements", name: "IX_Bike_ID", newName: "IX_BikeId");
            RenameIndex(table: "dbo.Bicycles", name: "IX_Brand_Id", newName: "IX_BrandId");
            RenameIndex(table: "dbo.Bicycles", name: "IX_Type_Id", newName: "IX_TypeId");
            RenameIndex(table: "dbo.Bicycles", name: "IX_WheelSize_Id", newName: "IX_WheelSizeId");
            RenameIndex(table: "dbo.Bicycles", name: "IX_FrameSize_Id", newName: "IX_FrameSizeId");
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
            
            AddColumn("dbo.Advertisements", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Advertisements", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Advertisements", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bicycles", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Advertisements", "UserID");
            CreateIndex("dbo.Bicycles", "UserId");
            AddForeignKey("dbo.Bicycles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Advertisements", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "Status", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Bicycles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "RentUserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "AdvertismentId", "dbo.Advertisements");
            DropIndex("dbo.Bookings", new[] { "AdvertismentId" });
            DropIndex("dbo.Bookings", new[] { "RentUserId" });
            DropIndex("dbo.Bicycles", new[] { "UserId" });
            DropIndex("dbo.Advertisements", new[] { "UserID" });
            AlterColumn("dbo.Bicycles", "UserId", c => c.Int());
            AlterColumn("dbo.Advertisements", "EndDate", c => c.String());
            AlterColumn("dbo.Advertisements", "Description", c => c.String());
            DropColumn("dbo.Advertisements", "StartDate");
            DropTable("dbo.Bookings");
            RenameIndex(table: "dbo.Bicycles", name: "IX_FrameSizeId", newName: "IX_FrameSize_Id");
            RenameIndex(table: "dbo.Bicycles", name: "IX_WheelSizeId", newName: "IX_WheelSize_Id");
            RenameIndex(table: "dbo.Bicycles", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameIndex(table: "dbo.Bicycles", name: "IX_BrandId", newName: "IX_Brand_Id");
            RenameIndex(table: "dbo.Advertisements", name: "IX_BikeId", newName: "IX_Bike_ID");
            RenameColumn(table: "dbo.Bicycles", name: "WheelSizeId", newName: "WheelSize_Id");
            RenameColumn(table: "dbo.Bicycles", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Bicycles", name: "FrameSizeId", newName: "FrameSize_Id");
            RenameColumn(table: "dbo.Bicycles", name: "BrandId", newName: "Brand_Id");
            RenameColumn(table: "dbo.Advertisements", name: "BikeId", newName: "Bike_ID");
            CreateIndex("dbo.Bicycles", "UserId");
            CreateIndex("dbo.Advertisements", "UserId");
            AddForeignKey("dbo.Bicycles", "UserId", "dbo.Users", "Id");
        }
    }
}
