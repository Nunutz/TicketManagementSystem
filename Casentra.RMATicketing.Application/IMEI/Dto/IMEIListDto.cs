using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.IMEI.Dto
{
    [AutoMap(typeof(IMEINumber))]
    public class IMEIListDto
    {
        public string IMEINo { get; set; }
        public string Model { get; set; }
        public DateTime? PurchasedDate { get; set; }
    }
}
