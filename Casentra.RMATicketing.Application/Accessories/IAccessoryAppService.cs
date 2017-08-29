using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.Accessories.Dto;
using Casentra.RMATicketing.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Accessories
{
    public interface IAccessoryAppService
    {
        Task<ListResultDto<AccessoryListDto>> GetAllTicketsAsync();
    }

    public class AccessoryAppService: RMATicketingAppServiceBase,IAccessoryAppService
    {
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public AccessoryAppService(IRepository<Accessory> accessoryRepository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _accessoryRepository = accessoryRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<AccessoryListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _accessoryRepository.GetAllListAsync();
            return new ListResultDto<AccessoryListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<AccessoryListDto>>());
        }

    }
}
