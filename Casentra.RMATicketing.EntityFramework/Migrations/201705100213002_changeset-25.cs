namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeset25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpareParts", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpareParts", "Note");
        }
    }
}
