namespace CurrencyChart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyId = c.Int(nullable: false),
                        SallerId = c.Int(nullable: false),
                        BuyerId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.Member_Id)
                .Index(t => t.CurrencyId)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Member_Id", "dbo.Member");
            DropForeignKey("dbo.Transaction", "CurrencyId", "dbo.Currency");
            DropIndex("dbo.Transaction", new[] { "Member_Id" });
            DropIndex("dbo.Transaction", new[] { "CurrencyId" });
            DropTable("dbo.Member");
            DropTable("dbo.Transaction");
            DropTable("dbo.Currency");
        }
    }
}
