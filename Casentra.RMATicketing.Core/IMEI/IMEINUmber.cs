using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.IMEI
{
    [Table("IMEINumbers")]
    public class IMEINumber : FullAuditedEntity
    {
        public string IMEINo { get; set; }
        public string Model { get; set; }
        public DateTime? PurchasedDate { get; set; }
    }
}
