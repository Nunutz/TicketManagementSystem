using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.AppModel
{
    public  class SparePartModel
    {
        public string SpareName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int BatchTicketId { get; set; }
        public int CustomerId { get; set; }       
        public bool IsDelivered { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Note { get; set; }
        public int SpareId { get; set; }
    }
}
