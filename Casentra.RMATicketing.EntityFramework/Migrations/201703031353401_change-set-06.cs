namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "BrandId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "BrandId");
        }
    }
}
