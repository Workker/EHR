using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

namespace EHR.Infrastructure.Service.Report
{
    public class ReportGenerationService : IDisposable
    {
        private readonly LocalReport _localReport = new LocalReport();

        public ReportGenerationService(string reportPath)
        {
            _localReport.ReportPath = reportPath;
        }

        public byte[] GenerateReport(List<ReportDataSource> reportDataSources, ReportType type)
        {
            foreach (var reportDataSource in reportDataSources)
            {
                _localReport.DataSources.Add(reportDataSource);
            }

            string reportType = type.ToString();
            string encoding;
            string mimeType;
            string fileNameExtension;
            Warning[] warnings;
            string[] streams;
            string deviceInfo = DefinePropertiesOfDevice(type);
            byte[] renderedBytes = _localReport.Render(reportType, deviceInfo, out mimeType, out encoding,
                                                       out fileNameExtension, out streams, out warnings);

            return renderedBytes;
        }

        public ReportDataSource CreateReportDataSource(object data, string dataSet)
        {
            return new ReportDataSource { Name = dataSet, Value = data };
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _localReport.Dispose();
            }
        }

        public enum ReportType
        {
            pdf,
            image
        }
    }
}
