using Abp.AutoMapper;
using Casentra.RMATicketing.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.ProductNames.Dto
{
    [AutoMap(typeof(Product))]
    public class ProductNameListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
