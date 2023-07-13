using System;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts;
using MailKit.Security;

namespace DocumentOperationServiceSample.Services {
    public class CustomDocumentOperationService : DocumentOperationService {
        public override bool CanPerformOperation(DocumentOperationRequest request) {
            return true;
        }

        public override DocumentOperationResponse PerformOperation(DocumentOperationRequest request, 
            PrintingSystemBase initialPrintingSystem, 
            PrintingSystemBase printingSystemWithEditingFields) {
            using (var stream = new MemoryStream()) {
                System.Net.Mail.MailMessage mMessage = 
                    printingSystemWithEditingFields.ExportToMail(request.CustomData,
                    "john.doe@test.com",
                    "John Doe");
                mMessage.Subject = "Test";

                // Create a new attachment and add the PDF document.
                printingSystemWithEditingFields.ExportToPdf(stream);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                System.Net.Mail.Attachment attachedDoc = 
                    new System.Net.Mail.Attachment(stream, "TestReport.pdf", "application/pdf");
                mMessage.Attachments.Add(attachedDoc);

                var message = (MimeKit.MimeMessage)mMessage;

                string smtpHost = "localhost";
                int smtpPort = 25;


                using (message) {
                    using (var client = new MailKit.Net.Smtp.SmtpClient()) {
                        try {
                            client.Connect(smtpHost, smtpPort, SecureSocketOptions.Auto);
                            //client.Authenticate(userName, password);
                            client.Send(message);
                            client.Disconnect(true);
                            return new DocumentOperationResponse {
                                Succeeded = true,
                                Message = "Mail was sent successfully",
                                DocumentId = printingSystemWithEditingFields.Document.Name
                            };
                        }
                        catch (Exception ex) {
                            return new DocumentOperationResponse {
                                Message = ex.Message, 
                                Succeeded = false,
                            };
                        }
                    }
                }
            }
        }

        protected string RemoveNewLineSymbols(string value) {
            return value;
        }
    }
}
