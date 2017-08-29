using Casentra.RMATicketing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.AppModel
{
    public class ReportModel
    {
        public string TicketNo { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Product { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public string StatusName { get; set; }
        public string IMEINumber { get; set; }
        public string CreatedDate { get; set; }
        public AdminTicketStatus Status {get;set;}

    }

    public class CountModel
    {
        
        public int Count { get; set; }
        public string StatusName { get; set; }
        
        public int Status { get; set; }
    }
}
