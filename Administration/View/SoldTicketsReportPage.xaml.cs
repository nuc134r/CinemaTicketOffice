using System;
using System.Data.SqlClient;
using Administration.Properties;
using Administration.Report;
using Administration.Report.SoldTicketsTableAdapters;
using DataAccess;
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

            reportDataSource1.Name = "SoldTicketsDataSet";
            reportDataSource1.Value = dataset.BrowseTickets;
            _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            _reportViewer.LocalReport.ReportEmbeddedResource = "Administration.Report.SoldTicketsReport.rdlc";

            dataset.EndInit();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);
            var connection = new SqlConnection(connectionString);
            var adapter = new BrowseTicketsTableAdapter { ClearBeforeFill = true, Connection = connection };
            adapter.Fill(dataset.BrowseTickets);

            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.RefreshReport();


            isReportViewerLoaded = true;
        }
    }
}