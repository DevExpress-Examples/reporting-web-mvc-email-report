using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;

namespace DocumentOperationServiceSample.Services {
    public class ReportProvider : IReportProvider {
        XtraReport IReportProvider.GetReport(string id, ReportProviderContext context) {

            return (id == "CategoriesReport") ? new CategoriesReport() : new XtraReport() ;
        }
    }
}
