namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnCommentShortTitleAndTechnicalSpecification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortTitle", c => c.String(maxLength: 300));
            AddColumn("dbo.Products", "TechnicalSpecifications", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "TechnicalSpecifications");
            DropColumn("dbo.Products", "ShortTitle");
        }
    }
}
