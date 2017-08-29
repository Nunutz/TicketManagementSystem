using Abp.Application.Services.Dto;
using Casentra.RMATicketing.BoughtAts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BoughtAts
{
    public interface IBoughtAtAppService
    {
        Task<ListResultDto<BoughtAtListDto>> GetAllTicketsAsync();
    }
}
