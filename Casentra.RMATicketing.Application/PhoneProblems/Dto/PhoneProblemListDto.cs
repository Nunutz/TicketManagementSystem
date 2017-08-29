using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.PhoneProblems.Dto
{
    [AutoMap(typeof(PhoneProblem))]
    public class PhoneProblemListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
