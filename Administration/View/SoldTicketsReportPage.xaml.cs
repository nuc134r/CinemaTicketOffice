using System;
using Administration.Report;
using Administration.Report.SoldTicketsTableAdapters;
using Microsoft.Reporting.WinForms;

namespace Administration.View
{
    public partial class SoldTicketsReportPage
    {
        private bool isReportViewerLoaded;

        public SoldTicketsReportPage()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (isReportViewerLoaded) return;

            var reportDataSource1 = new ReportDataSource();
            var dataset = new SoldTickets();

            dataset.BeginInit();

            reportDataSource1.Name = "SoldTicketsDataSet"; //Name of the report dataset in our .RDLC file
            reportDataSource1.Value = dataset.BrowseTickets;
            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            _reportViewer.LocalReport.ReportEmbeddedResource = "Administration.Report.SoldTicketsReport.rdlc";

            dataset.EndInit();

            //fill data into adventureWorksDataSet
            var adapter = new BrowseTicketsTableAdapter {ClearBeforeFill = true};
            adapter.Fill(dataset.BrowseTickets);

            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.RefreshReport();


            isReportViewerLoaded = true;
        }
    }
}