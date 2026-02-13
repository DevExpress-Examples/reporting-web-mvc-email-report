<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128596979/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T566760)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Global.asax.cs](./CS/CustomDocumentOperationService/Global.asax.cs) (VB: [Global.asax.vb](./VB/CustomDocumentOperationService/Global.asax.vb))
* **[CustomDocumentOperationService.cs](./CS/CustomDocumentOperationService/Services/CustomDocumentOperationService.cs) (VB: [CustomDocumentOperationService.vb](./VB/CustomDocumentOperationService/Services/CustomDocumentOperationService.vb))**
* [Viewer.cshtml](./CS/CustomDocumentOperationService/Views/Home/Viewer.cshtml)
<!-- default file list end -->
# Web Document Viewer - How to send a report via Email from the client side


This example demonstrates how to perform a custom operation with a Document Viewer's currently opened document. In particular, it shows how to send the document via Email.<br><br>On the server side, create a <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DocumentOperationService.class">DocumentOperationService</a> class descendant and override its <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DocumentOperationService.CanPerformOperation.method">CanPerformOperation</a> and <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DocumentOperationService.PerformOperation.method">PerformOperation</a> methods. To register your custom class, use the <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.Register~T~TImpl~.method">DefaultWebDocumentViewerContainer.Register</a> method at the application startup.<br><br>On the client side, add a new toolbar button in the <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Scripts.ASPxClientWebDocumentViewer.CustomizeMenuActions.event">CustomizeMenuActions</a> event handler and call the <a href="https://documentation.devexpress.com/XtraReports/DevExpress.XtraReports.Web.Scripts.ASPxClientWebDocumentViewer.PerformCustomDocumentOperation.overloads">PerformCustomDocumentOperation</a> method when clicking this button.

<br/>


<!-- feedback -->
## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-web-mvc-email-report&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-web-mvc-email-report&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
