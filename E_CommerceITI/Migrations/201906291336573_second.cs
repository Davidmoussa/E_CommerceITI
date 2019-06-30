namespace E_CommerceITI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" }, "dbo.Discounts");
            DropIndex("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" });
            DropColumn("dbo.Discounts", "Discount_productId");
            DropColumn("dbo.Discounts", "Discount_StartDate");
            DropColumn("dbo.Discounts", "Discount_EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "Discount_EndDate", c => c.DateTime());
            AddColumn("dbo.Discounts", "Discount_StartDate", c => c.DateTime());
            AddColumn("dbo.Discounts", "Discount_productId", c => c.Int());
            CreateIndex("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" });
            AddForeignKey("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" }, "dbo.Discounts", new[] { "productId", "StartDate", "EndDate" });
        }
    }
}
