using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace WebApplication_Dianthus.Models.Interface
{
    public interface IReportRepository
    {
        PagedResult<Report> GetReports(ReportFilter filter);
        Report GetReportById(int id);
        bool UpdateReport(ReportUpdateDto report);
        List<string> GetDistinctSpecimenTypes(string columnName);
        IEnumerable<Report> GetReportsForExport(ReportFilter filter);
    }
}