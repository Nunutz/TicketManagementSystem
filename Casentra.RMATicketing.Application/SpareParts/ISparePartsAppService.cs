using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.SpareParts.Dto;
using Casentra.RMATicketing.Spares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.SpareParts
{
    public interface ISparePartsAppService: IApplicationService
    {
        Task<ListResultDto<SparePartListDto>> GetAllSparePartsAsync();
    }
    public class SparePartsAppService : RMATicketingAppServiceBase, ISparePartsAppService
    {
        private readonly IRepository<SparePart> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public SparePartsAppService(IRepository<SparePart> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<SparePartListDto>> GetAllSparePartsAsync()
        {
            var spares = await _repository.GetAllListAsync();
            return new ListResultDto<SparePartListDto>(spares.OrderBy(o => o.Model).MapTo<List<SparePartListDto>>());
        }
    }
}
