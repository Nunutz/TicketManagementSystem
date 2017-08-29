using Casentra.RMATicketing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.AppModel
{
    public class BatchItemModel
    {
        public int BatchItemId { get; set; }
        public int TicketId { get; set; }
        public int CustomerId { get; set; }
        public string  TicketNo { get; set; }
        public AdminTicketStatus TicketStatus { get; set; }
        public string TicketStatusName { get; set; }
        public TicketBoard TicketBoard { get; set; }
        public  string TicketBoardName { get; set; }
        public string Product { get; set; }
        public string Brand { get; set; }
        public string PhoneProblem { get; set; }
        public string Color { get; set; }
        public string Capacity { get; set; }
        public string PhoneCondition { get; set; }
        public string Accessory { get; set; }
        public string BoughtAt { get; set; }
        public string Password { get; set; }
        public string IcloudAddress { get; set; }
        public string IcloudPassword { get; set; }
        public string IMEINumber { get; set; }
        public DateTime PurchasedDate { get; set; }

        public string IssueSummary { get; set; }


    }
}
