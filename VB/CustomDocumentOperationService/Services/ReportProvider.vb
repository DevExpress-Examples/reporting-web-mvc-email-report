Imports DevExpress.XtraReports.Services
Imports DevExpress.XtraReports.UI

Namespace DocumentOperationServiceSample.Services
	Public Class ReportProvider
		Implements IReportProvider

		Private Function IReportProvider_GetReport(ByVal id As String, ByVal context As ReportProviderContext) As XtraReport Implements IReportProvider.GetReport

			Return If(id = "CategoriesReport", New CategoriesReport(), New XtraReport())
		End Function
	End Class
End Namespace
