namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnCommentInvoiceIdInInvoiceItems : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.InvoiceItems", name: "Invoice_Id", newName: "InvoiceId");
            RenameIndex(table: "dbo.InvoiceItems", name: "IX_Invoice_Id", newName: "IX_InvoiceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.InvoiceItems", name: "IX_InvoiceId", newName: "IX_Invoice_Id");
            RenameColumn(table: "dbo.InvoiceItems", name: "InvoiceId", newName: "Invoice_Id");
        }
    }
}
