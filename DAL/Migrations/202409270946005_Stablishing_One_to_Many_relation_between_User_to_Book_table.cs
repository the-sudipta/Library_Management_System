namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stablishing_One_to_Many_relation_between_User_to_Book_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "User_ID");
            AddForeignKey("dbo.Books", "User_ID", "dbo.Users", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "User_ID", "dbo.Users");
            DropIndex("dbo.Books", new[] { "User_ID" });
            DropColumn("dbo.Books", "User_ID");
        }
    }
}
