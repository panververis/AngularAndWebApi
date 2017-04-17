namespace AngularAndWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AreaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.AreaID)
                .Index(t => t.AreaID);
            
            CreateTable(
                "dbo.Dealers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regions", t => t.RegionID)
                .Index(t => t.RegionID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        SaleValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VehicleID = c.Int(nullable: false),
                        StaffID = c.Int(nullable: false),
                        DealerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dealers", t => t.DealerID)
                .ForeignKey("dbo.Staffs", t => t.StaffID)
                .ForeignKey("dbo.Vehicles", t => t.VehicleID)
                .Index(t => t.VehicleID)
                .Index(t => t.StaffID)
                .Index(t => t.DealerID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        JobType = c.Int(nullable: false),
                        DealerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dealers", t => t.DealerID)
                .Index(t => t.DealerID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        MakeYear = c.Short(nullable: false),
                        ChassisNumber = c.String(),
                        EngineCapacity = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "VehicleID", "dbo.Vehicles");
            DropForeignKey("dbo.Sales", "StaffID", "dbo.Staffs");
            DropForeignKey("dbo.Staffs", "DealerID", "dbo.Dealers");
            DropForeignKey("dbo.Sales", "DealerID", "dbo.Dealers");
            DropForeignKey("dbo.Dealers", "RegionID", "dbo.Regions");
            DropForeignKey("dbo.Regions", "AreaID", "dbo.Areas");
            DropIndex("dbo.Staffs", new[] { "DealerID" });
            DropIndex("dbo.Sales", new[] { "DealerID" });
            DropIndex("dbo.Sales", new[] { "StaffID" });
            DropIndex("dbo.Sales", new[] { "VehicleID" });
            DropIndex("dbo.Dealers", new[] { "RegionID" });
            DropIndex("dbo.Regions", new[] { "AreaID" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Staffs");
            DropTable("dbo.Sales");
            DropTable("dbo.Dealers");
            DropTable("dbo.Regions");
            DropTable("dbo.Areas");
        }
    }
}
