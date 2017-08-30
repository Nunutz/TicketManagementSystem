using Abp.Domain.Repositories;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casentra.RMATicketing.Web.ViewModelBuilder
{
    public class TicketModelBuilder
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Customer> _customerRepository;   
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PhoneProblem> _problemRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IRepository<BoughtAt> _boughtAtRepository;
        private readonly IRepository<Spare> _spareRepository;

        public TicketModelBuilder(IRepository<Ticket> ticketRepository, IRepository<Customer> customerRepository,
           
             IRepository<Brand> brandRepository,
            IRepository<Product> productRepository,
            IRepository<PhoneProblem> problemRepository,
              IRepository<Accessory> accessoryRepository,
             IRepository<BoughtAt> boughtAtRepository,
             IRepository<Spare> spareRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _accessoryRepository = accessoryRepository;
            _boughtAtRepository = boughtAtRepository;
            _spareRepository = spareRepository;
            _problemRepository = problemRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// load drop down list french data
        /// </summary>
        /// <param name="model"></param>
        public void LoadLookUps(TicketModel model,bool IsPrivate)
        {
            foreach (var value in Enum.GetValues(typeof(AdminTicketStatus)))
            {
                model.TicketStatus.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<AdminTicketStatus>.GetDisplayValue((AdminTicketStatus)value)
                });
            }


            foreach (var value in Enum.GetValues(typeof(TicketPriority)))
            {
                model.TicketPriority.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<TicketPriority>.GetDisplayValue((TicketPriority)value)
                });
            }

            foreach (var value in Enum.GetValues(typeof(TicketBoard)))
            {
                model.TicketBoard.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<TicketBoard>.GetDisplayValue((TicketBoard)value)
                });
            }


            foreach (var value in Enum.GetValues(typeof(ColorList)))
            {
                model.ProductColors.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<ColorList>.GetDisplayValue((ColorList)value)
                });
            }
            model.ProductColors.OrderBy(q => q.Text);

            foreach (var value in Enum.GetValues(typeof(CapacityList)))
            {
                model.Capacities.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<CapacityList>.GetDisplayValue((CapacityList)value)
                });
            }
            model.Capacities.OrderBy(q => q.Text);


            foreach (var value in Enum.GetValues(typeof(ConditionList)))
            {
                model.PhoneConditions.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<ConditionList>.GetDisplayValue((ConditionList)value)
                });
            }

            model.PhoneConditions.OrderBy(q => q.Text);

            model.Brands = (from b in _brandRepository.GetAll()
                            select new SelectListItem
                            {
                                Value = b.Id.ToString(),
                                Text = b.Name
                            }).OrderBy(q => q.Text).ToList();

            model.Products = (from p in _productRepository.GetAll()

                              select new SelectListItem
                              {
                                  Value = p.Id.ToString(),
                                  Text = p.Name,

                              }).OrderBy(q => q.Text).ToList();

            model.PhoneProblems = (from b in _problemRepository.GetAll()
                                   select new SelectListItem
                                   {
                                       Value = b.Id.ToString(),
                                       Text = b.FrenchName,

                                   }).OrderBy(q => q.Text).ToList();

            if(IsPrivate)
            {
                model.BoughtAts = (from b in _boughtAtRepository.GetAll()
                                   select new SelectListItem
                                   {
                                       Value = b.Id.ToString(),
                                       Text = b.FrenchName
                                   }).OrderBy(q => q.Text).ToList();
            }

            else
            {
                foreach (var value in Enum.GetValues(typeof(EngBoughtAtList)))
                {
                    model.BoughtAts.Add(new SelectListItem
                    {
                        Value = Convert.ToString((int)value),
                        Text = EnumHelper<EngBoughtAtList>.GetDisplayValue((EngBoughtAtList)value)
                    });
                }
            }


            model.Accesseries = (from b in _accessoryRepository.GetAll()
                                 select new SelectListItem
                                 {
                                     Value = b.Id.ToString(),
                                     Text = b.FrenchName
                                 }).OrderBy(q => q.Text).ToList();

        }

        /// <summary>
        /// load dropdown list english data
        /// </summary>
        /// <param name="model"></param>
        public void EngLoadLookUps(TicketModel model,bool isPrivate)
        {
            foreach (var value in Enum.GetValues(typeof(AdminTicketStatus)))
            {
                model.TicketStatus.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<AdminTicketStatus>.GetDisplayValue((AdminTicketStatus)value)
                });
            }
            
            foreach (var value in Enum.GetValues(typeof(TicketPriority)))
            {
                model.TicketPriority.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<TicketPriority>.GetDisplayValue((TicketPriority)value)
                });
            }

            foreach (var value in Enum.GetValues(typeof(TicketBoard)))
            {
                model.TicketBoard.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<TicketBoard>.GetDisplayValue((TicketBoard)value)
                });
            }
            

            foreach (var value in Enum.GetValues(typeof(EngColorList)))
            {
                model.ProductColors.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<EngColorList>.GetDisplayValue((EngColorList)value)
                });
            }
            model.ProductColors.OrderBy(q => q.Text);
            foreach (var value in Enum.GetValues(typeof(EngCapacityList)))
            {
                model.Capacities.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<EngCapacityList>.GetDisplayValue((EngCapacityList)value)
                });
            }

            model.Capacities.OrderBy(q => q.Text);

            foreach (var value in Enum.GetValues(typeof(EngConditionList)))
            {
                model.PhoneConditions.Add(new SelectListItem
                {
                    Value = Convert.ToString((int)value),
                    Text = EnumHelper<EngConditionList>.GetDisplayValue((EngConditionList)value)
                });

            }

            model.PhoneConditions.OrderBy(q => q.Text);

            model.Brands = (from b in _brandRepository.GetAll()
                            select new SelectListItem
                            {
                                Value = b.Id.ToString(),
                                Text = b.Name
                            }).OrderBy(q => q.Text).ToList();

            model.Products = (from p in _productRepository.GetAll()

                              select new SelectListItem
                              {
                                  Value = p.Id.ToString(),
                                  Text = p.Name,
                              }).OrderBy(q => q.Text).ToList();

            model.PhoneProblems = (from b in _problemRepository.GetAll()
                                   select new SelectListItem
                                   {
                                       Value = b.Id.ToString(),
                                       Text = b.Name,
                                   }).OrderBy(q => q.Text).ToList();
            if(isPrivate)
            model.BoughtAts = (from b in _boughtAtRepository.GetAll()
                               select new SelectListItem
                               {
                                   Value = b.Id.ToString(),
                                   Text = b.Name
                               }).OrderBy(q => q.Text).ToList();
            else
            {
                foreach (var value in Enum.GetValues(typeof(EngBoughtAtList)))
                {
                    model.BoughtAts.Add(new SelectListItem
                    {
                        Value = Convert.ToString((int)value),
                        Text = EnumHelper<EngBoughtAtList>.GetDisplayValue((EngBoughtAtList)value)
                    });
                }
            }

            model.Accesseries = (from b in _accessoryRepository.GetAll()
                                 select new SelectListItem
                                 {
                                     Value = b.Id.ToString(),
                                     Text = b.Name
                                 }).OrderBy(q => q.Text).ToList();

        }

        public Ticket GetTicket(TicketModel model)
        {
            model.TicketBoardId = 1;
            model.TicketPriorityId = 2;
            model.TicketStatusId = 0;
          
            var ticket = new Ticket
            {
                Summary = model.IssueSummary,
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
                CreatedDate = DateTime.Now,
                ClosedDate = null,
                Accessories = model.Accessories,
                PhoneProblems = model.Problems
            };

            var problemsInFrench = string.Empty;
            var problemsInChinese = string.Empty;

            if(string.IsNullOrEmpty(model.ProblemIds))
                return ticket;

            var splitProblemIds = model.ProblemIds.Split(',');
            foreach (var item in splitProblemIds)
            {
                if (string.IsNullOrEmpty(item))
                    break;

                var id = Convert.ToInt32(item);
                var prob = _problemRepository.GetAll().Where(q => q.Id == id).FirstOrDefault();

                problemsInFrench += prob.FrenchName;
                problemsInChinese += prob.ChinesName;
            }

            ticket.PhoneProblemsInFrench = problemsInFrench;
            ticket.PhoneProblemsInChinese = problemsInChinese;

            return ticket;
        }
        public Customer GetCustomer(TicketModel model)
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
                IsProfessional = model.IsProfessional,
                IsTrading = false,
                Street = string.Empty,

            };
            return customer;
        }
       
        public string TicketNo(int id)
        {
            return string.Format("T{0}", id + 1000);
        }

        public string TicketDetail(TicketModel model, string ticketNo)
        {
            var brand = _brandRepository.GetAll().Where(q => q.Id == model.BrandId).FirstOrDefault();
            var product = _productRepository.GetAll().Where(q => q.Id == model.ProductId).FirstOrDefault();

            var strTble = "<table style='width:600px'>";

            strTble += "<tr>";
            strTble += "<td style='width:200px'>";
            strTble += "Ticket No ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += ticketNo;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Prénom ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.FirstName;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Adresse email ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.Email;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Numero de téléphone ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.MobileNumber;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Marque ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += brand.Name;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Modèle ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += product.Name;
            strTble += "</td>";
            strTble += "</tr>";


            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Date d'achat ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.PurchasedDate.ToShortDateString();
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Numero IMEI ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.IMEINumber;
            strTble += "</td>";
            strTble += "</tr>";


            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Accessoires fournis ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.Accessories;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Probleme rencontré ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.Problems;
            strTble += "</td>";
            strTble += "</tr>";


            strTble += "<tr>";
            strTble += "<td>";
            strTble += "Détails ";
            strTble += "</td>";
            strTble += "<td>";
            strTble += model.IssueSummary;
            strTble += "</td>";
            strTble += "</tr>";

            strTble += "</table>";
            return strTble;

        }

        public TicketModel GetPrivateTicketModel()
        {
            var model = new TicketModel
            {
                TicketStatus = new List<SelectListItem>(),
                Accesseries = new List<SelectListItem>(),
                BoughtAts = new List<SelectListItem>(),
                Capacities = new List<SelectListItem>(),
                Products = new List<SelectListItem>(),
                PhoneProblems = new List<SelectListItem>(),
                Brands = new List<SelectListItem>(),
                PhoneConditions = new List<SelectListItem>(),
                ProductColors = new List<SelectListItem>(),
                TicketBoard = new List<SelectListItem>(),
                TicketPriority = new List<SelectListItem>(),

            };
            return model;
        }

       
    }
}