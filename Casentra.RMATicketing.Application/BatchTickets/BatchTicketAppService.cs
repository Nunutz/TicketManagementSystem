
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Casentra.RMATicketing.BatchItems;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.Notes;
using Casentra.RMATicketing.AppModel;

namespace Casentra.RMATicketing.BatchTickets
{
    public class BatchTicketAppService : RMATicketingAppServiceBase, IBatchTicketAppService
    {
        private readonly IRepository<BatchTicket> _ticketRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<BatchItem> _batchItemRepository;
        private readonly IRepository<SparePart> _spareRepository;
        private readonly IRepository<Note> _noteRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;
        private readonly UserManager _userManager;
        private readonly IAbpSession _session;

        public BatchTicketAppService(IRepository<BatchTicket> ticketRepository, IRepository<Customer> customerRepository,
            IRepository<BatchItem> batchItemRepository, IRepository<SparePart> spareRepository, IRepository<Note> noteRepository,
            IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context,
            UserManager userManager, IAbpSession session)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _batchItemRepository = batchItemRepository;
            _spareRepository = spareRepository;
            _noteRepository = noteRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
            _userManager = userManager;
            _session = session;
        }

        /// <summary>
        /// Get Batch Tickets Array
        /// </summary>
        /// <returns></returns>
        public object GetBatchTicketsArray()
        {
            var tickets = (from t in _ticketRepository.GetAll()
                           join c in _customerRepository.GetAll()
                           on t.CustomerId equals c.Id
                           orderby t.CreatedDate descending
                           select new DashboardModel
                           {
                               TicketId = t.Id,
                               TicketNo = t.BatchTicketNo,
                               CustomerId = c.Id,
                               CustomerName = c.FirstName + " " + c.LastName,
                               Email = c.Email,
                               MobileNumber = c.MobileNumber,
                               NoofMobiles=0,
                               CreatedDate = t.CreatedDate,
                               Action = "<button class='btn btn-primary' data-id=" + t.Id + ">View Ticket!</button>"

                           }).ToList();


            foreach (var t in tickets)
            {
                t.NoofMobiles = (from s in _batchItemRepository.GetAll()
                                    where s.BatchTicketId == t.TicketId && s.CustomerId == t.CustomerId
                                 select s).Count();
            }

            var ticketObj = (from t in tickets
                             select new
                             {
                                 TicketId = t.TicketId,
                                 TicketNo = t.TicketNo,
                                 CustomerId = t.CustomerId,
                                 CustomerName = t.CustomerName,
                                 Email = t.Email,
                                 MobileNumber = t.MobileNumber,
                                 NoofMobiles = t.NoofMobiles,
                                 CreatedDate = t.CreatedDate.ToShortDateString(),
                                 Action = t.Action

                             }).ToArray();

            return new
            {
                ticketData = ticketObj
            };

        }

