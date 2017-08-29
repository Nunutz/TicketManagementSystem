using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.BatchItems;
using Casentra.RMATicketing.BatchTickets;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.IMEI;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Web.Models.IMEI;
using Casentra.RMATicketing.Web.Models.Ticket;
using Casentra.RMATicketing.Web.ViewModelBuilder;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.Controllers
{
    public class ProfessionalController : RMATicketingControllerBase
    {

        private readonly IRepository<BatchTicket> _ticketRepository;
        private readonly IRepository<BatchItem> _batchItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<SparePart> _sparePartRepository;
        private readonly IRepository<Spare> _spareRepository;
        private readonly IRepository<PhoneProblem> _phoneProblemRepository;
        private readonly IRepository<IMEINumber> _imeiRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly ProfessionalTicketModelBuilder _ticketModelBuilder;
        private const string attachmentPath1 = @"EmailAttachments/FICHE-DE-RETOUR-SAV-B2B.pdf";

        public ProfessionalController(IRepository<BatchTicket> ticketRepository, 
            IRepository<Customer> customerRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<BatchItem> batchItemRepository,
            IRepository<SparePart> sparePartRepository, IRepository<Spare> spareRepository, IRepository<PhoneProblem> phoneProblemRepository, IRepository<IMEINumber> imeiRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _batchItemRepository = batchItemRepository;
            _sparePartRepository = sparePartRepository;
            _spareRepository = spareRepository;
            _phoneProblemRepository = phoneProblemRepository;
            _imeiRepository = imeiRepository;
            _unitOfWorkManager = unitOfWorkManager;

            _ticketModelBuilder = new ProfessionalTicketModelBuilder(spareRepository,ticketRepository, batchItemRepository, customerRepository, sparePartRepository, phoneProblemRepository, imeiRepository);
        }


        // GET: ProfessionalTicket
        [HttpGet]
        public ActionResult Ticket()
        {
            var model = _ticketModelBuilder.GetProfessionalTicketModel();
            _ticketModelBuilder.LoadProfessionalLookUps(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult EnTicket()
        {
            var model = _ticketModelBuilder.GetProfessionalTicketModel();
            _ticketModelBuilder.LoadProfessionalEngLookUps(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult IMEI()
        {
            var model = new ImeiModel();
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UploadIMEI(ImeiModel model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.FileName))
                {
                    // mobile phone list
                    var filePath = Path.Combine(Server.MapPath("~/UploadedExcelFiles/"), model.FileName);
                    var dataTable = _ticketModelBuilder.GetDataFromExcel(filePath, "IMEI");

                    //loop through the excel sheet data
                    foreach (var item in dataTable.Rows)
                    {
                        var row = (DataRow)item;
                        if (!string.IsNullOrEmpty(row[0].ToString()))
                        {
                            if (_ticketModelBuilder.IsExist(row[0].ToString()))
                                continue;
                            
                            var imei = _ticketModelBuilder.GetIMEINumber(row);
                            await _imeiRepository.InsertAndGetIdAsync(imei);
                        }
                    }
                    
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
               

                return Json(new Abp.Web.Models.AjaxResponse { Result = "Successsfully saved !" });
            }
            catch (Exception ex)
            {
                return Json(new Abp.Web.Models.AjaxResponse { Result = ex.Message });
            }
           

        }
        /// <summary>
        /// Save the batch ticket for professional clients
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveProfTicket(TicketProfModel model)
        {
            var ticketId = 0;
            try
            {
                CheckModelState();
                if (string.IsNullOrEmpty(model.FileName) && string.IsNullOrEmpty(model.SpareFileName))
                {
                    return Json(new Abp.Web.Models.AjaxResponse { Result = "Please upload a file" });
                }
                
                //saving customer data
                var customer = _ticketModelBuilder.GetCustomer(model);
                var customerId = 0;
                //if existing customer
                var isExist = _customerRepository.GetAll().Where(q => q.Email == model.Email && q.FirstName == model.FirstName && q.LastName == model.LastName && q.IsTrading).FirstOrDefault();
                if (isExist == null)
                {
                    customerId = await _customerRepository.InsertAndGetIdAsync(customer);
                }
                else
                customerId = isExist.Id;
                
                //saving batch ticket data
                var ticket = _ticketModelBuilder.GetBatchTicket(customerId);
                ticketId = await _ticketRepository.InsertAndGetIdAsync(ticket);
                
                //update the batch ticket number
                ticket.BatchTicketNo = _ticketModelBuilder.TicketNo(ticketId);
                await _ticketRepository.UpdateAsync(ticket);

                if (!string.IsNullOrEmpty(model.FileName))
                {
                    // mobile phone list
                    var filePath = Path.Combine(Server.MapPath("~/UploadedExcelFiles/"), model.FileName);
                    var dataTable = _ticketModelBuilder.GetDataFromExcel(filePath, model.Version);

                    //loop through the excel sheet data
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (string.IsNullOrEmpty(row[0].ToString()))
                            break;

                        var batchItem = _ticketModelBuilder.GetBatchItem(row, ticketId, customerId);
                        await _batchItemRepository.InsertAndGetIdAsync(batchItem);
                    }

                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
              

                // spare parts list
                if(!string.IsNullOrEmpty(model.SpareFileName))
                {
                    var fileSparePath = Path.Combine(Server.MapPath("~/UploadedExcelFiles/"), model.SpareFileName);
                    var dataSpareTable = _ticketModelBuilder.GetDataFromExcel(fileSparePath, model.Version);

                    //loop through the excel sheet data
                    foreach (DataRow row in dataSpareTable.Rows)
                    {
                        if (string.IsNullOrEmpty(row[0].ToString()))
                            break;

                        var batchItem = _ticketModelBuilder.GetSparePart(row, ticketId, customerId);
                        await _sparePartRepository.InsertAndGetIdAsync(batchItem);
                    }

                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(fileSparePath)))
                    {
                        System.IO.File.Delete(fileSparePath);
                    }
                }   

                var path = Server.MapPath("~/" + attachmentPath1); 
                EmailService.EmailService.CreateTicket(customer.Email, customer.FirstName, "Ticket Creation", "Ticket No: "+ ticket.BatchTicketNo, path);
                return Json(new Abp.Web.Models.AjaxResponse { Result = ticketId });

            }
            catch (Exception ex)
            {
                await _ticketRepository.DeleteAsync(ticketId);
                return Json(new Abp.Web.Models.AjaxResponse { Result = ex.Message });
            }


        }

        /// <summary>
        ///  Upload excel file and store in the specific folder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    var fname = string.Empty;
                    var newFileName = string.Empty;
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                                                
                        if (!file.FileName.Contains(".xls"))
                        {
                            return Json("Only Excel file can be uploaded.");
                        } 
                         
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        
                        if (file.FileName.EndsWith(".xls"))
                        {
                             newFileName = file.FileName.Replace(".xls", "").Trim() + "-" + DateTime.Now.Ticks.ToString() + ".xls";
                        }

                        if (file.FileName.EndsWith(".xlsx"))
                        {
                             newFileName = file.FileName.Replace(".xlsx", "").Trim() + "-" + DateTime.Now.Ticks.ToString() + ".xlsx";
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadedExcelFiles/"), newFileName);
                        file.SaveAs(fname);
                    }

                    //return the file name
                    var result = new
                    {
                        FileName = newFileName
                    };

                    //Returns message that successfully uploaded                    
                    return Json(result,JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        
    }
}