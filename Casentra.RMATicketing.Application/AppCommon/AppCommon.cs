using Casentra.RMATicketing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casentra.RMATicketing.Common
{
    public static class AppCommon
    {
        public static void SendEMail(int emailType, string ownerEmail, string owenrName, string note)
        {
            var subject = string.Empty;
            switch (emailType)
            {
                case (int)EmailTypes.ReceptionsConfirmationInFR:
                    subject = "Reception's confirmation in FR";
                    EmailService.EmailService.ReceptionsConfirmationInFR(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.ReparationInProgress:
                    subject = "Reparation in progress";
                    EmailService.EmailService.ReparationInProgress(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.ReparationInFrance:
                    subject = "Reparation in France";
                    EmailService.EmailService.ReparationInFrance(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.ShippedFromFrance:
                    subject = "Shipped from France";
                    EmailService.EmailService.ShippedFromFrance(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.NeedToShipToChina:
                    subject = "Need to ship in China";
                    EmailService.EmailService.NeedToShipToChina(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.ReparationInChina:
                    subject = "Reparation in China";
                    EmailService.EmailService.ReparationInChina(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.ShippedFromChina:
                    subject = "Shipped from China";
                    EmailService.EmailService.ShippedFromChina(ownerEmail, owenrName, subject, note);
                    break;
                case (int)EmailTypes.QuotationRequest:
                    subject = "Quotation request";
                    EmailService.EmailService.QuotationRequest(ownerEmail, owenrName, subject, note);
                    break;
                default:
                    break;
            }
        }
        public static string AdminStatusName(int statusId)
        {
            var status = string.Empty;
            switch (statusId)
            {
                case (int)EmailTypes.ReceptionsConfirmationInFR:
                    status = "Reception's confirmation in FR";
                    
                    break;
                case (int)EmailTypes.ReparationInProgress:
                    status = "Reparation in progress";
                    
                    break;
                case (int)EmailTypes.ReparationInFrance:
                    status = "Reparation in France";
                    
                    break;
                case (int)EmailTypes.ShippedFromFrance:
                    status = "Shipped from France";
                    
                    break;
                case (int)EmailTypes.NeedToShipToChina:
                    status = "Need to ship in China";
                    
                    break;
                case (int)EmailTypes.ReparationInChina:
                    status = "Reparation in China";
                   
                    break;
                case (int)EmailTypes.ShippedFromChina:
                    status = "Shipped from China";
                    
                    break;
                case (int)EmailTypes.QuotationRequest:
                    status = "Quotation request";
                   
                    break;
                default:
                    status = "-";
                    break;
            }

            return status;
        }



    }
}
