using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Colors;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.ProductColors.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.ProductColors
{
    public interface IProductColorAppService
    {
        Task<ListResultDto<ProductColorListDto>> GetAllTicketsAsync();
    }
    public class ProductColorAppService: RMATicketingAppServiceBase,IProductColorAppService
    {
        private readonly IRepository<Color> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public ProductColorAppService(IRepository<Color> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<ProductColorListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<ProductColorListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<ProductColorListDto>>());
        }
    }
}
