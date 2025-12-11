using System.Data;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Utilities;
using WebApplication_Dianthus.Models.DTO;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository;

public class ReportRepository : IReportRepository
{
    private readonly IDbConnection _db;
    private readonly AppDbContext _context;

    public ReportRepository(IDbConnection db, AppDbContext context)
    {
        _db = db;
        _context = context;
    }

    public Report GetReportById(int id)
    {
        var sql = @"
        SELECT
            id AS Id,
            report_id AS ReportId,
            medical_order AS MedicalOrder,
            consent_form_state AS ConsentFormState,
            specimen_dely_state AS SpecimenDelyState,
            send_email_state AS SendEmailState,
            product_name AS ProductName,
            tracking_status AS TrackingStatus,
            notification_status AS NotificationStatus,
            partition_id AS PartitionId,
            department_id AS DepartmentId,
            submission_date AS SubmissionDate,
            name AS Name,
            id_number AS IdNumber,
            mr_number AS MrNumber,
            cost AS Cost,
            return_date AS ReturnDate,
            sending_physician_name AS SendingPhysicianName,
            remark AS Remark,
            report_date AS ReportDate,
            report_results AS ReportResults,
            create_id AS CreateId,
            modify_id AS ModifyId,
            specimen_number AS SpecimenNumber,
            testing_date AS TestingDate,
            weeks_of_pregnancy AS WeeksOfPregnancy,
            due_date AS DueDate,
            inspection_institution AS InspectionInstitution,
            inspection_institution_phone AS InspectionInstitutionPhone,
            responsible_business_person AS ResponsibleBusinessPerson,
            responsible_business_phone AS ResponsibleBusinessPhone,
            responsible_business_email AS ResponsibleBusinessEmail,
            business_manager AS BusinessManager,
            business_manager_phone AS BusinessManagerPhone,
            business_manager_email AS BusinessManagerEmail,
            abnormal_report_delivery_method AS AbnormalReportDeliveryMethod,
            abnormal_report_notification_method AS AbnormalReportNotificationMethod,
            inspection_group AS InspectionGroup,
            notification_circumstances AS NotificationCircumstances,
            prenatal_testing_project_tracking_time AS PrenatalTestingProjectTrackingTime,
            confirm_specimen_submission_time AS ConfirmSpecimenSubmissionTime,
            confirm_specimen_type AS ConfirmSpecimenType,
            confirm_the_test_report_results AS ConfirmTheTestReportResults,
            tracking_time AS TrackingTime,
            tracking AS Tracking,
            tracking_results AS TrackingResults,
            tracking_the_followup_status_of_NIPS_cases AS TrackingTheFollowupStatusOfNIPS_Cases,
            referral_institution AS ReferralInstitution,
            referring_physician AS ReferringPhysician,
            written_report_processing_methood AS WrittenReportProcessingMethood,
            fmr1_report_results AS Fmr1ReportResults,
            chr_report_date AS ChrReportDate,
            chr_report_results AS ChrReportResults,
            wafer_report_date AS WaferReportDate,
            wafer_report_results AS WaferReportResults,
            v2_v3_testing_results AS V2V3TestingResults,
            gene_report_date AS GeneReportDate,
            gene_report_results AS GeneReportResults,
            other_report_date AS OtherReportDate,
            other_report_results AS OtherReportResults,
            created_at AS CreatedAt,
            updated_at AS UpdatedAt,
            deleted_at AS DeletedAt
            FROM reports
            WHERE id = @Id
            LIMIT 1;
        ";
        var report = _db.QueryFirstOrDefault<Report>(sql, new { Id = id });

        if (report != null && report.TestItemId > 0)
        {
            report.TestItem = _context.TestItems.Find(report.TestItemId);
        }

        return report;
    }

    public PagedResult<Report> GetReports(ReportFilter filter)
    {
        filter ??= new ReportFilter();
        filter.DateFrom ??= DateTime.Today.AddMonths(-1);
        filter.DateTo ??= DateTime.Today;

        // 基礎查詢
        var query = _context.Reports
            .Where(r => r.TestingDate >= filter.DateFrom && r.TestingDate <= filter.DateTo);

        // 多值篩選：NotificationStatus
        if (filter.NotifyStatus != null && filter.NotifyStatus.Any())
            query = query.Where(r => filter.NotifyStatus.Contains(r.NotificationStatus));

        // 多值篩選：TrackingStatus
        if (filter.TrackStatus != null && filter.TrackStatus.Any())
            query = query.Where(r => filter.TrackStatus.Contains(r.TrackingStatus));

        // 關鍵字模糊查詢
        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = query.Where(r =>
                r.SpecimenNumber.Contains(filter.Keyword) ||
                r.InspectionInstitution.Contains(filter.Keyword) ||
                r.SendingPhysicianName.Contains(filter.Keyword) ||
                r.Name.Contains(filter.Keyword)
            );
        }

