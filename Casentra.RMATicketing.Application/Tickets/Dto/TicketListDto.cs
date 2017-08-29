﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casentra.RMATicketing.Tickets;

namespace Casentra.RMATicketing.Tickets.Dto
{
    [AutoMap(typeof(Ticket))]
    public class TicketListDto: Abp.Application.Services.Dto.FullAuditedEntityDto
    {
        public string TicketNo { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime PurchasedDate { get; set; }
        [Required]
        public int TicketStatus { get; set; }

        [Required]
        public int TicketBoard { get; set; }

        [Required]
        public int TicketPriority { get; set; }


        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int CapacityId { get; set; }
        [Required]
        public int PhoneConditionId { get; set; }
        [Required]
        public int PhoneProblemId { get; set; }
        [Required]
        public int BoughtAtId { get; set; }
        [Required]
        public string IMEINumber { get; set; }
        [Required]
        public string Password { get; set; }

        public string IcloudAddress { get; set; }
        public string IcloudPassword { get; set; }
        public string Accessories { get; set; }
    }
}
