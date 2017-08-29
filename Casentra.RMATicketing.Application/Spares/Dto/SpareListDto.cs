using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Spares.Dto
{
    [AutoMap(typeof(Spare))]
    public class SpareListDto
    {
        public string Name { get; set; }
        public string FrenchName { get; set; }

    }
}
