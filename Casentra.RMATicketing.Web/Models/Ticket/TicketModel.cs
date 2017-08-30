using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.Models.Ticket
{
    
    public class TicketModel: CustomerModel
    {
                    
        //ticket informaion
        public string IssueSummary { get; set; }
        //[Required]
        public DateTime PurchasedDate { get; set; }
        public int TicketStatusId { get; set; }

        public int TicketBoardId { get; set; }

        public int TicketPriorityId { get; set; }
                   
        public DateTime? CustomerResponseDate { get; set; }

        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int CapacityId { get; set; }
        public int PhoneConditionId { get; set; }
        public int PhoneProblemId { get; set; }
        public int AccessoryId { get; set; }

        public int BoughtAtId { get; set; }
       // [Required]
        public string IMEINumber { get; set; }

        //[Required]
        public string Password { get; set; }
       // [Required]
        public string IcloudAddress { get; set; }

       // [Required]
        public string IcloudPassword { get; set; }
                   
        public List<SelectListItem> BoughtAts { get; set; }
        public List<SelectListItem> Brands { get; set; }
        public List<SelectListItem> Products { get; set; }
        public List<SelectListItem> ProductColors { get; set; }
        public List<SelectListItem> Capacities { get; set; }
        public List<SelectListItem> Accesseries { get; set; }
        public List<SelectListItem> PhoneConditions { get; set; }
        public List<SelectListItem> TicketStatus { get; set; }
        public List<SelectListItem> PhoneProblems { get; set; }
        public List<SelectListItem> TicketPriority { get; set; }
        public List<SelectListItem> TicketBoard { get; set; }

        public string Accessories { get; set; }
        public string Problems { get; set; }
        public string ProblemIds { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsTrading { get; set; }

    }

   

    
    
    
}