namespace CurrencyChart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
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
                .ForeignKey("dbo.Member", t => t.Member_Id)
                .ForeignKey("dbo.Member", t => t.BuyerId, cascadeDelete: false)
                .ForeignKey("dbo.Currency", t => t.CurrencyId, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.SallerId, cascadeDelete: false)
                .Index(t => t.CurrencyId)
                .Index(t => t.SallerId)
                .Index(t => t.BuyerId)
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
            DropForeignKey("dbo.Transaction", "SallerId", "dbo.Member");
            DropForeignKey("dbo.Transaction", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Transaction", "BuyerId", "dbo.Member");
            DropForeignKey("dbo.Transaction", "Member_Id", "dbo.Member");
            DropIndex("dbo.Transaction", new[] { "Member_Id" });
            DropIndex("dbo.Transaction", new[] { "BuyerId" });
            DropIndex("dbo.Transaction", new[] { "SallerId" });
            DropIndex("dbo.Transaction", new[] { "CurrencyId" });
            DropTable("dbo.Member");
            DropTable("dbo.Transaction");
            DropTable("dbo.Currency");
        }
    }
}
