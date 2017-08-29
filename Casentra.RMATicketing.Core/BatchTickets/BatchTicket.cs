using Abp.Domain.Entities.Auditing;
using Casentra.RMATicketing.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BatchTickets
{
    [Table("BatchTickets")]
    public class BatchTicket: FullAuditedEntity
    {
        public string BatchTicketNo { get; set; }
        public TicketBoard TicketBoard { get; set; }
        public DateTime CreatedDate { get; set; } 
        public int CustomerId { get; set; }
        public DateTime? ClosedDate { get; set; } 
        public string IssueSummary { get; set; }
       

    }
}
