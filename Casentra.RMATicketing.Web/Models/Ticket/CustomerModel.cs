using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Casentra.RMATicketing.Web.Models.Ticket
{
    public class CustomerModel
    {
        //customer Information
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string MobileNumber { get; set; }

    }
}