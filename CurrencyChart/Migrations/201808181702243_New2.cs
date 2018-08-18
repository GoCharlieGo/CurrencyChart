namespace CurrencyChart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaction", "Member_Id", "dbo.Member");
            DropIndex("dbo.Transaction", new[] { "Member_Id" });
            DropColumn("dbo.Transaction", "Member_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "Member_Id", c => c.Int());
            CreateIndex("dbo.Transaction", "Member_Id");
            AddForeignKey("dbo.Transaction", "Member_Id", "dbo.Member", "Id");
        }
    }
}
