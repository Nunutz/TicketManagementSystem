using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Brands.Dto
{
    [AutoMap(typeof(Brand))]
    public class BrandListDto
    {
      public string Name { get; set; }

    }
}
