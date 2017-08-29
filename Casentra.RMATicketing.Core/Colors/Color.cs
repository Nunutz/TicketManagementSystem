using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Colors
{
    [Table("Colors")]
    public class Color: FullAuditedEntity
    {
        public string Name { get; set; }

    }
}
