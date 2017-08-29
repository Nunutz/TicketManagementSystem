namespace Casentra.RMATicketing.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class changeset17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BatchItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchTicketId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        BoughtAt = c.String(),
                        Brand = c.String(),
                        Product = c.String(),
                        ProductColor = c.String(),
                        Capacity = c.String(),
                        Accessery = c.String(),
                        IMEINumber = c.String(),
                        Password = c.String(),
                        PhoneCondition = c.String(),
                        IcloudAddress = c.String(),
                        IcloudPassword = c.String(),
                        Accessories = c.String(),
                        PhoneProblem = c.String(),
                        PhoneProblemsInFrench = c.String(),
                        PhoneProblemsInChinese = c.String(),
                        IssueDetail = c.String(),
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
                    { "DynamicFilter_BatchItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BatchTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchTicketNo = c.String(),
                        TicketStatus = c.Int(nullable: false),
                        TicketBoard = c.Int(nullable: false),
                        TicketPriority = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ClosedDate = c.DateTime(),
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
                    { "DynamicFilter_BatchTicket_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Status = c.String(),
                        TicketId = c.Int(nullable: false),
                        BatchTicketId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
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
                    { "DynamicFilter_Note_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpareParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpareName = c.String(),
                        Model = c.String(),
                        Quantity = c.Int(nullable: false),
                        BatchTicketId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        DeliveredUserId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
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
                    { "DynamicFilter_SparePart_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpareParts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SparePart_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Notes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Note_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BatchTickets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchTicket_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.BatchItems",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchItem_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
