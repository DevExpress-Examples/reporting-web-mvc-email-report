Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Web.WebDocumentViewer
Imports DevExpress.XtraReports.Web.WebDocumentViewer.DataContracts

Namespace DocumentOperationServiceSample.Services
    Public Class CustomDocumentOperationService
        Inherits DocumentOperationService

        Public Overrides Function CanPerformOperation(ByVal request As DocumentOperationRequest) As Boolean
            Return True
        End Function

        Public Overrides Function PerformOperation(ByVal request As DocumentOperationRequest, ByVal initialPrintingSystem As PrintingSystemBase, ByVal printingSystemWithEditingFields As PrintingSystemBase) As DocumentOperationResponse
            Using stream = New MemoryStream()
                printingSystemWithEditingFields.ExportToPdf(stream)
                stream.Position = 0
                Dim mailAddress = New MailAddress(request.CustomData)
                Dim recipients = New MailAddressCollection() From { mailAddress }
                Dim attachment = New Attachment(stream, System.Net.Mime.MediaTypeNames.Application.Pdf)
                Return SendEmail(recipients, "Enter_Mail_Subject", "Enter_Message_Body", attachment)
            End Using
        End Function

        Private Function SendEmail(ByVal recipients As MailAddressCollection, ByVal subject As String, ByVal messageBody As String, ByVal attachment As Attachment) As DocumentOperationResponse
            Dim SmtpHost As String = Nothing
            Dim SmtpPort As Integer = -1
            If String.IsNullOrEmpty(SmtpHost) OrElse SmtpPort = -1 Then
                Return New DocumentOperationResponse With {.Message = "Please configure the SMTP server settings."}
            End If

            Dim SmtpUserName As String = "Enter_Sender_User_Account"
            Dim SmtpUserPassword As String = "Enter_Sender_Password"
            Dim SmtpFrom As String = "Enter_Sender_Address"
            Dim SmtpFromDisplayName As String = "Enter_Sender_Display_Name"
            Using smtpClient = New SmtpClient(SmtpHost, SmtpPort)
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
                smtpClient.EnableSsl = True

                If Not String.IsNullOrEmpty(SmtpUserName) Then
                    smtpClient.Credentials = New NetworkCredential(SmtpUserName, SmtpUserPassword)
                End If

                Using message = New MailMessage()
                    message.Subject = subject.Replace(vbCr, "").Replace(vbLf, "")
                    message.IsBodyHtml = True
                    message.Body = messageBody
                    message.From = New MailAddress(SmtpFrom, SmtpFromDisplayName)

                    For Each item In recipients
                        message.To.Add(item)
                    Next item

                    Try
                        If attachment IsNot Nothing Then
                            message.Attachments.Add(attachment)
                        End If
                        smtpClient.Send(message)
                        Return New DocumentOperationResponse With { _
                            .Succeeded = True, _
                            .Message = "Mail was sent successfully" _
                        }
                    Catch e As SmtpException
                        Return New DocumentOperationResponse With {.Message = "Sending an email message failed."}
                    Finally
                        message.Attachments.Clear()
                    End Try
                End Using
            End Using
        End Function

        Protected Function RemoveNewLineSymbols(ByVal value As String) As String
            Return value
        End Function
    End Class
End Namespace