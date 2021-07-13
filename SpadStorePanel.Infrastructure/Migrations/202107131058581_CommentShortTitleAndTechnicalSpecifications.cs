namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentShortTitleAndTechnicalSpecifications : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Products", "ShortTitle");
            DropColumn("dbo.Products", "TechnicalSpecifications");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "TechnicalSpecifications", c => c.String());
            //AddColumn("dbo.Products", "ShortTitle", c => c.String(maxLength: 300));
        }
    }
}
