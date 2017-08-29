namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessories", "FrenchName", c => c.String());
            AddColumn("dbo.BoughtAts", "FrenchName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoughtAts", "FrenchName");
            DropColumn("dbo.Accessories", "FrenchName");
        }
    }
}
