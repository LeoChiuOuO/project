using System.Data;
using Dapper;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Services
{

    public class ReportService : IReportService
    {
        private readonly IDbConnection _db;
        private readonly IAuthService _auth;
        private readonly IReportRepository _repo;


        public ReportService(IDbConnection db, IAuthService auth, IReportRepository repo)
        {
            _db = db;
            _auth = auth;
            _repo = repo;
        }

        public void CreateReport(Report report)
        {
            throw new NotImplementedException();
        }

        public void DeleteReport(int id)
        {
            throw new NotImplementedException();
        }

        public Report? GetReportById(int id)
        {
            return _repo.GetReportById(id);
        }

        public PagedResult<Report> GetReports(ReportFilter filter)
        {
            return _repo.GetReports(filter);
        }

        public bool UpdateReport(ReportUpdateDto report)
        {
            return _repo.UpdateReport(report);
        }

        public List<string> GetSpecimenTypeOptions(string columnName)
        {
            return _repo.GetDistinctSpecimenTypes(columnName);
        }

    }
}