namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accessories", "FrenchName", c => c.String());
            AddColumn("dbo.Accessories", "ChinesName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accessories", "ChinesName");
            DropColumn("dbo.Accessories", "FrenchName");
        }
    }
}
