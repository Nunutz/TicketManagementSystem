namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchItems", "TicketBoard", c => c.Int(nullable: false));
            AddColumn("dbo.BatchItems", "ClosedDate", c => c.DateTime());
            AddColumn("dbo.BatchTickets", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatchTickets", "Note");
            DropColumn("dbo.BatchItems", "ClosedDate");
            DropColumn("dbo.BatchItems", "TicketBoard");
        }
    }
}
