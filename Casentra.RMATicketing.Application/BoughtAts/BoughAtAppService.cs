using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.BoughtAts.Dto;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BoughtAts
{
    public class BoughtAtAppService: RMATicketingAppServiceBase,IBoughtAtAppService
    {
        private readonly IRepository<BoughtAt> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public BoughtAtAppService(IRepository<BoughtAt> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<BoughtAtListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<BoughtAtListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<BoughtAtListDto>>());
        }
    }
}
