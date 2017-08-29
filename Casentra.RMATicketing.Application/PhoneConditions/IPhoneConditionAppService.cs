using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.PhoneConditions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.PhoneConditions
{
    public interface IPhoneConditionAppService
    {
        Task<ListResultDto<PhoneConditionListDto>> GetAllTicketsAsync();
    }

    public class PhoneConditionAppService: RMATicketingAppServiceBase,IPhoneConditionAppService
    {
        private readonly IRepository<PhoneCondition> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public PhoneConditionAppService(IRepository<PhoneCondition> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<PhoneConditionListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<PhoneConditionListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<PhoneConditionListDto>>());
        }
    }
}
