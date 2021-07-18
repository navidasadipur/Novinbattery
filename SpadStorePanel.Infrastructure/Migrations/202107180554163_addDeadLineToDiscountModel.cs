namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeadLineToDiscountModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "DeadLine", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "DeadLine");
        }
    }
}
