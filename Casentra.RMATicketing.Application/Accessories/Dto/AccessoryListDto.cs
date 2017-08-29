using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Accessories.Dto
{
    [AutoMap(typeof(Accessory))]
    public class AccessoryListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FrenchName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
