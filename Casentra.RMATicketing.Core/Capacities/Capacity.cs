﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Capacities
{
    [Table("Capacities")]
    public class Capacity: FullAuditedEntity
    {
       
        public string Name { get; set; }

        
    }
}
