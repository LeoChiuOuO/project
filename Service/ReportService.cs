using System.Data;
using AutoMapper;
using Dapper;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using WebApplication_Dianthus.Models.Interface;
using ClosedXML.Excel;
using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Services
{

    public class ReportService : IReportService
    {
        private readonly IDbConnection _db;
        private readonly IAuthService _auth;
        private readonly IReportRepository _repo;
        private readonly ExcelExporter _excelExporter;
        private readonly IMapper _mapper;

        public ReportService(IDbConnection db, IAuthService auth, IReportRepository repo, ExcelExporter excelExporter, IMapper mapper)
        {
            _db = db;
            _auth = auth;
            _repo = repo;
            _excelExporter = excelExporter;
            _mapper = mapper;
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

        public byte[] ExportReports(ReportFilter filter)
        {
            var reports = _repo.GetReportsForExport(filter);
            var dtoList = _mapper.Map<List<ReportDTO>>(reports);
            var result = _excelExporter.ExportReports(dtoList);
            return result;
        }

        public PagedResult<ReportDTO> SearchReports(ReportFilter filter)
        {
            var pagedResult = _repo.Search(filter);

            var dtoList = pagedResult.Data.Select(r => new ReportDTO
            {
                Id = r.Id,
                SpecimenNumber = r.SpecimenNumber,
                SpecimenType = r.SpecimenType,
                InspectionProgress = r.InspectionProgress,
                MrNumber = r.MrNumber,
                Name = r.Name,
                TestItemName = r.TestItem.name, // ✅ 來自 TestItem 導覽屬性

                ReceivedDate = r.ReceivedDate,
                TestingDate = r.TestingDate,
                ReportDate = r.ReportDate,
                InspectionInstitution = r.InspectionInstitution,
                SendingPhysicianName = r.SendingPhysicianName,
                AssessmentStatus = r.AssessmentStatus,
                NotificationStatus = r.NotificationStatus,
                TrackingStatus = r.TrackingStatus
            }).ToList();

            return new PagedResult<ReportDTO>
            {
                Data = dtoList,
                Total = pagedResult.Total,
                Page = pagedResult.Page,
                PageSize = pagedResult.PageSize
            };
        }

        public List<TestItem> GetAllTestItem()
        {
            return _repo.GetAllTestItem();
        }
    }
}