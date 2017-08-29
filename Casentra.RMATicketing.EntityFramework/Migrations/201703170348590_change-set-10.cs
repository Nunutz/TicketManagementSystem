namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhoneProblems", "FrenchName", c => c.String());
            AddColumn("dbo.PhoneProblems", "ChinesName", c => c.String());
            DropColumn("dbo.Accessories", "FrenchName");
            DropColumn("dbo.Accessories", "ChinesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accessories", "ChinesName", c => c.String());
            AddColumn("dbo.Accessories", "FrenchName", c => c.String());
            DropColumn("dbo.PhoneProblems", "ChinesName");
            DropColumn("dbo.PhoneProblems", "FrenchName");
        }
    }
}
