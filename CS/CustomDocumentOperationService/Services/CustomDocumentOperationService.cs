using System.IO;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;

namespace DocumentOperationServiceSample.Services
{
    public class CustomDocumentOperationService : DocumentOperationService {
        public override bool CanPerformOperation(DocumentOperationRequest request)
        {
            return true;
        }

        public override DocumentOperationResponse PerformOperation(DocumentOperationRequest request, PrintingSystemBase initialPrintingSystem, PrintingSystemBase printingSystemWithEditingFields)
        {
            using (var stream = new MemoryStream()) {
                printingSystemWithEditingFields.ExportToPdf(stream);
                stream.Position = 0;
                var mailAddress = new MailAddress(request.CustomData);
                var recipients = new MailAddressCollection() { mailAddress };
                var attachment = new Attachment(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
                return SendEmail(recipients, "Enter_Mail_Subject", "Enter_Message_Body", attachment);
            }
        }

        DocumentOperationResponse SendEmail(MailAddressCollection recipients, string subject, string messageBody, Attachment attachment) {
            string SmtpHost = null;
            int SmtpPort = -1;
            if (string.IsNullOrEmpty(SmtpHost) || SmtpPort == -1) {
                return new DocumentOperationResponse { Message = "Please configure the SMTP server settings." };
            }

            string SmtpUserName = "Enter_Sender_User_Account";
            string SmtpUserPassword = "Enter_Sender_Password";
            string SmtpFrom = "Enter_Sender_Address";
            string SmtpFromDisplayName = "Enter_Sender_Display_Name";
            using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                if (!string.IsNullOrEmpty(SmtpUserName))
                {
                    smtpClient.Credentials = new NetworkCredential(SmtpUserName, SmtpUserPassword);
                }

                using (var message = new MailMessage())
                {
                    message.Subject = subject.Replace("\r", "").Replace("\n", "");
                    message.IsBodyHtml = true;
                    message.Body = messageBody;
                    message.From = new MailAddress(SmtpFrom, SmtpFromDisplayName);

                    foreach (var item in recipients)
                    {
                        message.To.Add(item);
                    }

                    try
                    {
                        if (attachment != null)
                        {
                            message.Attachments.Add(attachment);
                        }
                        smtpClient.Send(message);
                        return new DocumentOperationResponse
                        {
                            Succeeded = true,
                            Message = "Mail was sent successfully"
                        };
                    }
                    catch (SmtpException e)
                    {
                        return new DocumentOperationResponse
                        {
                            Message = "Sending an email message failed."
                        };
                    }
                    finally
                    {
                        message.Attachments.Clear();
                    }
                }
            }
        }

        protected string RemoveNewLineSymbols(string value)
        {
            return value;
        }
    }
}