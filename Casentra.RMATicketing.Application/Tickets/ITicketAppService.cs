using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Casentra.RMATicketing.Accessories;
using Casentra.RMATicketing.BoughtAts;
using Casentra.RMATicketing.Brands;
using Casentra.RMATicketing.PhoneProblems;
using Casentra.RMATicketing.Products;
using Casentra.RMATicketing.Spares;
using Casentra.RMATicketing.Tickets.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Tickets
{
    public interface ITicketAppService : IApplicationService
    {
        Task<ListResultDto<TicketListDto>> GetAllTicketsAsync();
        Task<TicketListDto> GetTicketForEditAsync(NullableIdDto<int> input);
        Task<int> CreateOrEditTicketAsync(CreateTicketInput input);
        object GetTicketsArray();
        object GetProfTicketsArray();
        object GetTicketObject(NullableIdDto<int> input);
        Task<int> UpdateTicket(TicketCustomerModel input);

        object GetDataEntries();
        object GetLookups();
        Task<int> CreateBrandAsync(Brand input);
        Task<int> CreateProductAsync(Product input);
        Task<int> CreateProblemAsync(PhoneProblem input);
        Task<int> CreateAccessoryAsync(Accessory input);
        Task<int> CreateBoughtAtAsync(BoughtAt input);
        Task<int> CreateSpareAsync(Spare input);

        //Update
        Task<int> UpdatePhoneProblemAsync(PhoneProblem input);
        Task<int> UpdateBrandAsync(Brand input);
        Task<int> UpdateProductAsync(Product input);
        Task<int> UpdateAccessoryAsync(Accessory input);
        Task<int> UpdateBoughtAtAsync(BoughtAt input);
        Task<int> UpdateSpareAsync(Spare input);


        //Delete
        void DeletePhoneProblemAsync(NullableIdDto<int> input);
        void DeleteBrandAsync(NullableIdDto<int> input);
        void DeleteProductAsync(NullableIdDto<int> input);
        void DeleteAccessoryAsync(NullableIdDto<int> input);
        void DeleteBoughtAtAsync(NullableIdDto<int> input);
        void DeleteSpareAsync(NullableIdDto<int> input);

    }
}
