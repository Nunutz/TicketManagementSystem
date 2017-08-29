using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Customers
{
    [Table("Customers")]
    public class Customer: FullAuditedEntity
    {
                      
        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [NotMapped]
        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public bool IsProfessional { get; set; }

        public bool IsTrading { get; set; }



    }
}
