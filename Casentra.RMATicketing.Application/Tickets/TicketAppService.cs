using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Casentra.RMATicketing.Tickets.Dto;
using Casentra.RMATicketing.Customers;
using Abp.AutoMapper;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.Users;
using Abp.Runtime.Session;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.Products;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Spares;

namespace Casentra.RMATicketing.Tickets
{
    [AbpAuthorize]
    public class TicketAppService : RMATicketingAppServiceBase, ITicketAppService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Customer> _customerRepository;

        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PhoneProblem> _problemRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IRepository<BoughtAt> _boughtAtRepository;
        private readonly IRepository<Spare> _spareRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;
        private readonly UserManager _userManager;
        private readonly IAbpSession _session;

        public TicketAppService(IRepository<Ticket> ticketRepository, IRepository<Customer> customerRepository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context,
            UserManager userManager, IAbpSession session,
            IRepository<Brand> brandRepository,
            IRepository<Product> productRepository,
            IRepository<PhoneProblem> problemRepository,
            IRepository<Accessory> accessoryRepository,
            IRepository<BoughtAt> boughtAtRepository,
            IRepository<Spare> spareRepository
            )
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _problemRepository = problemRepository;
            _accessoryRepository = accessoryRepository;
            _boughtAtRepository = boughtAtRepository;
            _spareRepository = spareRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userManager = userManager;
            _session = session;
            _context = context;
        }

        public async Task<ListResultDto<TicketListDto>> GetAllTicketsAsync()
        {
            var tickets= await _ticketRepository.GetAllListAsync();
            return new ListResultDto<TicketListDto>(tickets.OrderBy(o => o.CreatedDate).MapTo<List<TicketListDto>>());
        }
        public async Task<TicketListDto> GetTicketForEditAsync(NullableIdDto<int> input)
        {
            var ticket = await _ticketRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
            var result = ticket.MapTo<TicketListDto>();
            return result;
        }
        public object GetTicketObject(NullableIdDto<int> input)
        {          
            var query = (from t in _ticketRepository.GetAll()
                           join c in _customerRepository.GetAll()
                           on t.CustomerId equals c.Id
                           join p in _productRepository.GetAll()
                           on t.ProductId equals p.Id
                           where t.TicketBoard == TicketBoard.France 
                           && t.Id == input.Id.Value
                           select new TicketCustomerModel
                           {
                               TicketId = t.Id,
                               TicketNo = t.TicketNo,
                               CustomerId = c.Id,
                               FirstName = c.FirstName,
                               LastName = c.LastName,
                               Address = c.Address,
                               City = c.City,
                               Zipcode = c.Zipcode,
                               PhoneNumber = c.PhoneNumber,
                               MobileNumber = c.MobileNumber,
                               Email = c.Email,

                               TicketStatus = ((AdminTicketStatus)t.TicketStatus).ToString(),
                               TicketPriority =((TicketPriority) t.TicketPriority).ToString(),
                               TicketBoard =((TicketBoard) t.TicketBoard).ToString(),

                               TicketStatusId = (int)t.TicketStatus,
                               TicketPriorityId = (int)t.TicketPriority,
                               TicketBoardId = (int)t.TicketBoard,
                               ProductId = t.ProductId,
                               BrandId = t.BrandId,
                               ColorId = t.ColorId,
                               CapacityId = t.CapacityId,
                               PhoneConditionId = t.PhoneConditionId,
                               PhoneProblemId = t.PhoneProblemId,
                               AccessoryId = t.AccessoryId,
                               BoughtAtId = t.BoughtAtId,
                               Password = t.Password,
                               IcloudAddress = t.IcloudAddress,
                               IcloudPassword = t.IcloudPassword,
                               Note = string.IsNullOrEmpty(t.Description)?string.Empty: t.Description,
                               PreviousNote = t.Description,

                               IMEINumber = t.IMEINumber,
                               CreatedDate = t.CreatedDate,
                               PurchasedDate = t.PurchasedDate,
                               IssueSummary = t.Summary,

                               LastUpatedDate=t.LastModificationTime.ToString(),
                               LastUpatedStatus= ((AdminTicketStatus)t.TicketStatus).ToString(),
                               LastUpatedUser=t.LastModifierUserId.ToString(),
                               Accessory=t.Accessories.Trim(),
                               PhoneProblem=t.PhoneProblems.Trim(),
                               PhoneProblemsInFrench = t.PhoneProblemsInFrench.Trim(),
                               PhoneProblemsInChinese = t.PhoneProblemsInChinese.Trim(),
                               ProductName=p.Name,
                               TrackingNumber=t.TrackingNumber,

                           }).ToList().FirstOrDefault();

            
            //format object
            var previousNotes = query.Note.Split('*');
            var cDate = Convert.ToDateTime(query.CreatedDate).ToShortDateString();
            var pDate = Convert.ToDateTime(query.PurchasedDate).ToShortDateString();

            var ticketObj = new
            {
                TicketId = query.TicketId,
                TicketNo=query.TicketNo,
                CustomerId = query.CustomerId,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Address = query.Address,
                City = query.City,
                Zipcode = query.Zipcode,
                PhoneNumber = query.PhoneNumber,
                MobileNumber = query.MobileNumber,
                Email = query.Email,

                TicketStatus = query.TicketStatusId == 0 ? "Client Request" : Common.AppCommon.AdminStatusName((int)query.TicketStatusId),
               
                TicketPriority = query.TicketPriority,
                TicketBoard = query.TicketBoard,

                TicketStatusId = query.TicketStatusId,
                TicketPriorityId = query.TicketPriorityId,
                TicketBoardId = query.TicketBoardId,
                ProductId = query.ProductId,
                BrandId = query.BrandId,
                ColorId = query.ColorId,
                CapacityId = query.CapacityId,
                PhoneConditionId = query.PhoneConditionId,
                PhoneProblemId = query.PhoneProblemId,
                AccessoryId = query.AccessoryId,
                BoughtAtId = query.BoughtAtId,
                Password = query.Password,
                IcloudAddress = query.IcloudAddress,
                IcloudPassword = query.IcloudPassword,
                //Note = query.Note,
                Notes= previousNotes,
                PreviousNote=query.Note,

                IMEINumber = query.IMEINumber,
                CreatedDate = cDate,
                PurchasedDate = pDate,
                IssueSummary = query.IssueSummary,
                Accessories =string.IsNullOrEmpty(query.Accessory)?"":query.Accessory.Substring(0, query.Accessory.Length - 1),
                PhoneProblems = string.IsNullOrEmpty(query.PhoneProblem) ?"" : query.PhoneProblem.Substring(0, query.PhoneProblem.Length - 1),
                PhoneProblemsInFrench = string.IsNullOrEmpty(query.PhoneProblemsInFrench) ? "" : query.PhoneProblemsInFrench.Substring(0, query.PhoneProblemsInFrench.Length - 1),
                PhoneProblemsInChinese = string.IsNullOrEmpty(query.PhoneProblemsInChinese) ? "" : query.PhoneProblemsInChinese.Substring(0, query.PhoneProblemsInChinese.Length - 1),
                ProductName = query.ProductName,
                TrackingNumber=query.TrackingNumber
            };

            return ticketObj;
            
        }
        
        public async Task<int> CreateOrEditTicketAsync(CreateTicketInput input)
        {
            var ticket = input.MapTo<Ticket>();
            return await _ticketRepository.InsertAndGetIdAsync(ticket);

        }

        public async Task<int> UpdateTicket(TicketCustomerModel input)
        {
            try
            {
                var ticket = GetTicket(input);
                var customer = GetCustomer(input);
                var updateTicket = await _ticketRepository.UpdateAsync(ticket);
                var updateCustome= await _customerRepository.UpdateAsync(customer);
                //send email
                Common.AppCommon.SendEMail(input.TicketStatusId, customer.Email, customer.FirstName, input.Note);
                return updateTicket.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> CreateBrandAsync(Brand input)
        {
            return await _brandRepository.InsertAndGetIdAsync(input);

        }
        public async Task<int> CreateProductAsync(Product input)
        {
            return await _productRepository.InsertAndGetIdAsync(input);
        }

        public async Task<int> CreateProblemAsync(PhoneProblem input)
        {
            return await _problemRepository.InsertAndGetIdAsync(input);

        }
        public async Task<int> CreateAccessoryAsync(Accessory input)
        {
            return await _accessoryRepository.InsertAndGetIdAsync(input);

        }
        public async Task<int> CreateBoughtAtAsync(BoughtAt input)
        {
            return await _boughtAtRepository.InsertAndGetIdAsync(input);

        }

        #region Update and Delete
        public async Task<int> UpdatePhoneProblemAsync(PhoneProblem input)
        {
            var problem = await _problemRepository.UpdateAsync(input);
            return problem.Id;

        }
        public async void DeletePhoneProblemAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _problemRepository.DeleteAsync(id);
        }

        public async Task<int> UpdateBrandAsync(Brand input)
        {
            var accessory = await _brandRepository.UpdateAsync(input);
            return accessory.Id;
        }
        public async void DeleteBrandAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _brandRepository.DeleteAsync(id);
        }

        public async Task<int> UpdateProductAsync(Product input)
        {
            var product = await _productRepository.UpdateAsync(input);
            return product.Id;

        }
        public async void DeleteProductAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _productRepository.DeleteAsync(id);

        }

        public async Task<int> UpdateAccessoryAsync(Accessory input)
        {
            var accessory = await _accessoryRepository.UpdateAsync(input);
            return accessory.Id;

        }
        public async void DeleteAccessoryAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _accessoryRepository.DeleteAsync(id);
        }

        public async Task<int> UpdateBoughtAtAsync(BoughtAt input)
        {
            var boughtAt = await _boughtAtRepository.UpdateAsync(input);
            return boughtAt.Id;
        }
        public async void DeleteBoughtAtAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _boughtAtRepository.DeleteAsync(id);
        }
        #endregion
     

        public object GetDataEntries()
        {
            var brands = (from b in _brandRepository.GetAll()
                          select new {
                              Id=b.Id,
                              Name=b.Name
                          }).OrderBy(q => q.Name).ToList();

            var products = (from p in _productRepository.GetAll()
                           join  b in _brandRepository.GetAll()
                           on p.BrandId equals b.Id
                          select new
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Brand=b.Name,
                              BrandId = b.Id,

                          }).OrderBy(q => q.Name).ToList();

            var problems = (from b in _problemRepository.GetAll()
                          select new
                          {
                              Id = b.Id,
                              Name = b.Name,
                              FrenchName = b.FrenchName,
                              ChinesName = b.ChinesName,
                          }).OrderBy(q => q.Name).ToList();

            var accessories = (from b in _accessoryRepository.GetAll()
                            select new
                            {
                                Id = b.Id,
                                Name = b.Name,
                                FrenchName=b.FrenchName

                            }).OrderBy(q => q.Name).ToList();

            var boughtats = (from b in _boughtAtRepository.GetAll()
                               select new
                               {
                                   Id = b.Id,
                                   Name = b.Name,
                                   FrenchName = b.FrenchName
                               }).OrderBy(q => q.Name).ToList();

            var spares = (from b in _spareRepository.GetAll()
                             select new
                             {
                                 Id = b.Id,
                                 Name = b.Name,
                                 FrenchName = b.FrenchName
                             }).OrderBy(q => q.Name).ToList();

            return new
            {
                brands,
                products,
                problems,
                accessories,
                boughtats,
                spares
            };

        }
        public object GetTicketsArray()
        {
           var tickets = (from t in _ticketRepository.GetAll()
                     join c in _customerRepository.GetAll()
                     on t.CustomerId equals c.Id
                     join p in _productRepository.GetAll()
                     on t.ProductId equals p.Id
                     join b in _brandRepository.GetAll()
                     on t.BrandId equals b.Id
                     where t.TicketBoard == TicketBoard.France && t.ClosedDate==null
                     orderby t.CreatedDate descending
                     select new
                     {
                         TicketId=t.Id,
                         TicketNo=t.TicketNo,
                         CustomerId=c.Id,
                         CustomerName = c.FirstName + " " + c.LastName,
                         Email = c.Email,
                         MobileNumber = c.MobileNumber,
                         //PhoneNumber = c.PhoneNumber,
                         PhoneLocation = ((TicketBoard)t.TicketBoard).ToString(),
                         Status =  t.TicketStatus == 0 ?"Client Request":((AdminTicketStatus)t.TicketStatus).ToString(),
                         //StatusId= (int)t.TicketStatus,
                         Product = p.Name,
                         Brand = b.Name,
                         IMEINumber = t.IMEINumber,
                         CreatedDate = t.CreatedDate.ToString(),
                         Action= "<button class='btn btn-primary' data-id="+ t.Id +">View Ticket!</button>"

                     }).ToArray();

           

            return new
            {
                ticketData= tickets

            };
        }
        public object GetProfTicketsArray()
        {
            var tickets = (from t in _ticketRepository.GetAll()
                           join c in _customerRepository.GetAll()
                           on t.CustomerId equals c.Id
                           join p in _productRepository.GetAll()
                           on t.ProductId equals p.Id
                           join b in _brandRepository.GetAll()
                           on t.BrandId equals b.Id
                           where t.TicketBoard == TicketBoard.France && t.ClosedDate == null
                           && c.IsProfessional==true
                           orderby t.CreatedDate descending
                           select new
                           {
                               TicketId = t.Id,
                               TicketNo = t.TicketNo,
                               CustomerId = c.Id,
                               CustomerName = c.FirstName + " " + c.LastName,
                               Email = c.Email,
                               MobileNumber = c.MobileNumber,
                               //PhoneNumber = c.PhoneNumber,
                               PhoneLocation = ((TicketBoard)t.TicketBoard).ToString(),
                               Status = t.TicketStatus == 0 ? "Client Request" : ((AdminTicketStatus)t.TicketStatus).ToString(),
                               //StatusId= (int)t.TicketStatus,
                               Product = p.Name,
                               Brand = b.Name,
                               IMEINumber = t.IMEINumber,
                               CreatedDate = t.CreatedDate.ToString(),
                               Action = "<button class='btn btn-primary' data-id=" + t.Id + ">View Ticket!</button>"

                           }).ToArray();



            return new
            {
                ticketData = tickets

            };
        }


        public string TicketNo(int id)
        {
           return string.Format("T{0}", id + 30000); 
        }

        private Ticket GetTicket(TicketCustomerModel model)
        {
            var userId = _session.UserId; //null
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            //always france
            model.TicketBoardId = 1;

            #region note
            var note = string.Empty;
            note += DateTime.Now.ToShortDateString();
            note += ",";
            note += ((AdminTicketStatus)model.TicketStatusId).ToString();
            note += ",";
            note += user.UserName;
            note += ",";
            note += model.Note;
            note += "*";
            note += model.PreviousNote;
            #endregion


            var ticket = new Ticket
            {
                TicketNo=model.TicketNo,
                Id=model.TicketId,
                TicketPriority = (TicketPriority)model.TicketPriorityId,
                TicketStatus = (AdminTicketStatus)model.TicketStatusId,
                TicketBoard = (TicketBoard)model.TicketBoardId,
                BrandId = model.BrandId,
                ProductId = model.ProductId,
                ColorId = model.ColorId,
                CapacityId = model.CapacityId,

                PhoneConditionId = model.PhoneConditionId,
                PhoneProblemId = model.PhoneProblemId,
                AccessoryId = model.AccessoryId,
                BoughtAtId = model.BoughtAtId,
                IMEINumber = model.IMEINumber,
                Password = model.Password,
                IcloudAddress = model.IcloudAddress,
                IcloudPassword = model.IcloudPassword,
                PurchasedDate = Convert.ToDateTime(model.PurchasedDate),
                CreatedDate = Convert.ToDateTime(model.CreatedDate),
                ClosedDate = model.ClosedDate,
                Summary=model.IssueSummary,
                Description= note,
                CustomerId=model.CustomerId,
                Accessories = model.Accessories,
                PhoneProblems = model.PhoneProblems,
                TrackingNumber=model.TrackingNumber

            };

            return ticket;
        }

        private Customer GetCustomer(TicketCustomerModel model)
        {
            var customer = new Customer
            {
                Id=model.CustomerId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                Zipcode = model.Zipcode,
                PhoneNumber = model.PhoneNumber,
                MobileNumber = model.MobileNumber,
                IsProfessional = model.IsProfessional,
                Street = string.Empty,

            };
            return customer;
        }        

        public async Task<int> CreateSpareAsync(Spare input)
        {
            return await _spareRepository.InsertAndGetIdAsync(input);
        }

        public async Task<int> UpdateSpareAsync(Spare input)
        {
            var spare = await _spareRepository.UpdateAsync(input);
            return spare.Id;
        }

        public async void DeleteSpareAsync(NullableIdDto<int> input)
        {
            var id = (int)input.Id;
            await _spareRepository.DeleteAsync(id);
        }

        public object GetLookups()
        {
            var ticketStatus = Helper.GetEnumObject<AdminTicketStatus>();
            var ticketPriority = Helper.GetEnumObject<TicketPriority>();
            var ticketBoard = Helper.GetEnumObject<TicketBoard>();
            var productColors = Helper.GetEnumObject<ColorList>();
            var capacities = Helper.GetEnumObject<CapacityList>();
            var phoneConditions = Helper.GetEnumObject<ConditionList>();

            var boughtAtClients = Helper.GetEnumObject<EngBoughtAtList>();


            var brands = (from b in _brandRepository.GetAll()
                          select new
                          {
                              Id = b.Id,
                              Name = b.Name
                          }).OrderBy(q => q.Name).ToList();

            var products = (from p in _productRepository.GetAll()
                            join b in _brandRepository.GetAll()
                            on p.BrandId equals b.Id
                            select new
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Brand = b.Name,
                            }).OrderBy(q => q.Name).ToList();

            var phoneProblems = (from b in _problemRepository.GetAll()
                                 select new
                                 {
                                     Id = b.Id,
                                     Name = b.Name
                                 }).OrderBy(q => q.Name).ToList();


            var boughtAts = (from b in _boughtAtRepository.GetAll()
                             select new
                             {
                                 Id = b.Id,
                                 Name = b.Name
                             }).OrderBy(q => q.Name).ToList();

            //if (!isPrivate)
            //{
            //    boughtAts = Helper.GetEnumObject<EngBoughtAtList>();
            //}


            var accesseries = (from b in _accessoryRepository.GetAll()
                               select new
                               {
                                   Id = b.Id,
                                   Name = b.Name
                               }).OrderBy(q => q.Name).ToList();

            return new
            {
                boughtAts,
                boughtAtClients,
                brands,
                products,
                productColors,
                capacities,
                accesseries,
                phoneConditions,
                phoneProblems,
                ticketStatus,
                ticketPriority,
                ticketBoard

            };
        }

    }
}
