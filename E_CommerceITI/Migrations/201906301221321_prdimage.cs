namespace E_CommerceITI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prdimage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductImages", "imgSrc", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductImages", "imgSrc", c => c.Binary());
        }
    }
}
