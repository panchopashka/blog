namespace Blog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "TitleImage", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "TitleImage");
        }
    }
}
