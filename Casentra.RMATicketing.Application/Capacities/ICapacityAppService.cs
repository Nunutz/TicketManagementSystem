using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Capacities.Dto;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Capacities
{
    public interface ICapacityAppService
    {
        Task<ListResultDto<CapacityListDto>> GetAllTicketsAsync();
    }

    public class CapacityAppService: ICapacityAppService
    {
        private readonly IRepository<Capacity> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public CapacityAppService(IRepository<Capacity> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<CapacityListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<CapacityListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<CapacityListDto>>());
        }
    }
}
