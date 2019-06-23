namespace E_CommerceITI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "SellerId", newName: "CustomerId");
            RenameIndex(table: "dbo.Customers", name: "IX_SellerId", newName: "IX_CustomerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_CustomerId", newName: "IX_SellerId");
            RenameColumn(table: "dbo.Customers", name: "CustomerId", newName: "SellerId");
        }
    }
}
