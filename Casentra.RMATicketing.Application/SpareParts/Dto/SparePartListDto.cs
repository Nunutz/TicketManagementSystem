using Abp.AutoMapper;
using Casentra.RMATicketing.Spares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.SpareParts.Dto
{
    [AutoMap(typeof(SparePart))]
    public class SparePartListDto
    {
        public string SpareName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int BatchTicketId { get; set; }
        public int CustomerId { get; set; }
        public bool IsDelivered { get; set; }
        public int DeliveredUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
