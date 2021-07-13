namespace SpadStorePanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentInvoiceModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "DiscountCodeId", "dbo.DiscountCodes");
            DropForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.Invoices", new[] { "GeoDivisionId" });
            DropIndex("dbo.Invoices", new[] { "DiscountCodeId" });
            DropIndex("dbo.EPayments", new[] { "InvoiceId" });
            DropTable("dbo.Invoices");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EPayments", "InvoiceId");
            CreateIndex("dbo.Invoices", "DiscountCodeId");
            CreateIndex("dbo.Invoices", "GeoDivisionId");
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.InvoiceItems", "InvoiceId");
            AddForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices", "Id");
            AddForeignKey("dbo.Invoices", "GeoDivisionId", "dbo.GeoDivisions", "Id");
            AddForeignKey("dbo.EPayments", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "DiscountCodeId", "dbo.DiscountCodes", "Id");
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
