using Microsoft.Reporting.WebForms;

namespace EHR.Infrastructure.Service.Report
{
    public class ReportGenerationService
    {
        private readonly LocalReport _localReport = new LocalReport();


        public ReportGenerationService(string reportPath)
        {
            _localReport.ReportPath = reportPath;
        }

        public byte[] GenerateReport(object data, ReportType type)
        {
            var reportDataSource = new ReportDataSource { Name = "DataSet1", Value = data };

            _localReport.DataSources.Add(reportDataSource);

            string reportType = type.ToString();
            string mimeType;
            string encoding;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            string deviceInfo = DefinePropertiesOfDevice(type);
            byte[] renderedBytes = _localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            return renderedBytes;
        }

        private static string DefinePropertiesOfDevice(ReportType type)
        {
            string deviceInfo = null;
            switch (type)
            {
                case ReportType.pdf:
                    deviceInfo = "<DeviceInfo>" +
                                 "  <OutputFormat>PDF</OutputFormat>" +
                                 "  <PageWidth>8.5in</PageWidth>" +
                                 "  <PageHeight>11in</PageHeight>" +
                                 "  <MarginTop>0.5in</MarginTop>" +
                                 "  <MarginLeft>1in</MarginLeft>" +
                                 "  <MarginRight>1in</MarginRight>" +
                                 "  <MarginBottom>0.5in</MarginBottom>" +
                                 "</DeviceInfo>";
                    break;
                case ReportType.image:
                    deviceInfo = "<DeviceInfo>" +
                                 "  <OutputFormat>jpeg</OutputFormat>" +
                                 "  <PageWidth>8.5in</PageWidth>" +
                                 "  <PageHeight>11in</PageHeight>" +
                                 "  <MarginTop>0.5in</MarginTop>" +
                                 "  <MarginLeft>1in</MarginLeft>" +
                                 "  <MarginRight>1in</MarginRight>" +
                                 "  <MarginBottom>0.5in</MarginBottom>" +
                                 "</DeviceInfo>";
                    break;
            }
            return deviceInfo;
        }
    }

    public enum ReportType
    {
        pdf,
        image
    }
}
