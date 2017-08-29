using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.Spares.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Spares
{
    public interface ISpareAppService: IApplicationService
    {
        Task<ListResultDto<SpareListDto>> GetAllSparesAsync();
    }
    public class SpareAppService : RMATicketingAppServiceBase, ISpareAppService
    {
        private readonly IRepository<Spare> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public SpareAppService(IRepository<Spare> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<SpareListDto>> GetAllSparesAsync()
        {
            var spares = await _repository.GetAllListAsync();
            return new ListResultDto<SpareListDto>(spares.OrderBy(o => o.Name).MapTo<List<SpareListDto>>());
        }
    }
}
