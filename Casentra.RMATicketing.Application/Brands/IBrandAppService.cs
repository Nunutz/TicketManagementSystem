using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Brands.Dto;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Brands
{
    public interface IBrandAppService
    {
        Task<ListResultDto<BrandListDto>> GetAllTicketsAsync();
    }

    public class BrandAppService: RMATicketingAppServiceBase, IBrandAppService
    {
        private readonly IRepository<Brand> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public BrandAppService(IRepository<Brand> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<BrandListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<BrandListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<BrandListDto>>());
        }
    }
}
