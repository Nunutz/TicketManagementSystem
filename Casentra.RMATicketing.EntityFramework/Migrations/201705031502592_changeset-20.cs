namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchItems", "TicketStatus", c => c.Int(nullable: false));
            AddColumn("dbo.BatchTickets", "IssueSummary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatchTickets", "IssueSummary");
            DropColumn("dbo.BatchItems", "TicketStatus");
        }
    }
}
