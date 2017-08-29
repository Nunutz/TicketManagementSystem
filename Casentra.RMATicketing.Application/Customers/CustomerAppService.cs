using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Customers.Dto;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Customers
{
    public class CustomerAppService : RMATicketingAppServiceBase, ICustomerAppService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public CustomerAppService(IRepository<Customer> customerRepository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _customerRepository = customerRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }

        public async Task<ListResultDto<CustomerListDto>> GetAllTicketsAsync()
        {
            var customers = await _customerRepository.GetAllListAsync();
            return new ListResultDto<CustomerListDto>(customers.OrderBy(o => o.Name).MapTo<List<CustomerListDto>>());
        }

        public async Task<CustomerListDto> GetTicketForEditAsync(NullableIdDto<int> input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
            var result = customer.MapTo<CustomerListDto>();
            return result;

        }

        public async Task<int> CreateOrEditTicketAsync(CreateCustomerInput input)
        {
            var customer = input.MapTo<Customer>();
            return await _customerRepository.InsertAndGetIdAsync(customer);

        }
    }
}
