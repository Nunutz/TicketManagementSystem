using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.PhoneProblems.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.PhoneProblems
{
    public interface IPhoneProblemAppService
    {
        Task<ListResultDto<PhoneProblemListDto>> GetAllTicketsAsync();
    }

    public class PhoneProblemAppService: RMATicketingAppServiceBase, IPhoneProblemAppService
    {
        private readonly IRepository<PhoneProblem> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public PhoneProblemAppService(IRepository<PhoneProblem> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<PhoneProblemListDto>> GetAllTicketsAsync()
        {
            var accesseries = await _repository.GetAllListAsync();
            return new ListResultDto<PhoneProblemListDto>(accesseries.OrderBy(o => o.Name).MapTo<List<PhoneProblemListDto>>());
        }

    }
}
