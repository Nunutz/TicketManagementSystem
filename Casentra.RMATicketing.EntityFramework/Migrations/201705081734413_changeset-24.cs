namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class changeset24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IMEINumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IMEINo = c.String(),
                        Model = c.String(),
                        PurchasedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_IMEINumber_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SpareParts", "DeliveredDate", c => c.DateTime());
            AlterColumn("dbo.SpareParts", "DeliveredUserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpareParts", "DeliveredUserId", c => c.Int(nullable: false));
            DropColumn("dbo.SpareParts", "DeliveredDate");
            DropTable("dbo.IMEINumbers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_IMEINumber_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
