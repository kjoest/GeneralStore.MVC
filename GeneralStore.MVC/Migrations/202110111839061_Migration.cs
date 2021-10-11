namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Transactions", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.Transactions", name: "Product_ProductId", newName: "ProductId");
            AlterColumn("dbo.Transactions", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "ProductId");
            AddForeignKey("dbo.Transactions", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ProductId", "dbo.Products");
            DropIndex("dbo.Transactions", new[] { "ProductId" });
            AlterColumn("dbo.Transactions", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Transactions", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.Transactions", "Product_ProductId");
            AddForeignKey("dbo.Transactions", "Product_ProductId", "dbo.Products", "ProductId");
        }
    }
}
