namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsTrading", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsTrading");
        }
    }
}
