using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Customers.Dto
{
    [AutoMap(typeof(Customer))]
    public class CreateCustomerInput
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }


        public string LastName { get; set; }

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

        [Required]
        public string Email { get; set; }

        public bool IsVip { get; set; }

        public bool AllowEmailNotification { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDisabled { get; set; }

    }
}
