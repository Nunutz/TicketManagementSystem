using Abp.Application.Services.Dto;
using Casentra.RMATicketing.Customers.Dto;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Customers
{
    public interface ICustomerAppService
    {
        Task<ListResultDto<CustomerListDto>> GetAllTicketsAsync();
        Task<CustomerListDto> GetTicketForEditAsync(NullableIdDto<int> input);
        Task<int> CreateOrEditTicketAsync(CreateCustomerInput input);

    }
}
