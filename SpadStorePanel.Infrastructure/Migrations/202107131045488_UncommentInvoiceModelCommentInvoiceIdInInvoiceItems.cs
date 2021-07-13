namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UncommentInvoiceModelCommentInvoiceIdInInvoiceItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountAmount = c.Long(nullable: false),
                        TotalPriceBeforeDiscount = c.Long(nullable: false),
                        TotalPrice = c.Long(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        CustomerId = c.Int(),
                        CustomerName = c.String(maxLength: 500),
                        GeoDivisionId = c.Int(),
                        Address = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 50),
                        IsPayed = c.Boolean(nullable: false),
                        CompanyName = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        PostalCode = c.String(maxLength: 50),
                        Email = c.String(),
                        Description = c.String(),
                        DiscountCodeId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.DiscountCodes", t => t.DiscountCodeId)
                .ForeignKey("dbo.GeoDivisions", t => t.GeoDivisionId)
                .Index(t => t.CustomerId)
                .Index(t => t.GeoDivisionId)
                .Index(t => t.DiscountCodeId);
            
            AddColumn("dbo.InvoiceItems", "Invoice_Id", c => c.Int());
            CreateIndex("dbo.InvoiceItems", "Invoice_Id");
            CreateIndex("dbo.EPayments", "InvoiceId");
            AddForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "Invoice_Id", "dbo.Invoices", "Id");
            DropColumn("dbo.InvoiceItems", "InvoiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceItems", "InvoiceId", c => c.Int());
            DropForeignKey("dbo.InvoiceItems", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "DiscountCodeId", "dbo.DiscountCodes");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.EPayments", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "DiscountCodeId" });
            DropIndex("dbo.Invoices", new[] { "GeoDivisionId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_Id" });
            DropColumn("dbo.InvoiceItems", "Invoice_Id");
            DropTable("dbo.Invoices");
        }
    }
}
