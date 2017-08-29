using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Casentra.RMATicketing.AppModel;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.BatchTickets
{
    public interface IBatchTicketAppService: IApplicationService
    {
        object GetBatchTicketsArray();
        object GetBatchTicketObject(NullableIdDto<int> input);
        object GetTicketObject(NullableIdDto<int> input);
        object GetLookups();
        Task<int> UpdateTicket(TicketCustomerModel input);

        object GetSparePart(NullableIdDto<int> input);

        Task<int> UpdateSpare(SparePartModel input);
    }
}
