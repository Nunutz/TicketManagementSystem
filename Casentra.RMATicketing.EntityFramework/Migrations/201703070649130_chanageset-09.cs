namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chanageset09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "PhoneProblems", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "PhoneProblems");
        }
    }
}
