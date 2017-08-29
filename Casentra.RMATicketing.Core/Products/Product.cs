using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Products
{
    [Table("Products")]
    public class Product: FullAuditedEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }

        
    }
}
