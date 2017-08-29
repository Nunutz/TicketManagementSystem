using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Spares
{
    [Table("SpareParts")]
    public class SparePart : FullAuditedEntity
    {
        public string SpareName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int BatchTicketId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelivered { get; set; }
        public int? DeliveredUserId { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Note { get; set; }

    }
}
