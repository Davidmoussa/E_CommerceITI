namespace E_CommerceITI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flagtodeleteproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "deleted");
        }
    }
}
