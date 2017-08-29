using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Notes
{
    [Table("Notes")]
    public class Note : FullAuditedEntity
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public int TicketId { get; set; }
        public int BatchTicketId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
