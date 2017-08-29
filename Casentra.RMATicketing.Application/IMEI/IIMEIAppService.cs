using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Casentra.RMATicketing.EntityFramework;
using Casentra.RMATicketing.IMEI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.IMEI
{
    public interface IImeiNumberAppService : IApplicationService
    {
        Task<ListResultDto<IMEIListDto>> GetAllImeiAsync();
        Task<string> GetImeiAsync(string imei);
        object GetImeiNumberArray();
    }
    public class ImeiNumberAppService : RMATicketingAppServiceBase, IImeiNumberAppService
    {
        private readonly IRepository<IMEINumber> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RMATicketingDbContext _context;

        public ImeiNumberAppService(IRepository<IMEINumber> repository, IUnitOfWorkManager unitOfWorkManager, RMATicketingDbContext context)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _context = context;
        }
        public async Task<ListResultDto<IMEIListDto>> GetAllImeiAsync()
        {
            var spares = await _repository.GetAllListAsync();
            return new ListResultDto<IMEIListDto>(spares.OrderBy(o => o.Model).MapTo<List<IMEIListDto>>());
        }


        public async Task<string> GetImeiAsync(string imei)
        {
            var spares = await _repository.GetAllListAsync();
            var imeiFound=spares.Where(x => x.IMEINo.Contains(imei)).FirstOrDefault();
            if (imeiFound == null)
                return null;
            else
                return imeiFound.IMEINo;
        }
        public object GetImeiNumberArray()
        {
            var iMeiNumbers =  _repository.GetAll();
            var imeiObj = (from i in iMeiNumbers
                             select new
                             {
                                 imeiNumber = i.IMEINo,
                                 Product = i.Model,
                                 //PurchasedDate = i.PurchasedDate==null?"-": (DateTime)i.PurchasedDate,
                                 PurchasedDate=""

                             }).ToArray();

            return new
            {
                imeiData = imeiObj
            };
        }
    }
}
