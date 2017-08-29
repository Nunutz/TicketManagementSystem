using Abp.Domain.Entities.Auditing;
using Casentra.RMATicketing.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BatchItems
{
    [Table("BatchItems")]
    public class BatchItem : FullAuditedEntity
    {
        public int BatchTicketId { get; set; }
        public int CustomerId { get; set; }
        public DateTime PurchasedDate { get; set; }
        public string BoughtAt { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }
        public string ProductColor { get; set; }
        public string Capacity { get; set; }
        public string Accessery { get; set; }
        public string IMEINumber { get; set; }
        public string Password { get; set; }
        public string PhoneCondition { get; set; }
        public string IcloudAddress { get; set; }
        public string IcloudPassword { get; set; }
        public string Accessories { get; set; }
        public string PhoneProblem { get; set; }
        public string PhoneProblemsInFrench { get; set; }
        public string PhoneProblemsInChinese { get; set; }
        public string IssueDetail { get; set; }
        public int PhoneProblemId { get; set; }
        public AdminTicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public TicketBoard TicketBoard { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string TrackingNumber { get; set; }
    }
}
