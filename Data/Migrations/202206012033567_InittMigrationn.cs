namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InittMigrationn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApartmentType = c.String(nullable: false, maxLength: 50),
                        Area = c.Double(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        TownId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: true)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownName = c.String(nullable: false, maxLength: 50),
                        Population = c.Int(nullable: false),
                        Area = c.Double(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Apartments", "TownId", "dbo.Towns");
            DropIndex("dbo.Offers", new[] { "ApartmentId" });
            DropIndex("dbo.Apartments", new[] { "TownId" });
            DropTable("dbo.Offers");
            DropTable("dbo.Towns");
            DropTable("dbo.Apartments");
        }
    }
}
