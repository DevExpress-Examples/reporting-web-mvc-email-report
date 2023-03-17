Imports System
Imports System.IO
Imports DevExpress.DocumentServices.ServiceModel.DataContracts
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts
Imports MailKit.Security

Namespace DocumentOperationServiceSample.Services
	Public Class CustomDocumentOperationService
		Inherits DocumentOperationService

		Public Overrides Function CanPerformOperation(ByVal request As DocumentOperationRequest) As Boolean
			Return True
		End Function

		Public Overrides Function PerformOperation(ByVal request As DocumentOperationRequest, ByVal initialPrintingSystem As PrintingSystemBase, ByVal printingSystemWithEditingFields As PrintingSystemBase) As DocumentOperationResponse
			Using stream = New MemoryStream()
				Dim mMessage As System.Net.Mail.MailMessage = printingSystemWithEditingFields.ExportToMail(request.CustomData, "john.doe@test.com", "John Doe")
				mMessage.Subject = "Test"

				' Create a new attachment and add the PDF document.
				printingSystemWithEditingFields.ExportToPdf(stream)
				stream.Seek(0, System.IO.SeekOrigin.Begin)
				Dim attachedDoc As New System.Net.Mail.Attachment(stream, "TestReport.pdf", "application/pdf")
				mMessage.Attachments.Add(attachedDoc)

				Dim message = CType(mMessage, MimeKit.MimeMessage)

				Dim smtpHost As String = "localhost"
				Dim smtpPort As Integer = 25


				Using message
					Using client = New MailKit.Net.Smtp.SmtpClient()
						Try
							client.Connect(smtpHost, smtpPort, SecureSocketOptions.Auto)
							'client.Authenticate(userName, password);
							client.Send(message)
							client.Disconnect(True)
							Return New DocumentOperationResponse With {
								.Succeeded = True,
								.Message = "Mail was sent successfully",
								.DocumentId = printingSystemWithEditingFields.Document.Name
							}
						Catch ex As Exception
							Return New DocumentOperationResponse With {
								.Message = ex.Message,
								.Succeeded = False
							}
						End Try
					End Using
				End Using
			End Using
		End Function

		Protected Function RemoveNewLineSymbols(ByVal value As String) As String
			Return value
		End Function
	End Class
End Namespace