        // 總筆數
        var total = query.Count();

        // 分頁 + 排序
        var data = query
            .OrderByDescending(r => r.TestingDate)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResult<Report>
        {
            Data = data,
            Total = total,
            Page = filter.Page,
            PageSize = filter.PageSize
        };

    }
    public bool UpdateReport(ReportUpdateDTO report)
    {
        var sql = @"
        UPDATE reports SET
            notification_status = @NotificationStatus,
            tracking_status = @TrackingStatus,
            notification_circumstances = @NotificationCircumstances,
            confirm_specimen_submission_time = @ConfirmSpecimenSubmissionTime,
            confirm_specimen_type = @ConfirmSpecimenType,
            confirm_the_test_report_results = @ConfirmTheTestReportResults,
            referral_institution = @ReferralInstitution,
            referring_physician = @ReferringPhysician,
            remark = @Remark,
            updated_at = CURRENT_TIMESTAMP
        WHERE id = @Id;
        ";

        var affected = _db.Execute(sql, report);
        return affected > 0;
    }

    public List<string> GetDistinctSpecimenTypes(string columnName)
    {
        // 安全性防呆：只允許特定欄位
        var allowedColumns = new[] {
            "confirm_specimen_type",
            "test_item",
            "notification_status",
            "tracking_status"
        };

        if (!allowedColumns.Contains(columnName))
            throw new ArgumentException("不允許查詢此欄位");

        var sql = $@"
            SELECT DISTINCT {columnName}
            FROM reports
            WHERE {columnName} IS NOT NULL AND {columnName} != ''
            ORDER BY {columnName};
        ";

        return _db.Query<string>(sql).ToList();

    }

    public IEnumerable<Report> GetSimplifiedReportsForExport(ReportFilter filter)
    {
        var sql = @"
            SELECT
                r.id AS Id,
                r.report_id AS ReportId,
                r.test_item_id AS TestItemId,
                ti.name AS TestItemName,
                r.name AS Name,
                r.mr_number AS MrNumber,
                r.specimen_number AS SpecimenNumber,
                r.testing_date AS TestingDate,
                r.report_date AS ReportDate,
                r.notification_status AS NotificationStatus,
                r.tracking_status AS TrackingStatus,
                r.sending_physician_name AS SendingPhysicianName,
                r.inspection_institution AS InspectionInstitution,
                r.remark AS Remark
            FROM reports r
            LEFT JOIN test_item ti ON r.test_item_id = ti.id
            WHERE r.testing_date BETWEEN @DateFrom AND @DateTo
        ";

        var parameters = new DynamicParameters();
        parameters.Add("DateFrom", filter.DateFrom ?? DateTime.Today.AddMonths(-1));
        parameters.Add("DateTo", filter.DateTo ?? DateTime.Today);

        if (filter.TestItemIds?.Any() == true)
        {
            sql += " AND r.test_item_id IN @TestItemIds";
            parameters.Add("TestItemIds", filter.TestItemIds);
        }

        if (filter.NotifyStatus?.Any() == true)
        {
            sql += " AND r.notification_status IN @NotifyStatus";
            parameters.Add("NotifyStatus", filter.NotifyStatus);
        }

        if (filter.TrackStatus?.Any() == true)
        {
            sql += " AND r.tracking_status IN @TrackStatus";
            parameters.Add("TrackStatus", filter.TrackStatus);
        }

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            sql += @"
                AND (
                    r.specimen_number LIKE CONCAT('%', @Keyword, '%') OR
                    r.inspection_institution LIKE CONCAT('%', @Keyword, '%') OR
                    r.sending_physician_name LIKE CONCAT('%', @Keyword, '%') OR
                    r.name LIKE CONCAT('%', @Keyword, '%')
                )
            ";
            parameters.Add("Keyword", filter.Keyword);
        }

        sql += " ORDER BY r.testing_date DESC";

        return _db.Query<Report>(sql, parameters);

    }
    public IEnumerable<Report> GetFullReportsForExport(ReportFilter filter)
    {
        var sqlBuilder = new StringBuilder(@"
        SELECT
            r.id AS Id,
            r.report_id AS ReportId,
            r.medical_order AS MedicalOrder,
            r.consent_form_state AS ConsentFormState,
            r.specimen_dely_state AS SpecimenDelyState,
            r.send_email_state AS SendEmailState,
            r.product_name AS ProductName,
            r.tracking_status AS TrackingStatus,
            r.notification_status AS NotificationStatus,
            r.partition_id AS PartitionId,
            r.department_id AS DepartmentId,
            r.submission_date AS SubmissionDate,
            r.name AS Name,
            r.id_number AS IdNumber,
            r.mr_number AS MrNumber,
            r.test_item_id AS TestItemId,
            ti.name AS TestItemName,
            r.cost AS Cost,
            r.return_date AS ReturnDate,
            r.sending_physician_name AS SendingPhysicianName,
            r.remark AS Remark,
            r.report_date AS ReportDate,
            r.report_results AS ReportResults,
            r.create_id AS CreateId,
            r.modify_id AS ModifyId,
            r.specimen_number AS SpecimenNumber,
            r.testing_date AS TestingDate,
            r.weeks_of_pregnancy AS WeeksOfPregnancy,
            r.due_date AS DueDate,
            r.inspection_institution AS InspectionInstitution,
            r.inspection_institution_phone AS InspectionInstitutionPhone,
            r.responsible_business_person AS ResponsibleBusinessPerson,
            r.responsible_business_phone AS ResponsibleBusinessPhone,
            r.responsible_business_email AS ResponsibleBusinessEmail,
            r.business_manager AS BusinessManager,
            r.business_manager_phone AS BusinessManagerPhone,
            r.business_manager_email AS BusinessManagerEmail,
            r.abnormal_report_delivery_method AS AbnormalReportDeliveryMethod,
            r.abnormal_report_notification_method AS AbnormalReportNotificationMethod,
            r.inspection_group AS InspectionGroup,
            r.notification_circumstances AS NotificationCircumstances,
            r.prenatal_testing_project_tracking_time AS PrenatalTestingProjectTrackingTime,
            r.confirm_specimen_submission_time AS ConfirmSpecimenSubmissionTime,
            r.confirm_specimen_type AS ConfirmSpecimenType,
            r.confirm_the_test_report_results AS ConfirmTheTestReportResults,
            r.tracking_time AS TrackingTime,
            r.tracking AS Tracking,
            r.tracking_results AS TrackingResults,
            r.tracking_the_followup_status_of_NIPS_cases AS TrackingTheFollowupStatusOfNIPS_Cases,
            r.referral_institution AS ReferralInstitution,
            r.referring_physician AS ReferringPhysician,
            r.written_report_processing_methood AS WrittenReportProcessingMethood,
            r.fmr1_report_results AS Fmr1ReportResults,
            r.chr_report_date AS ChrReportDate,
            r.chr_report_results AS ChrReportResults,
            r.wafer_report_date AS WaferReportDate,
            r.wafer_report_results AS WaferReportResults,
            r.v2_v3_testing_results AS V2V3TestingResults,
            r.gene_report_date AS GeneReportDate,
            r.gene_report_results AS GeneReportResults,
            r.other_report_date AS OtherReportDate,
            r.other_report_results AS OtherReportResults,
            r.created_at AS CreatedAt,
            r.updated_at AS UpdatedAt,
            r.deleted_at AS DeletedAt
            FROM reports r
            LEFT JOIN test_item ti ON r.test_item_id = ti.id
            WHERE r.testing_date BETWEEN @DateFrom AND @DateTo
        ");

        var parameters = new DynamicParameters();
        parameters.Add("DateFrom", filter.DateFrom ?? DateTime.Today.AddMonths(-1));
        parameters.Add("DateTo", filter.DateTo ?? DateTime.Today);

        if (filter.TestItemIds?.Any() == true)
        {
            sqlBuilder.Append(" AND r.test_item_id IN @TestItemIds");
            parameters.Add("TestItemIds", filter.TestItemIds);
        }

        if (filter.NotifyStatus?.Any() == true)
        {
            sqlBuilder.Append(" AND r.notification_status IN @NotifyStatus");
            parameters.Add("NotifyStatus", filter.NotifyStatus);
        }

        if (filter.TrackStatus?.Any() == true)
        {
            sqlBuilder.Append(" AND r.tracking_status IN @TrackStatus");
            parameters.Add("TrackStatus", filter.TrackStatus);
        }

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            sqlBuilder.Append(@"
                AND (
                    r.specimen_number LIKE CONCAT('%', @Keyword, '%') OR
                    r.inspection_institution LIKE CONCAT('%', @Keyword, '%') OR
                    r.sending_physician_name LIKE CONCAT('%', @Keyword, '%') OR
                    r.name LIKE CONCAT('%', @Keyword, '%')
                )
            ");
            parameters.Add("Keyword", filter.Keyword);
        }

        sqlBuilder.Append(" ORDER BY r.testing_date DESC");

        return _db.Query<Report>(sqlBuilder.ToString(), parameters);
    }

    public PagedResult<Report> Search(ReportFilter filter, string partitionId, bool isAdmin)
    {
        var query = _context.Reports
        .Include(r => r.TestItem)
        .Include(r => r.Department)
        .Include(r => r.Partition)
        .AsQueryable();

        // 權限控管：非 Admin 則限制 PartitionId
        if (!isAdmin && !string.IsNullOrEmpty(partitionId))
        {
            query = query.Where(r => r.PartitionId.ToString() == partitionId);
        }

        // 篩選：日期區間(收件日期)
        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            query = query.Where(r => r.ReceivedDate >= filter.ReceiveDateFrom && r.ReceivedDate <= filter.ReceiveDateTo);

        // 篩選：日期區間(檢測日期)
        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            query = query.Where(r => r.TestingDate >= filter.TestDateFrom && r.TestingDate <= filter.TestDateTo);

        // 篩選：日期區間(報告日期)
        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            query = query.Where(r => r.ReportDate >= filter.DateFrom && r.ReportDate <= filter.DateTo);

        // 篩選：TestItemId
        if (filter.TestItemIds?.Any() == true)
            query = query.Where(r => filter.TestItemIds.Contains(r.TestItemId));

        // 篩選：NotificationStatus
        if (filter.NotifyStatus?.Any() == true)
            query = query.Where(r => filter.NotifyStatus.Contains(r.NotificationStatus));

        // 篩選：TrackingStatus
        if (filter.TrackStatus?.Any() == true)
            query = query.Where(r => filter.TrackStatus.Contains(r.TrackingStatus));

        // 關鍵字模糊查詢
        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            query = query.Where(r =>
                r.SpecimenNumber.Contains(filter.Keyword) ||
                r.InspectionInstitution.Contains(filter.Keyword) ||
                r.SendingPhysicianName.Contains(filter.Keyword) ||
                r.AssessmentStatus.Contains(filter.Keyword) ||
                r.Name.Contains(filter.Keyword));
        }

        // 篩選報告嚴重度
        if (!string.IsNullOrEmpty(filter.AssessmentStatus))
        query = query.Where(r => r.AssessmentStatus == filter.AssessmentStatus);

        // 總筆數
        var total = query.Count();

        // 分頁 + 排序
        var data = query
            .OrderByDescending(r => r.TestingDate)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResult<Report>
        {
            Data = data,
            Total = total,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
    }

    public List<TestItem> GetAllTestItem()
    {
        return _context.TestItems.ToList();
    }

    public IEnumerable<Report> GetReportsForExport(ReportFilter filter)
    {
        throw new NotImplementedException();
    }

    public ReportDashboardViewModel GetDashboardStats()
    {
        var today = DateTime.Today;
        var unread = _context.Reports
            .Count(r => r.TrackingStatus == "待追蹤" && r.NotificationStatus == "待通知" );

        var upcoming = _context.Reports
            .Count(r => r.TrackingStatus == "待追蹤" && r.NotificationStatus == "待通知"
                && r.ReportDate >= today.AddDays(-30) && r.ReportDate < today.AddDays(-19));

        var overdue = _context.Reports
            .Count(r => r.TrackingStatus == "待追蹤" && r.NotificationStatus == "待通知"
                && r.ReportDate >= today.AddDays(-60) && r.ReportDate < today.AddDays(-29));

        var critical = _context.Reports
            .Count(r => r.TrackingStatus == "待追蹤" && r.NotificationStatus == "待通知"
                && r.ReportDate < today.AddDays(-59));

        var criticalAssessment = _context.Reports
            .Count(r => r.AssessmentStatus == "重大");

        return new ReportDashboardViewModel
        {
            UnreadCount = unread,
            UpcomingOverdueCount = upcoming,
            OverdueCount = overdue,
            CriticalOverdueCount = critical,
            CriticalAssessmentCount = criticalAssessment    
        };

    }
}