        /// <summary>
        /// Get Batch Ticket Object
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object GetBatchTicketObject(NullableIdDto<int> input)
        {
            var query = (from t in _ticketRepository.GetAll()
                         join c in _customerRepository.GetAll()
                         on t.CustomerId equals c.Id
                         where  t.Id == input.Id.Value
                         select new TicketCustomerModel
                         {
                             TicketId = t.Id,
                             TicketNo = t.BatchTicketNo,
                             CustomerId = c.Id,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             Address = c.Address,
                             City = c.City,
                             Zipcode = c.Zipcode,
                             MobileNumber = c.MobileNumber,
                             Email = c.Email,
                             TicketBoard = t.TicketBoard.ToString(),
                             TicketBoardId = (int)t.TicketBoard,
                             CreatedDate = t.CreatedDate,
                             IssueSummary = t.IssueSummary,
                             
                         }).ToList().FirstOrDefault();



            var batchItems = (from s in _batchItemRepository.GetAll()
                              where s.BatchTicketId == query.TicketId && s.CustomerId == query.CustomerId
                              select new BatchItemModel
                              {
                                  BatchItemId=s.Id,
                                  TicketId = s.BatchTicketId,
                                  TicketNo = query.TicketNo,
                                  CustomerId = query.CustomerId,
                                  TicketStatus =s.TicketStatus,
                                  TicketBoard = s.TicketBoard,
                                  Product = s.Product,
                                  Brand = s.Brand,
                                  Color= s.ProductColor,
                                  Capacity = s.Capacity,
                                  PhoneCondition = s.PhoneCondition,
                                  PhoneProblem = s.PhoneProblem,
                                  Accessory = s.Accessories,
                                  BoughtAt = s.BoughtAt,
                                  Password = s.Password,
                                  IcloudAddress = s.IcloudAddress,
                                  IcloudPassword = s.IcloudPassword,                                 
                                  IMEINumber = s.IMEINumber,
                                  PurchasedDate = s.PurchasedDate,
                                  IssueSummary = s.IssueDetail,

                              }).ToArray();

            //fetch all spar parts
            var spareParts = (from s in _spareRepository.GetAll()
                              where s.BatchTicketId == query.TicketId && s.CustomerId == query.CustomerId
                              select new
                              {
                                  SpareId=s.Id,
                                  BatchTicketId = s.BatchTicketId,
                                  CustomerId = s.CustomerId,
                                  Model =s.Model,
                                  Note=s.Note,
                                  SpareName = s.SpareName,
                                  Quantity = s.Quantity,
                                  IsDelivered = s.IsDelivered,
                                  DeliveredDate = s.DeliveredDate,
                                  CreatedDate = s.CreatedDate,

                              }).ToArray();

            var cDate = query.CreatedDate.ToShortDateString();

            foreach (var i in batchItems)
            {
                i.TicketStatusName = i.TicketStatus == 0 ? "Client Request" : Common.AppCommon.AdminStatusName((int)i.TicketStatus);
                
            }

            var items = (from s in batchItems
                         select new
                         {
                             BatchItemId = s.BatchItemId,
                             TicketId = s.TicketId,
                             TicketNo = s.TicketNo,
                             CustomerId = s.CustomerId,
                             TicketStatus = s.TicketStatus,
                             TicketStatusName = s.TicketStatusName,
                             TicketBoard = s.TicketBoard,
                             Product = s.Product,
                             Brand = s.Brand,
                             Color = s.Color,
                             Capacity = s.Capacity,
                             PhoneCondition = s.PhoneCondition,
                             PhoneProblem = s.PhoneProblem,
                             Accessory = s.Accessory,
                             BoughtAt = s.BoughtAt,
                             Password = s.Password,
                             IcloudAddress = s.IcloudAddress,
                             IcloudPassword = s.IcloudPassword,
                             IMEINumber = s.IMEINumber,
                             PurchasedDate = s.PurchasedDate,
                             IssueSummary=s.IssueSummary

                         }).Distinct().ToArray();

            //return the object
            var ticketObj = new
            {
                TicketId = query.TicketId,
                TicketNo = query.TicketNo,
                CustomerId = query.CustomerId,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Address = query.Address,
                City = query.City,
                Zipcode = query.Zipcode,
                MobileNumber = query.MobileNumber,
                Email = query.Email,
                TicketStatus = query.TicketStatus,
               
                TicketPriority = query.TicketPriority,
                TicketBoard = query.TicketBoard,

                CreatedDate = cDate,
                IssueSummary = query.IssueSummary,
                BatchItems = items,
                SpareParts= spareParts

            };

            return ticketObj;
        }

        /// <summary>
        /// Get Ticket Object
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object GetTicketObject(NullableIdDto<int> input)
        {                      

            var batchItem = (from s in _batchItemRepository.GetAll()
                              where s.Id == input.Id.Value
                              select new
                              {
                                  BatchItemId = s.Id,
                                  TicketId = s.BatchTicketId,                                  
                                  CustomerId = s.CustomerId,
                                  TicketStatus = s.TicketStatus,
                                  TicketBoard = s.TicketBoard,
                                  Product = s.Product,
                                  Brand = s.Brand,
                                  Color = s.ProductColor,
                                  Capacity = s.Capacity,
                                  PhoneCondition = s.PhoneCondition,
                                  PhoneProblem = s.PhoneProblem,
                                  Accessory = s.Accessories,
                                  BoughtAt = s.BoughtAt,
                                  Password = s.Password,
                                  IcloudAddress = s.IcloudAddress,
                                  IcloudPassword = s.IcloudPassword,
                                  IMEINumber = s.IMEINumber,
                                  PurchasedDate = s.PurchasedDate,
                                  IssueSummary = s.IssueDetail,

                                  PhoneProblemsInChinese = s.PhoneProblemsInChinese,
                                  PhoneProblemsInFrench = s.PhoneProblemsInFrench,
                                  TrackingNumber=s.TrackingNumber

                              }).FirstOrDefault();

        
        var notes = (from n in _noteRepository.GetAll()
                              where n.TicketId == input.Id.Value
                              select new
                              {
                                  Note=n.Message,
                                  Status= n.Status,
                                  TicketId=n.TicketId,
                                  BatchTicketId=n.BatchTicketId,
                                  UserId=n.UserId,
                                  UserName=n.UserName,
                                  CreatedDate=n.CreatedDate

                              }).ToArray();

            //return object
            var Obj = new
            {
                BatchItemId = batchItem.BatchItemId,
                TicketId = batchItem.TicketId,

                CustomerId = batchItem.CustomerId,
                TicketStatus = Common.AppCommon.AdminStatusName((int)batchItem.TicketStatus),
                
                TicketStatusId =(int)batchItem.TicketStatus,
                TicketBoard = batchItem.TicketBoard,
                Product = batchItem.Product,
                Brand = batchItem.Brand,
                Color = batchItem.Color,
                Capacity = batchItem.Capacity,
                PhoneCondition = batchItem.PhoneCondition,
                PhoneProblem = batchItem.PhoneProblem,
                PhoneProblemInChinese = batchItem.PhoneProblemsInChinese,
                PhoneProblemInFrench = batchItem.PhoneProblemsInFrench,

                Accessory = batchItem.Accessory,
                BoughtAt = batchItem.BoughtAt,
                Password = batchItem.Password,
                IcloudAddress = batchItem.IcloudAddress,
                IcloudPassword = batchItem.IcloudPassword,
                IMEINumber = batchItem.IMEINumber,
                PurchasedDate = batchItem.PurchasedDate.ToShortDateString(),
                IssueSummary = batchItem.IssueSummary,
                TrackingNumber = batchItem.TrackingNumber,
                notes = notes
            };

            return Obj;
        }

