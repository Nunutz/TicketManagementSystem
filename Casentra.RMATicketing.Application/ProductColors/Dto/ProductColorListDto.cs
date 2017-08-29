using Abp.AutoMapper;
using Casentra.RMATicketing.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.ProductColors.Dto
{
    [AutoMap(typeof(Color))]
    public class ProductColorListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
