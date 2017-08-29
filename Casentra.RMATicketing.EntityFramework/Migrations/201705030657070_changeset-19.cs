namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchTickets", "TrackingNumber", c => c.String());
            AddColumn("dbo.Tickets", "TrackingNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "TrackingNumber");
            DropColumn("dbo.BatchTickets", "TrackingNumber");
        }
    }
}
