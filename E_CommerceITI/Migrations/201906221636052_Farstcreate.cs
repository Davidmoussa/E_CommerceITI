namespace E_CommerceITI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Farstcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddToWishLists",
                c => new
                    {
                        ProducId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Block = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProducId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProducId, cascadeDelete: true)
                .Index(t => t.ProducId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        SellerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SellerId)
                .ForeignKey("dbo.AspNetUsers", t => t.SellerId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Addres_City = c.String(),
                        Addres_Country = c.String(),
                        Addres_Street = c.String(),
                        Addres_blockNum = c.String(),
                        Addres_apartmentNum = c.String(),
                        Addres_others = c.String(),
                        UserId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BillItems",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        color = c.String(),
                        prodId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.prodId, cascadeDelete: true)
                .Index(t => t.prodId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Authorized = c.Boolean(nullable: false),
                        AdminAuthId = c.String(maxLength: 128),
                        categoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Admins", t => t.AdminAuthId)
                .ForeignKey("dbo.Categories", t => t.categoryId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerId)
                .Index(t => t.SellerId)
                .Index(t => t.AdminAuthId)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Block = c.Boolean(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Address_City = c.String(),
                        Address_Country = c.String(),
                        Address_Street = c.String(),
                        Address_blockNum = c.String(),
                        Address_apartmentNum = c.String(),
                        Address_others = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        SellerId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SellerId)
                .ForeignKey("dbo.AspNetUsers", t => t.SellerId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        productId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        sellerId = c.String(maxLength: 128),
                        percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount_productId = c.Int(),
                        Discount_StartDate = c.DateTime(),
                        Discount_EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.productId, t.StartDate, t.EndDate })
                .ForeignKey("dbo.Discounts", t => new { t.Discount_productId, t.Discount_StartDate, t.Discount_EndDate })
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.sellerId)
                .Index(t => t.productId)
                .Index(t => t.sellerId)
                .Index(t => new { t.Discount_productId, t.Discount_StartDate, t.Discount_EndDate });
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        Description = c.String(),
                        image = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        imgSrc = c.Binary(),
                        productId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ProducId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProducId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProducId, cascadeDelete: true)
                .Index(t => t.ProducId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProductAmounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProducId = c.Int(nullable: false),
                        SellerId = c.String(maxLength: 128),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProducId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerId)
                .Index(t => t.ProducId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductAmounts", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.ProductAmounts", "ProducId", "dbo.Products");
            DropForeignKey("dbo.AddToWishLists", "ProducId", "dbo.Products");
            DropForeignKey("dbo.AddToWishLists", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "SellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bills", "UserId", "dbo.Customers");
            DropForeignKey("dbo.BillItems", "prodId", "dbo.Products");
            DropForeignKey("dbo.Products", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Rates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Rates", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Reviews", "ProducId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProductImages", "productId", "dbo.Products");
            DropForeignKey("dbo.Products", "categoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "AdminAuthId", "dbo.Admins");
            DropForeignKey("dbo.Admins", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sellers", "SellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Discounts", "sellerId", "dbo.Sellers");
            DropForeignKey("dbo.Discounts", "productId", "dbo.Products");
            DropForeignKey("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" }, "dbo.Discounts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BillItems", "BillId", "dbo.Bills");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductAmounts", new[] { "SellerId" });
            DropIndex("dbo.ProductAmounts", new[] { "ProducId" });
            DropIndex("dbo.Rates", new[] { "CustomerId" });
            DropIndex("dbo.Rates", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.Reviews", new[] { "ProducId" });
            DropIndex("dbo.ProductImages", new[] { "productId" });
            DropIndex("dbo.Discounts", new[] { "Discount_productId", "Discount_StartDate", "Discount_EndDate" });
            DropIndex("dbo.Discounts", new[] { "sellerId" });
            DropIndex("dbo.Discounts", new[] { "productId" });
            DropIndex("dbo.Sellers", new[] { "SellerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Admins", new[] { "AdminId" });
            DropIndex("dbo.Products", new[] { "categoryId" });
            DropIndex("dbo.Products", new[] { "AdminAuthId" });
            DropIndex("dbo.Products", new[] { "SellerId" });
            DropIndex("dbo.BillItems", new[] { "BillId" });
            DropIndex("dbo.BillItems", new[] { "prodId" });
            DropIndex("dbo.Bills", new[] { "UserId" });
            DropIndex("dbo.Customers", new[] { "SellerId" });
            DropIndex("dbo.AddToWishLists", new[] { "CustomerId" });
            DropIndex("dbo.AddToWishLists", new[] { "ProducId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductAmounts");
            DropTable("dbo.Rates");
            DropTable("dbo.Reviews");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Categories");
            DropTable("dbo.Discounts");
            DropTable("dbo.Sellers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Admins");
            DropTable("dbo.Products");
            DropTable("dbo.BillItems");
            DropTable("dbo.Bills");
            DropTable("dbo.Customers");
            DropTable("dbo.AddToWishLists");
        }
    }
}
