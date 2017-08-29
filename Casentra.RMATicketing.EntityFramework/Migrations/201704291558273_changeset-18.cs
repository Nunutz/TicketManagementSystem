namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BatchItems", "PurchasedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BatchItems", "PhoneProblemId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BatchItems", "PhoneProblemId");
            DropColumn("dbo.BatchItems", "PurchasedDate");
        }
    }
}
