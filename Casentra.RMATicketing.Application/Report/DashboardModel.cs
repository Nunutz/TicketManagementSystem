using System;

namespace Casentra.RMATicketing.Report
{
    internal class DashboardModel
    {
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int NoofMobiles { get; set; }
        public int TicketId { get; set; }
        public object TicketNo { get; set; }
    }
}