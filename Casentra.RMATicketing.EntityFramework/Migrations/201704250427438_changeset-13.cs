namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsProfessional", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "IsVip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "IsVip", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customers", "IsProfessional");
        }
    }
}
