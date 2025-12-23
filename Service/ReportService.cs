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
        private readonly IOperationLogService _log;

        public ReportService(IDbConnection db, IAuthService auth, IReportRepository repo, ExcelExporter excelExporter, IMapper mapper, IOperationLogService log,IHttpContextAccessor http)
        {
            _db = db;
            _auth = auth;
            _repo = repo;
            _excelExporter = excelExporter;
            _mapper = mapper;
            _log = log;
        }

        public void CreateReport(Report report)
        {
            //塞入預設值
            report.TrackingStatus = "待追蹤";
            report.NotificationStatus = "待通知";
            report.SendEmailState = "未發送";
            _repo.CreateReport(report);
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

        public bool UpdateReport(ReportUpdateDTO report)
        {
            var success = _repo.UpdateReport(report);
            _log.Log(
                actionType: "Update",
                module: "ReportService",
                success: success,
                description: success
                    ? $"更新報告成功：ID={report.Id}"
                    : $"更新報告失敗：ID={report.Id}"
            );

            return success;
        }

        public List<string> GetSpecimenTypeOptions(string columnName)
        {
            return _repo.GetDistinctSpecimenTypes(columnName);
        }

        public byte[] ExportSimplifiedReports(ReportFilter filter)
        {
            try{
                var reports = _repo.GetSimplifiedReportsForExport(filter);
                var dtoList = _mapper.Map<List<ReportDTO>>(reports);
                _log.Log(actionType: "ExportSimplified", module: "ReportService", success: true, description: "ExportSimplified匯出成功");
                return _excelExporter.ExportReports(dtoList);
            }catch(Exception ex)
            {
                _log.Log(actionType: "ExportSimplified", module: "ReportService", success: false, description: "ExportSimplified匯出失敗: " + ex.Message);
                return null;
            }
        }

        public byte[] ExportFullReports(ReportFilter filter)
        {
            try{
                var reports = _repo.GetFullReportsForExport(filter);
                var dtoList = _mapper.Map<List<Report>>(reports);
                _log.Log(actionType: "ExportFull", module: "ReportService", success: true, description: "ExportFull匯出成功");
                return _excelExporter.ExportReports(dtoList);
            }catch(Exception ex)
            {
                _log.Log(actionType: "ExportFull", module: "ReportService", success: false, description: "ExportFull匯出失敗: " + ex.Message);
                return null;
            }
        }

        public PagedResult<ReportDTO> SearchReports(ReportFilter filter, string partitionId, bool isAdmin)
        {
            try{
                var pagedResult = _repo.Search(filter, partitionId, isAdmin);
                var dtoList = pagedResult.Data.Select(r => new ReportDTO
                {
                    Id = r.Id,
                    SpecimenNumber = r.SpecimenNumber,
                    SpecimenType = r.SpecimenType,
                    InspectionProgress = r.InspectionProgress,
                    MrNumber = r.MrNumber,
                    Name = r.Name,
                    TestItemName = r.TestItem.name, // 來自 TestItem 導覽屬性

                    ReceivedDate = r.ReceivedDate,
                    TestingDate = r.TestingDate,
                    ReportDate = r.ReportDate,
                    InspectionInstitution = r.InspectionInstitution,
                    SendingPhysicianName = r.SendingPhysicianName,
                    AssessmentStatus = r.AssessmentStatus,
                    NotificationStatus = r.NotificationStatus,
                    TrackingStatus = r.TrackingStatus
                }).ToList();

                _log.Log(actionType: "Search", module: "ReportService", success: true, description: "");

                return new PagedResult<ReportDTO>
                {
                    Data = dtoList,
                    Total = pagedResult.Total,
                    Page = pagedResult.Page,
                    PageSize = pagedResult.PageSize
                };
            }catch (Exception ex)
            {
                _log.Log(actionType: "Search", module: "ReportService", success: false, description: ex.Message);
                return new PagedResult<ReportDTO>();
            }
        }

        public List<TestItem> GetAllTestItem()
        {
            return _repo.GetAllTestItem();
        }

        public ReportDashboardViewModel GetDashboard()
        {
            return _repo.GetDashboardStats();
        }
    }
}