using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.ProductNames.Dto;
using Casentra.RMATicketing.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.ProductNames
{
    public interface IProductAppService
    {
        Task<ListResultDto<ProductNameListDto>> GetAllTicketsAsync();
    }
    public class ProductAppService: RMATicketingAppServiceBase,IProductAppService
    {
        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public ProductAppService(IRepository<Product> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<ProductNameListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<ProductNameListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<ProductNameListDto>>());
        }
    }
}
