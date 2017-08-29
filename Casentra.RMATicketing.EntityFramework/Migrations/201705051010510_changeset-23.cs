namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchItems", "TrackingNumber", c => c.String());
            DropColumn("dbo.BatchTickets", "TicketStatus");
            DropColumn("dbo.BatchTickets", "TicketPriority");
            DropColumn("dbo.BatchTickets", "TrackingNumber");
            DropColumn("dbo.BatchTickets", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BatchTickets", "Note", c => c.String());
            AddColumn("dbo.BatchTickets", "TrackingNumber", c => c.String());
            AddColumn("dbo.BatchTickets", "TicketPriority", c => c.Int(nullable: false));
            AddColumn("dbo.BatchTickets", "TicketStatus", c => c.Int(nullable: false));
            DropColumn("dbo.BatchItems", "TrackingNumber");
        }
    }
}
