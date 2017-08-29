using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.Models.Ticket
{
    public class TicketProfModel : CustomerModel
    {
        public string IssueSummary { get; set; }
        public string FileName { get; set; }
        public string SpareFileName { get; set; }

        public string Version { get; set; }

        public List<SelectListItem> SpareList { get; set; }
        public List<SpareList> SelectedSpares { get; set; }
    }
}