using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Capacities.Dto
{
    [AutoMap(typeof(Capacity))]
    public class CapacityListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
