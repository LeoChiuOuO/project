using System.Data;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Utilities;
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
            test_item AS TestItem,
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

        return _db.QueryFirstOrDefault<Report>(sql, new { Id = id });
    }

    public PagedResult<Report> GetReports(ReportFilter filter)
    {
        filter ??= new ReportFilter();
        filter.DateFrom ??= DateTime.Today.AddMonths(-1);
        filter.DateTo ??= DateTime.Today;

        // 基礎查詢
        var query = _context.Reports
            .Where(r => r.TestingDate >= filter.DateFrom && r.TestingDate <= filter.DateTo);

        // 單值篩選
        // if (!string.IsNullOrWhiteSpace(filter.TestItem))
            // query = query.Where(r => r.TestItem == filter.TestItem);

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
    public bool UpdateReport(ReportUpdateDto report)
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

    public IEnumerable<Report> GetReportsForExport(ReportFilter filter)
    {
        // 可重用 GetReports 的 SQL 組裝，但不要加 LIMIT/OFFSET
        var sqlBuilder = new StringBuilder(@"
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
            test_item AS TestItem,
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
            WHERE testing_date BETWEEN @DateFrom AND @DateTo
        ");

        var countBuilder = new StringBuilder("SELECT 1"); // 佔位，避免多餘

        var parameters = new DynamicParameters();
        parameters.Add("DateFrom", filter.DateFrom ?? DateTime.Today.AddMonths(-1));
        parameters.Add("DateTo", filter.DateTo ?? DateTime.Today);

        // if (!string.IsNullOrWhiteSpace(filter.TestItem))
        // {
        //     sqlBuilder.Append(" AND test_item = @TestItem");
        //     parameters.Add("TestItem", filter.TestItem);
        // }

        if (filter.NotifyStatus != null && filter.NotifyStatus.Any())
        {
            sqlBuilder.Append(" AND notification_status IN @NotifyStatus");
            parameters.Add("NotifyStatus", filter.NotifyStatus);
        }

        if (filter.TrackStatus != null && filter.TrackStatus.Any())
        {
            sqlBuilder.Append(" AND tracking_status IN @TrackStatus");
            parameters.Add("TrackStatus", filter.TrackStatus);
        }

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            sqlBuilder.Append(@"
                AND (
                    specimen_number LIKE CONCAT('%', @Keyword, '%') OR
                    inspection_institution LIKE CONCAT('%', @Keyword, '%') OR
                    sending_physician_name LIKE CONCAT('%', @Keyword, '%') OR
                    name LIKE CONCAT('%', @Keyword, '%')
                )
            ");
            parameters.Add("Keyword", filter.Keyword);
        }

        sqlBuilder.Append(" ORDER BY testing_date DESC");

        return _db.Query<Report>(sqlBuilder.ToString(), parameters);
    }

    public PagedResult<Report> Search(ReportFilter filter)
    {
        var query = _context.Reports
        .Include(r => r.TestItem)
        .Include(r => r.Department)
        .Include(r => r.Partition)
        .AsQueryable();

        // 篩選：日期區間
        if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
            query = query.Where(r => r.TestingDate >= filter.DateFrom && r.TestingDate <= filter.DateTo);

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
                r.Name.Contains(filter.Keyword));
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

    public List<TestItem> GetAllTestItem()
    {
        return _context.TestItems.ToList();
    }
}