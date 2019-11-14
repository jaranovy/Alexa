namespace Alexa.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GroupsToSuppliers",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.SupplierID })
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupsToSuppliers", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.GroupsToSuppliers", "GroupID", "dbo.Groups");
            DropIndex("dbo.GroupsToSuppliers", new[] { "SupplierID" });
            DropIndex("dbo.GroupsToSuppliers", new[] { "GroupID" });
            DropTable("dbo.GroupsToSuppliers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Groups");
        }
    }
}
