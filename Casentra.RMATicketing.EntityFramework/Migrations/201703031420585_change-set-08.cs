namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "TicketNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "TicketNo");
        }
    }
}
