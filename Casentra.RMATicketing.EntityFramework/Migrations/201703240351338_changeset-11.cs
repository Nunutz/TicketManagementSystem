namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "PhoneProblemsInFrench", c => c.String());
            AddColumn("dbo.Tickets", "PhoneProblemsInChinese", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "PhoneProblemsInChinese");
            DropColumn("dbo.Tickets", "PhoneProblemsInFrench");
        }
    }
}
