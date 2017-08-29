namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchItems", "TicketPriority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatchItems", "TicketPriority");
        }
    }
}
