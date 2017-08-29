using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BoughtAts
{
    [Table("BoughtAts")]
    public class BoughtAt: FullAuditedEntity
    {
        public string Name { get; set; }
        public string FrenchName { get; set; }
    }

}
