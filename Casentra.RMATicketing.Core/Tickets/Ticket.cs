using Abp.Domain.Entities.Auditing;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.Capacities;
using Casentra.RMATicketing.Colors;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.PhoneConditions;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Tickets
{
    [Table("Tickets")]
    public class Ticket:FullAuditedEntity
    {                   
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime PurchasedDate { get; set; }
        public AdminTicketStatus TicketStatus { get; set; }
        public TicketBoard TicketBoard { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public DateTime? ClosedDate { get; set; } // Ticket age: up to ClosedDate OR current date
                     
        public int CustomerId { get; set; }
        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int CapacityId { get; set; }
        public int PhoneConditionId { get; set; }
        public int PhoneProblemId { get; set; }
        public int BoughtAtId { get; set; }
        public  int AccessoryId { get; set; }
        public string IMEINumber { get; set; }
        public string Password { get; set; }

        public string IcloudAddress { get; set; }
        public string IcloudPassword { get; set; }
        public string Accessories { get; set; }
        public string PhoneProblems { get; set; }
        public string PhoneProblemsInFrench { get; set; }
        public string PhoneProblemsInChinese { get; set; }
        public DateTime CreatedDate { get; set; }

        public string TicketNo{ get; set;}
        public string TrackingNumber { get; set; }

        
    }

}
