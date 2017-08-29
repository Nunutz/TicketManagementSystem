using Abp.Application.Services;
using Casentra.RMATicketing.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Report
{
    public interface  IReportAppService : IApplicationService
    {
        object GetBatchTicketCountByStatus();
        object GetTicketCountByStatus();
        object GetSearchObjects();
        object GetBatchTicketsArray(SearchModel search);
        object GetTicketsArray(SearchModel search);
   

    }
}