        /// <summary>
        /// Get Spare Part
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object GetSparePart(NullableIdDto<int> input)
        {
            var batchItem = (from s in _batchItemRepository.GetAll()
                             where s.BatchTicketId == input.Id.Value
                             select new
                             {
                                 BatchItemId = s.Id,
                                 TicketId = s.BatchTicketId,                                
                                 CustomerId = s.CustomerId,
                                 TicketStatus = s.TicketStatus,
                                 TicketBoard = s.TicketBoard,
                                 Product = s.Product,
                                 Brand = s.Brand,
                                 Color = s.ProductColor,
                                 Capacity = s.Capacity,
                                 PhoneCondition = s.PhoneCondition,
                                 PhoneProblem = s.PhoneProblem,
                                 Accessory = s.Accessories,
                                 BoughtAt = s.BoughtAt,
                                 Password = s.Password,
                                 IcloudAddress = s.IcloudAddress,
                                 IcloudPassword = s.IcloudPassword,
                                 IMEINumber = s.IMEINumber,
                                 PurchasedDate = s.PurchasedDate,
                                 IssueSummary = s.IssueDetail,
                                 
                             }).FirstOrDefault();

            var Obj = new
            {
                ticket = batchItem
            };

            return Obj;
        }

        public object GetLookups()
        {
            var ticketStatus = Helper.GetEnumObject<AdminTicketStatus>();
            var ticketPriority = Helper.GetEnumObject<TicketPriority>();
            var ticketBoard = Helper.GetEnumObject<TicketBoard>();

            return new
            {
                ticketStatus,
                ticketPriority,
                ticketBoard
            };
        }

        /// <summary>
        /// Update Ticket
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> UpdateTicket(TicketCustomerModel input)
        {
            try
            {
                var userId = _session.UserId;
                var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

                
                var ticket = (from t in _batchItemRepository.GetAll()
                              where t.Id == input.BatchItemId
                              select t).FirstOrDefault();

                if (ticket == null)
                    return 0;

                ticket.TicketStatus = (AdminTicketStatus)input.TicketStatusId;
                ticket.TicketPriority = (TicketPriority)input.TicketPriorityId;
                ticket.TrackingNumber = input.TrackingNumber;

                var updateTicket = await _batchItemRepository.UpdateAsync(ticket);

                var note = new Note
                {
                    Message = input.Note,
                    Status = Common.AppCommon.AdminStatusName(input.TicketStatusId),
                    TicketId = ticket.Id,
                    BatchTicketId = ticket.BatchTicketId,
                    UserId = (int)userId,
                    UserName=user.Name,
                    CreatedDate=DateTime.Now,
                };

                var insertNote = await _noteRepository.InsertAndGetIdAsync(note);

                var customer= (from c in _customerRepository.GetAll()
                               where c.Id == input.CustomerId
                               select c).FirstOrDefault();

                //send email
                if(customer!=null)
                Common.AppCommon.SendEMail(input.TicketStatusId, customer.Email, customer.FirstName, input.Note);

                return input.TicketId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Update Spare
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> UpdateSpare(SparePartModel input)
        {
            try
            {
                var userId = _session.UserId; //null
                var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);


                var spare = (from t in _spareRepository.GetAll()
                              where t.Id == input.SpareId
                              select t).FirstOrDefault();

                if (spare == null)
                    return 0;

                spare.IsDelivered = input.IsDelivered;
                spare.Note = input.Note;
                spare.DeliveredUserId = (int)userId;

                var updateSpare = await _spareRepository.UpdateAsync(spare);
                                
               
                return updateSpare.BatchTicketId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private class DashboardModel
        {
            public int TicketId { get;set;}
            public string TicketNo { get; set; }
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public int NoofMobiles { get; set; }
            public string Status { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Action { get; set; }

        }
        
    }
}
