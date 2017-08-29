using Abp.Web.Mvc.Views;

namespace Casentra.RMATicketing.Web.Views
{
    public abstract class RMATicketingWebViewPageBase : RMATicketingWebViewPageBase<dynamic>
    {

    }

    public abstract class RMATicketingWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected RMATicketingWebViewPageBase()
        {
            LocalizationSourceName = RMATicketingConsts.LocalizationSourceName;
        }
    }
}