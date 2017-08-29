using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing
{
    public class TicketCustomerModel
    {
        //customer Information
        public int CustomerId { get; set;}
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }

        //ticket informaion

        public int TicketId { get; set; }

        public int BatchItemId { get; set; }
        public string TicketNo { get; set; }
        public string IssueSummary { get; set; }
        public DateTime PurchasedDate { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int TicketStatusId { get; set; }
        public int TicketBoardId { get; set; }

        public int TicketPriorityId { get; set; }

        public int BrandId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int CapacityId { get; set; }
        public int PhoneConditionId { get; set; }
        public int PhoneProblemId { get; set; }
        public int AccessoryId { get; set; }

        public int BoughtAtId { get; set; }
        public string IMEINumber { get; set; }
        public string Password { get; set; }
        public string IcloudAddress { get; set; }
        public string IcloudPassword { get; set; }
        public string Note { get; set; }
        public string PreviousNote { get; set; }


        //names
        public string TicketStatus { get; set; }
        public string TicketBoard { get; set; }
        public string TicketPriority { get; set; }
        public string Brand { get; set; }
        public string Product { get; set; }
        public string Color { get; set; }
        public string Capacity { get; set; }
        public string PhoneCondition { get; set; }
        public string PhoneProblem { get; set; }
        public string Accessory { get; set; }
        public string BoughtAt { get; set; }


        public string LastUpatedDate { get; set; }
        public string LastUpatedUser { get; set; }
        public string LastUpatedStatus { get; set; }

        public string Accessories { get; set; }
        public string PhoneProblems { get; set; }
        public string PhoneProblemsInFrench { get; set; }
        public string PhoneProblemsInChinese { get; set; }
        public string ProductName { get; set; }

        public string TrackingNumber { get; set; }
        public bool IsProfessional { get; set; }
    }
}
