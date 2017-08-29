using Abp.Domain.Repositories;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.AppModel;
using Casentra.RMATicketing.BatchItems;
using Casentra.RMATicketing.BatchTickets;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.Notes;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Products;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Report
{
    public class ReportAppService: RMATicketingAppServiceBase,IReportAppService
    {
        private readonly IRepository<BatchTicket> _batchTicketRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<BatchItem> _batchItemRepository;
        private readonly IRepository<SparePart> _sparePartRepository;
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PhoneProblem> _problemRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IRepository<BoughtAt> _boughtAtRepository;
        private readonly IRepository<Spare> _spareRepository;

        public ReportAppService(IRepository<BatchTicket> batchTicketRepository, IRepository<Customer> customerRepository,
           IRepository<BatchItem> batchItemRepository, IRepository<SparePart> sparePartRepository, IRepository<Note> noteRepository,
           IRepository<Ticket> ticketRepository,
           IRepository<Brand> brandRepository,
            IRepository<Product> productRepository,
            IRepository<PhoneProblem> problemRepository,
            IRepository<Accessory> accessoryRepository,
             IRepository<BoughtAt> boughtAtRepository,
             IRepository<Spare> spareRepository
           )
        {
            _batchTicketRepository = batchTicketRepository;
            _customerRepository = customerRepository;
            _batchItemRepository = batchItemRepository;
            _sparePartRepository = sparePartRepository;
            _noteRepository = noteRepository;

            _ticketRepository = ticketRepository;
            _problemRepository = problemRepository;
            _accessoryRepository = accessoryRepository;
            _boughtAtRepository = boughtAtRepository;
            _spareRepository = spareRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _spareRepository = spareRepository;
        }

        /// <summary>
        /// Get Search Objects
        /// </summary>
        /// <returns></returns>
        public object GetSearchObjects()
        {
            
            var products = (from b in _brandRepository.GetAll()
                            select new
                            {
                                Id = b.Id,
                                Name = b.Name,
                            }).OrderBy(q => q.Name).ToList();

            var ticketStatus = Helper.GetEnumObject<AdminTicketStatus>();

            return new
            {              
                products,
                ticketStatus
            };

        }

        /// <summary>
        /// Get Batch Ticket Count By Status
        /// </summary>
        /// <returns></returns>
        public object GetBatchTicketCountByStatus()
        {
            var Counts = (from t in _batchItemRepository.GetAll()
                          where t.ClosedDate == null
                          group t by t.TicketStatus into g
                          select new CountModel
                          {
                              Status = (int)g.Key,
                              StatusName = string.Empty,
                              Count = g.Count()
                          }).ToList();

            ////update the status with names
            foreach (var t in Counts)
            {
                t.StatusName = t.Status == 0 ? "Client Request" : Common.AppCommon.AdminStatusName(t.Status);
            }

            var ticketCounts = (from t in Counts
                                select new
                                {
                                    StatusName = t.StatusName,
                                    Status = t.Status,
                                    Count = t.Count,
                                }).ToArray();

            return new
            {
                ticketCounts = ticketCounts
            };

        }

        /// <summary>
        /// Get Ticket Count By Status
        /// </summary>
        /// <returns></returns>
        public object GetTicketCountByStatus()
        {
            var Counts = (from t in _ticketRepository.GetAll()
                          where t.ClosedDate == null
                          group t by t.TicketStatus into g
                          select new CountModel
                          {
                              Status = (int)g.Key,
                              StatusName = string.Empty,
                              Count = g.Count()
                          }).ToList();


            ////update the status with names
            foreach (var t in Counts)
            {
                t.StatusName = t.Status == 0 ? "Client Request" : Common.AppCommon.AdminStatusName(t.Status);
            }

            var ticketCounts = (from t in Counts
                                select new
                                {
                                    StatusName =t.StatusName,
                                    Status= t.Status,
                                    Count = t.Count,
                                }).ToArray();
            
            return new
            {
                ticketCounts = ticketCounts
            };
        }

        /// <summary>
        /// Get Batch Tickets Array
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public object GetBatchTicketsArray(SearchModel search)
        {

            var searchResults = (from b in _batchTicketRepository.GetAll()
                                 join t in _batchItemRepository.GetAll()
                                 on b.Id equals t.BatchTicketId
                                 join c in _customerRepository.GetAll()
                                 on b.CustomerId equals c.Id
                                 join p in _brandRepository.GetAll()
                                 on t.Brand equals p.Name
                                 where t.ClosedDate == null
                                 orderby b.CreatedDate descending
                                 select new ReportModel
                                 {
                                     TicketNo = b.BatchTicketNo,
                                     CustomerName = c.FirstName + " " + c.LastName,
                                     Email = c.Email,
                                     MobileNumber = c.MobileNumber,
                                     Product = t.Product,
                                     Brand = t.Brand,
                                     BrandId = p.Id,
                                     Status = t.TicketStatus,
                                     StatusName = String.Empty,
                                     IMEINumber = t.IMEINumber,
                                     CreatedDate = b.CreatedDate.ToString(),

                                 }).Distinct().ToArray();

            if (!string.IsNullOrEmpty(search.FromDate) && !string.IsNullOrEmpty(search.ToDate))
            {
                var fromDate = Convert.ToDateTime(search.FromDate);
                var toDate = Convert.ToDateTime(search.ToDate);

                searchResults = (from t in searchResults
                                 where Convert.ToDateTime(t.CreatedDate) >= fromDate && Convert.ToDateTime(t.CreatedDate) <= toDate
                                 select t).ToArray();
            }

            if (search.ModelId > 0)
            {
                searchResults = (from t in searchResults
                                 where Convert.ToInt32(t.BrandId) == search.ModelId
                                 select t).ToArray();
            }

            if (search.StatusId > 0)
            {
                searchResults = (from t in searchResults
                                 where Convert.ToInt32(t.Status) == search.StatusId
                                 select t).ToArray();
            }

            foreach (var item in searchResults)
            {
                item.StatusName = Common.AppCommon.AdminStatusName((int)item.Status);
            }

            var results = (from t in searchResults
                           select new
                           {
                               TicketNo = t.TicketNo,
                               CustomerName = t.CustomerName,
                               Email = t.Email,
                               MobileNumber = t.MobileNumber,
                               Product = t.Product,
                               Brand = t.Brand,
                               BrandId = t.BrandId,
                               Status = t.Status,
                               StatusName = t.StatusName,
                               IMEINumber = t.IMEINumber,
                               CreatedDate = t.CreatedDate.ToString(),
                           });

            return new
            {
                searchResult = results
            };
        }

        /// <summary>
        /// Get Tickets Array
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public object GetTicketsArray(SearchModel search)
        {
            if(search.IsProfessional)
            {
                return GetBatchTicketsArray(search);
            }

            var searchResults =(from t in _ticketRepository.GetAll()
                                               join c in _customerRepository.GetAll()
                                               on t.CustomerId equals c.Id
                                               join p in _productRepository.GetAll()
                                               on t.ProductId equals p.Id
                                               join b in _brandRepository.GetAll()
                                               on t.BrandId equals b.Id
                                               where t.ClosedDate == null
                                               orderby t.CreatedDate descending
                                               select new ReportModel
                                               {
                                                   TicketNo = t.TicketNo,
                                                   CustomerName = c.FirstName + " " + c.LastName,
                                                   Email = c.Email,
                                                   MobileNumber = c.MobileNumber,
                                                   Product = p.Name,
                                                   Brand = b.Name,
                                                   BrandId=b.Id,
                                                   Status=t.TicketStatus,
                                                   StatusName=String.Empty,
                                                   IMEINumber = t.IMEINumber,
                                                   CreatedDate = t.CreatedDate.ToString(),

                                               }).ToArray();

            if(!string.IsNullOrEmpty(search.FromDate) && !string.IsNullOrEmpty(search.ToDate))
            {
                var fromDate = Convert.ToDateTime(search.FromDate);
                var toDate = Convert.ToDateTime(search.ToDate);

                searchResults = (from t in searchResults
                                 where Convert.ToDateTime(t.CreatedDate) >= fromDate && Convert.ToDateTime(t.CreatedDate) <= toDate
                                 select t).ToArray();
            }

            if (search.ModelId>0)
            {
               searchResults = (from t in searchResults
                                 where Convert.ToInt32(t.BrandId)== search.ModelId
                                 select t).ToArray();
            }

            if (search.StatusId > 0)
            {
                searchResults = (from t in searchResults
                                 where Convert.ToInt32(t.Status) == search.StatusId
                                 select t).ToArray();
            }

            foreach (var item in searchResults)
            {
                item.StatusName = Common.AppCommon.AdminStatusName((int)item.Status);
            }


            var results = (from t in searchResults
                           select new
                           {
                               TicketNo = t.TicketNo,
                               CustomerName = t.CustomerName,
                               Email = t.Email,
                               MobileNumber = t.MobileNumber,
                               Product = t.Product,
                               Brand = t.Brand,
                               BrandId = t.BrandId,
                               Status = t.Status,
                               StatusName = t.StatusName,
                               IMEINumber = t.IMEINumber,
                               CreatedDate = t.CreatedDate.ToString(),
                           });

            return new
            {
                searchResult = results
            };
        }
    }
}
