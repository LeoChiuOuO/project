using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models.Interface
{
    public interface IReportRepository
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        Report GetReportById(int id);
        bool UpdateReport(ReportUpdateDTO report);
        List<string> GetDistinctSpecimenTypes(string columnName);
        IEnumerable<Report> GetReportsForExport(ReportFilter filter);
        IEnumerable<Report> GetSimplifiedReportsForExport(ReportFilter filter);
        IEnumerable<Report> GetFullReportsForExport(ReportFilter filter);

        PagedResult<Report> Search(ReportFilter filter, string partitionId, bool isAdmin);
        List<TestItem> GetAllTestItem();
        ReportDashboardViewModel GetDashboardStats();

    }
}