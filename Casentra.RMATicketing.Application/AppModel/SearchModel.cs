using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.AppModel
{
    public class SearchModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int ModelId { get; set; }
        public int StatusId { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsPrivate { get; set; }

    }
}
