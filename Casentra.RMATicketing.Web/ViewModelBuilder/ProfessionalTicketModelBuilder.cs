using Abp.Domain.Repositories;
using Casentra.RMATicketing.BatchItems;
using Casentra.RMATicketing.BatchTickets;
using Casentra.RMATicketing.Customers;
using Casentra.RMATicketing.Enums;
using Casentra.RMATicketing.IMEI;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Web.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.ViewModelBuilder
{

    public class ProfessionalTicketModelBuilder
    {
        private readonly IRepository<Spare> _spareRepository;
        private readonly IRepository<BatchTicket> _ticketRepository;
        private readonly IRepository<BatchItem> _batchItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<SparePart> _sparePartRepository;
        private readonly IRepository<PhoneProblem> _phoneProblemRepository;
        private readonly IRepository<IMEINumber> _imeiRepository;


        public ProfessionalTicketModelBuilder(IRepository<Spare> spareRepository,
            IRepository<BatchTicket> ticketRepository,
            IRepository<BatchItem> batchItemRepository,
            IRepository<Customer> customerRepository,
            IRepository<SparePart> sparePartRepository,
            IRepository<PhoneProblem> phoneProblemRepository,
            IRepository<IMEINumber> imeiRepository)
        {
            _spareRepository = spareRepository;
            _ticketRepository = ticketRepository;
            _batchItemRepository = batchItemRepository;
            _customerRepository = customerRepository;
            _sparePartRepository = sparePartRepository;
            _imeiRepository = imeiRepository;
            _phoneProblemRepository = phoneProblemRepository;
        }

        public Customer GetCustomer(TicketProfModel model)
        {
            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                City = model.City,
                Zipcode = model.Zipcode,
                MobileNumber = model.MobileNumber,
                //IsProfessional = true,
                Street = string.Empty,
                IsProfessional= true,
                IsTrading=true

            };
            return customer;
        }

        public TicketProfModel GetProfessionalTicketModel()
        {
            var model = new TicketProfModel
            {
                SpareList = new List<SelectListItem>(),
                SelectedSpares = new List<SpareList>(),

            };
            return model;
        }

        public void LoadProfessionalLookUps(TicketProfModel model)
        {
            model.SpareList = (from b in _spareRepository.GetAll()
                               select new SelectListItem
                               {
                                   Value = b.Id.ToString(),
                                   Text = b.FrenchName
                               }).OrderBy(q => q.Text).ToList();

            model.SelectedSpares = new List<SpareList>();

        }

        public bool IsExist(string imei)
        {
            var exist= (from i in _imeiRepository.GetAll()
                    where i.IMEINo== imei
                    select i).Any();
           
            return exist;

        }
        public void LoadProfessionalEngLookUps(TicketProfModel model)
        {

            model.SpareList = (from b in _spareRepository.GetAll()
                               select new SelectListItem
                               {
                                   Value = b.Id.ToString(),
                                   Text = b.Name
                               }).OrderBy(q => q.Text).ToList();

            model.SelectedSpares = new List<SpareList>();

        }

        /// <summary>
        ///  Get the excel sheet data as data table
        /// </summary>
        /// <param name="targetpath"> File location</param>
        /// <param name="version"> English /French</param>
        /// <returns></returns>
        public DataTable GetDataFromExcel(string targetpath, string version)
        {
            var dtable = new DataTable();
            var connectionString = "";
            if (targetpath.EndsWith(".xls"))
            {
                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;OLE DB Services=-4; data source={0}; Extended Properties=Excel 8.0;", targetpath);
            }
            else if (targetpath.EndsWith(".xlsx"))
            {
                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;OLE DB Services=-4;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", targetpath);
            }

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + version + "$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "ExcelTable");

            dtable = ds.Tables["ExcelTable"];
            return dtable;
        }

        /// <summary>
        /// Get Batch Ticket
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public BatchTicket GetBatchTicket(int customerId)
        {
            var ticket = new BatchTicket {
                CustomerId = customerId,
                ClosedDate = null,
                CreatedDate = System.DateTime.Now,
                TicketBoard = TicketBoard.France,
                BatchTicketNo = string.Empty
            };

            return ticket;
        }

        /// <summary>
        /// Get Batch Item
        /// </summary>
        /// <param name="row"></param>
        /// <param name="batchTicketId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public BatchItem GetBatchItem(DataRow row, int batchTicketId, int customerId)
        {
            // map the batch items
            var item = new BatchItem
            {
                BatchTicketId = batchTicketId,
                CustomerId = customerId,
                PurchasedDate = Convert.ToDateTime(row[0].ToString()),

                BoughtAt = row[1].ToString(),
                Brand = row[2].ToString(),
                Product = row[3].ToString(),
                ProductColor = row[4].ToString(),
                Capacity = row[5].ToString(),
                IMEINumber = row[6].ToString(),
                Password = row[7].ToString(),

                IcloudAddress = row[8].ToString(),
                IcloudPassword = row[9].ToString(),
                Accessories = row[10].ToString(),
                PhoneCondition = row[11].ToString(),

                PhoneProblem = row[12].ToString() ,
                PhoneProblemsInFrench = row[13].ToString(),
                PhoneProblemsInChinese = row[14].ToString(),
                IssueDetail = row[15].ToString(),
                PhoneProblemId = 0,
                TicketPriority = TicketPriority.Normal,
                TicketStatus = 0,
                TicketBoard = TicketBoard.France
            };

            return GetUpdateBatchItem(item, row[12].ToString(), row[13].ToString(), row[14].ToString());

        }
        public SparePart GetSparePart(DataRow row, int batchTicketId, int customerId)
        {           
            // map the spare parts
            var item = new SparePart
            {
                BatchTicketId = batchTicketId,
                CustomerId = customerId,
                Model = row[0].ToString(),
                SpareName = row[1].ToString(),
                Quantity = Convert.ToInt32(row[2].ToString()),
                IsDelivered = false,
                CreatedDate=DateTime.Now,

            };

            return item;

        }

        public IMEINumber GetIMEINumber(DataRow row)
        {
            var item = new IMEINumber
            {
                IMEINo = row[0].ToString(),
                Model = row[1].ToString(),
               
            };

            if (string.IsNullOrEmpty(row[2].ToString()))
               item.PurchasedDate = null;
            else
                item.PurchasedDate = Convert.ToDateTime(row[2].ToString());

            return item;

        }
        /// <summary>
        ///  get ticket number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string TicketNo(int id)
        {
            return string.Format("BT{0}", id + 1000);
        }

        private BatchItem GetUpdateBatchItem(BatchItem item,string problem1,string problem2,string problem3)
        {
            if (string.IsNullOrEmpty(problem1))
                return item;

            var problem = (from b in _phoneProblemRepository.GetAll()
                           where b.Name == problem1 || b.FrenchName == problem1 || b.Name.Contains(problem1)
                           select b).FirstOrDefault();

            if (problem == null)
                return item;


            item.PhoneProblem = problem.Name;
            item.PhoneProblemsInFrench = problem.FrenchName;
            item.PhoneProblemsInChinese = problem.ChinesName;

            if (string.IsNullOrEmpty(problem2))
                return item;

            var prob2 = (from b in _phoneProblemRepository.GetAll()
                           where b.Name == problem2 || b.FrenchName == problem2 || b.Name.Contains(problem2)
                           select b).FirstOrDefault();

            if (prob2 == null)
                return item;


            item.PhoneProblem += ","+ prob2.Name;
            item.PhoneProblemsInFrench += "," + prob2.FrenchName;
            item.PhoneProblemsInChinese += "," + prob2.ChinesName;


            if (string.IsNullOrEmpty(problem3))
                return item;

            var prob3 = (from b in _phoneProblemRepository.GetAll()
                         where b.Name == problem3 || b.FrenchName == problem3 || b.Name.Contains(problem3)
                         select b).FirstOrDefault();

            if (prob3 == null)
                return item;


            item.PhoneProblem += "," + prob3.Name;
            item.PhoneProblemsInFrench += "," + prob3.FrenchName;
            item.PhoneProblemsInChinese += "," + prob3.ChinesName;

            return item;
        }

        private BatchItem GetUpdateBatchItem(BatchItem item)
        {
            var problem=(from b in _phoneProblemRepository.GetAll()
                         where b.Name== item.PhoneProblem || b.FrenchName == item.PhoneProblem ||b.Name.Contains(item.PhoneProblem)
                         select b ).FirstOrDefault();

            if(problem==null)
                return item;

            item.PhoneProblem = problem.Name;
            item.PhoneProblemsInFrench = problem.FrenchName;
            item.PhoneProblemsInChinese = problem.ChinesName;


            return item;
        }
    }
}