using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Products;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Tickets;
using Casentra.RMATicketing.Web.Models.Ticket;
using Casentra.RMATicketing.Web.ViewModelBuilder;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.Controllers
{
    public class PrivateController : RMATicketingControllerBase
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PhoneProblem> _problemRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IRepository<BoughtAt> _boughtAtRepository;
        private readonly IRepository<Spare> _spareRepository;
        private readonly TicketModelBuilder _ticketModelBuilder;

        private const string attachmentPath1 = @"EmailAttachments/FICHE-DE-RETOUR-Price-Minister-Online-B2B.pdf";        
        private const string attachmentPath2 = @"EmailAttachments/FICHE-DE-RETOUR-SAV-Fnac-Darty-Online-GS.pdf";
        private const string attachmentPath3 = @"EmailAttachments/Fiche-de-retour-B2B.pdf";


        /// <summary>
        ///  Constructor for Ticket Controller
        /// </summary>
        /// <param name="ticketRepository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="brandRepository"></param>
        /// <param name="productRepository"></param>
        /// <param name="problemRepository"></param>
        /// <param name="accessoryRepository"></param>
        /// <param name="boughtAtRepository"></param>
        public PrivateController(IRepository<Ticket> ticketRepository, IRepository<Customer> customerRepository,
            IUnitOfWorkManager unitOfWorkManager,
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
            _accessoryRepository = accessoryRepository;
            _boughtAtRepository = boughtAtRepository;
            _problemRepository = problemRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _spareRepository = spareRepository;

            _unitOfWorkManager = unitOfWorkManager;
            _ticketModelBuilder = new TicketModelBuilder(ticketRepository, customerRepository, brandRepository, productRepository,
                problemRepository, accessoryRepository, boughtAtRepository, spareRepository);
        }

        /// <summary>
        ///  French Ticket creation page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ticket()
        {
            var model=_ticketModelBuilder.GetPrivateTicketModel();
            _ticketModelBuilder.LoadLookUps(model,true);
            return View(model);
        }


        /// <summary>
        ///  English Ticket creation page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EnTicket()
        {
            var model = _ticketModelBuilder.GetPrivateTicketModel();
            _ticketModelBuilder.EngLoadLookUps(model,true);
            return View(model);
        }

        /// <summary>
        ///  French Ticket creation page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Client()
        {
            var model = _ticketModelBuilder.GetPrivateTicketModel();
            _ticketModelBuilder.LoadLookUps(model,false);
            return View(model);
        }

        /// <summary>
        ///  French Ticket creation page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EngClient()
        {
            var model = _ticketModelBuilder.GetPrivateTicketModel();
            _ticketModelBuilder.EngLoadLookUps(model,false);
            return View(model);
        }

        /// <summary>
        ///  saving the ticket
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <param name="returnUrlHash"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveTicket(TicketModel model)
        {
            try
            {             
                CheckModelState();
                var ticket = _ticketModelBuilder.GetTicket(model);
                var customer = _ticketModelBuilder.GetCustomer(model);

                //if existing customer
                var isExist = _customerRepository.GetAll().Where(q => q.Email == model.Email && q.FirstName == model.FirstName && q.LastName == model.LastName &&!q.IsTrading).FirstOrDefault();
                if(isExist==null)
                {
                    var customerId = await _customerRepository.InsertAndGetIdAsync(customer);
                    ticket.CustomerId = customerId;
                }
                else
                ticket.CustomerId = isExist.Id;

                var ticketId = await _ticketRepository.InsertAndGetIdAsync(ticket);

                //update ticket No
                ticket.TicketNo = _ticketModelBuilder.TicketNo(ticketId);
                await _ticketRepository.UpdateAsync(ticket);

                var path = GetAttachmentPath(model.BoughtAtId, model.IsProfessional);

                EmailService.EmailService.CreateTicket(customer.Email, customer.FirstName, "Ticket Creation", _ticketModelBuilder.TicketDetail(model, ticket.TicketNo), path);

                return Json(new Abp.Web.Models.AjaxResponse { Result = ticketId });

            }
            catch (Exception ex)
            {
                return Json(new Abp.Web.Models.AjaxResponse { Result = ex.Message });
            }
           
        }

        private string GetAttachmentPath(int baughtAt,bool IsProfessional)
        {
            var path = string.Empty;
            var result = (from b in _boughtAtRepository.GetAll()
                          where b.Id == baughtAt select b).FirstOrDefault();
             
            if(result != null && !IsProfessional)
            {       
                var baught = result.Name.ToLower();
                //Amazon / Cdiscount / PriceMinister / Lazada / Nunutz.com / Raidfox Shop
                if(baught.Contains("amazon")||baught.Contains("cdiscount") || baught.Contains("priceminister") || baught.Contains("lazada")
                    || baught.Contains("nunutz.com") || baught.Contains("raidfox shop"))
                {
                    return Server.MapPath("~/" + attachmentPath1);
                                               
                } 

                //Darty/Macway/Fnac/Pixmania
                if (baught.Contains("darty") || baught.Contains("macway") || baught.Contains("fnac") || baught.Contains("pixmania"))
                {
                    return Server.MapPath("~/"+ attachmentPath2);                                
                }
                              
            }

            //for client pro
            try
            {
                if (IsProfessional) // but not Trading
                {
                    var enumName = EnumHelper<EngBoughtAtList>.GetDisplayValue((EngBoughtAtList)baughtAt);
                    if (!string.IsNullOrEmpty(enumName) && path == string.Empty)
                        return Server.MapPath("~/" + attachmentPath3);
                }
            }
            catch (Exception)
            {

            }
            
            return path;
        }
    }
}  
 