using Casentra.RMATicketing.EmailService.Emails;
using System;
using System.Collections.Specialized;
using System.Net.Mail;


namespace Casentra.RMATicketing.EmailService
{
    public static class EmailService
    {
        
        #region Public Methods
        public static void Send(MailMessage message, bool isSent)
        {
            isSent = true;
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(message);
                }
                
            }
            catch (SmtpException ex)
            {
                isSent = false;
            }
        }

        public static void SendEmail(string from,string to, string subject,string body,string attachmentPath)
        {
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = body
            };

            if(!string.IsNullOrEmpty(attachmentPath))
            {
                var attachment = new Attachment(attachmentPath);
                message.Attachments.Add(attachment);
            }
            
            Send(message, false);
            
        }
        public static void SendEmail(string from, string to, string subject, string body)
        {
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = body
            };
                        
            Send(message, false);

        }

        public static void CreateTicket(string ownerEmail, string ownerName, string subject, string note,string attachmentPath)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new CreateTicket
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText(), attachmentPath);
        }
        public static void ReceptionsConfirmationInFR(string ownerEmail,string ownerName,string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ReceptionsConfirmationInFR
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from,ownerEmail,subject, mailTemplate.TransformText());
        }

        public static void ReparationInProgress(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ReparationInProgress
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void ReparationInFrance(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ReparationInFrance
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void ShippedFromFrance(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ShippedFromFrance
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void NeedToShipToChina(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new NeedToShipToChina
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void ReparationInChina(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ReparationInChina
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void ShippedFromChina(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new ShippedFromChina
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }

        public static void QuotationRequest(string ownerEmail, string ownerName, string subject, string note)
        {
            var from = "support@suivi-rma.com";
            var mailTemplate = new QuotationRequest
            {
                OwnerName = ownerName,
                Note = note
            };

            SendEmail(from, ownerEmail, subject, mailTemplate.TransformText());
        }
        #endregion
    }
}