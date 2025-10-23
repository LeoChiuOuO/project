using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Utilities;
using WebApplication_Dianthus.Models.Service.Interface;

public class HomeController : Controller
{
    private readonly IDbConnection _db;
    private readonly IReportService _reportService;

    public HomeController(IDbConnection db,IReportService reportService)
    {
        _db = db;
        _reportService = reportService;
    }
    public IActionResult Index()
    {
        var dashboard = _reportService.GetDashboard();
        return View(dashboard);

    }

    [HttpGet]
    public IActionResult GetDashboard()
    {
        var dashboard = _reportService.GetDashboard();
        return Json(dashboard);
    }


    public IActionResult TestOne()
    {
        var sql = @"INSERT INTO reports (
    report_id, medical_order, consent_form_state, specimen_dely_state, send_email_state, product_name,
    tracking_status, notification_status, partition_id, department_id, submission_date, name, id_number,
    mr_number, test_item, cost, return_date, sending_physician_name, remark, report_date, report_results,
    create_id, modify_id, specimen_number, testing_date, weeks_of_pregnancy, due_date, inspection_institution,
    inspection_institution_phone, responsible_business_person, responsible_business_phone, responsible_business_email,
    business_manager, business_manager_phone, business_manager_email, abnormal_report_delivery_method,
    abnormal_report_notification_method, inspection_group, notification_circumstances, prenatal_testing_project_tracking_time,
    confirm_specimen_submission_time, confirm_specimen_type, confirm_the_test_report_results, tracking_time, tracking,
    tracking_results, tracking_the_followup_status_of_NIPS_cases, referral_institution, referring_physician,
    written_report_processing_methood, fmr1_report_results, chr_report_date, chr_report_results, wafer_report_date,
    wafer_report_results, v2_v3_testing_results, gene_report_date, gene_report_results, other_report_date, other_report_results,
    created_at, updated_at, deleted_at
    )
    SELECT
        CONCAT('RPT', LPAD(1,5,'0')),
        CONCAT('MO', LPAD(1,3,'0')),
        ELT(1 + FLOOR(RAND(11)*2), '已簽','未簽'),
        ELT(1 + FLOOR(RAND(13)*2), '已送達','運送中'),
        ELT(1 + FLOOR(RAND(17)*2), '已發送','未發送'),
        ELT(1 + FLOOR(RAND(19)*5), '產品A','產品B','產品C','產品D','產品E'),
        ELT(1 + FLOOR(RAND(23)*3), '已追蹤','待追蹤','不需追蹤'),
        ELT(1 + FLOOR(RAND(29)*3), '已通知','待通知','不需通知'),
        1 + FLOOR(RAND(31)*3),
        1 + FLOOR(RAND(37)*3),
        CURDATE(),
        CONCAT('測試姓名', 1),
        CONCAT(CHAR(65 + FLOOR(RAND(43)*26)), LPAD(FLOOR(RAND(47)*999999999),9,'0')),
        1000 + 1,
        ELT(1 + FLOOR(RAND(53)*5), '檢測A','檢測B','檢測C','檢測D','檢測E'),
        4000 + FLOOR(RAND(59)*2001),
        CURDATE(),
        CONCAT('醫師', 1),
        CONCAT('備註', 1),
        CURDATE(),
        ELT(1 + FLOOR(RAND(71)*2), '正常','異常'),
        1 + FLOOR(RAND(73)*5),
        1 + FLOOR(RAND(79)*5),
        CONCAT('SP', LPAD(1,4,'0')),
        CURDATE(),
        10 + FLOOR(RAND(89)*21),
        CURDATE(),
        ELT(1 + FLOOR(RAND(101)*5), '台北醫院','榮總','長庚','馬偕醫院','新光醫院'),
        20000000 + FLOOR(RAND(103)*80000000),
        CONCAT('業務', 1),
        900000000 + FLOOR(RAND(107)*99999999),
        CONCAT('sales', 1, '@example.com'),
        CONCAT('主管', 1),
        900000000 + FLOOR(RAND(109)*99999999),
        CONCAT('manager', 1, '@example.com'),
        ELT(1 + FLOOR(RAND(113)*3), '郵寄','快遞','親送'),
        ELT(1 + FLOOR(RAND(127)*3), '電話','簡訊','Email'),
        CONCAT('組別', 1 + FLOOR(RAND(131)*5)),
        CONCAT('情形', 1 + FLOOR(RAND(137)*5)),
        CURDATE(),
        CURDATE(),
        ELT(1 + FLOOR(RAND(151)*2), '血液','羊水'),
        ELT(1 + FLOOR(RAND(157)*2), '陰性','陽性'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(167)*3), '已追蹤','待追蹤','不需追蹤'),
        ELT(1 + FLOOR(RAND(173)*2), '正常','異常'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(181)*5), '台北醫院','榮總','長庚','馬偕醫院','新光醫院'),
        CONCAT('醫師', 1 + FLOOR(RAND(191)*50)),
        ELT(1 + FLOOR(RAND(193)*3), '郵寄','快遞','親送'),
        ELT(1 + FLOOR(RAND(197)*2), '陰性','陽性'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(211)*2), '正常','異常'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(227)*2), '正常','異常'),
        ELT(1 + FLOOR(RAND(229)*2), '陰性','陽性'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(239)*2), '正常','異常'),
        CURDATE(),
        ELT(1 + FLOOR(RAND(251)*2), '正常','異常'),
        NOW(),
        NOW(),
        NULL;";
        _db.Execute(sql);
        return Ok(new { success = true });
    }

    public IActionResult Test100()
    {
        var sql = @"INSERT INTO reports (
    report_id, medical_order, consent_form_state, specimen_dely_state, send_email_state, product_name,
    tracking_status, notification_status, partition_id, department_id, submission_date, name, id_number,
    mr_number, test_item, cost, return_date, sending_physician_name, remark, report_date, report_results,
    create_id, modify_id, specimen_number, testing_date, weeks_of_pregnancy, due_date, inspection_institution,
    inspection_institution_phone, responsible_business_person, responsible_business_phone, responsible_business_email,
    business_manager, business_manager_phone, business_manager_email, abnormal_report_delivery_method,
    abnormal_report_notification_method, inspection_group, notification_circumstances, prenatal_testing_project_tracking_time,
    confirm_specimen_submission_time, confirm_specimen_type, confirm_the_test_report_results, tracking_time, tracking,
    tracking_results, tracking_the_followup_status_of_NIPS_cases, referral_institution, referring_physician,
    written_report_processing_methood, fmr1_report_results, chr_report_date, chr_report_results, wafer_report_date,
    wafer_report_results, v2_v3_testing_results, gene_report_date, gene_report_results, other_report_date, other_report_results,
    created_at, updated_at, deleted_at
)
SELECT
    CONCAT('RPT', LPAD(n,5,'0')) AS report_id,
    CONCAT('MO', LPAD(n,3,'0')) AS medical_order,
    ELT(1 + FLOOR(RAND(n*11)*2), '已簽','未簽') AS consent_form_state,
    ELT(1 + FLOOR(RAND(n*13)*2), '已送達','運送中') AS specimen_dely_state,
    ELT(1 + FLOOR(RAND(n*17)*2), '已發送','未發送') AS send_email_state,
    ELT(1 + FLOOR(RAND(n*19)*5), '產品A','產品B','產品C','產品D','產品E') AS product_name,
    ELT(1 + FLOOR(RAND(n*23)*3), '已追蹤','待追蹤','不需追蹤') AS tracking_status,
    ELT(1 + FLOOR(RAND(n*29)*3), '已通知','待通知','不需通知') AS notification_status,
    1 + FLOOR(RAND(n*31)*3) AS partition_id,        -- 1..3
    1 + FLOOR(RAND(n*37)*3) AS department_id,       -- 1..3
    DATE_SUB(CURDATE(), INTERVAL FLOOR(RAND(n*41)*365) DAY) AS submission_date,
    CONCAT('測試姓名', n) AS name,
    CONCAT(CHAR(65 + FLOOR(RAND(n*43)*26)), LPAD(FLOOR(RAND(n*47)*999999999),9,'0')) AS id_number,
    1000 + n AS mr_number,
    ELT(1 + FLOOR(RAND(n*53)*5), '檢測A','檢測B','檢測C','檢測D','檢測E') AS test_item,
    4000 + FLOOR(RAND(n*59)*2001) AS cost,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*61)*60) DAY) AS return_date,
    CONCAT('醫師', n) AS sending_physician_name,
    CONCAT('備註', n) AS remark,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*67)*60) DAY) AS report_date,
    ELT(1 + FLOOR(RAND(n*71)*2), '正常','異常') AS report_results,
    1 + FLOOR(RAND(n*73)*5) AS create_id,
    1 + FLOOR(RAND(n*79)*5) AS modify_id,
    CONCAT('SP', LPAD(n,4,'0')) AS specimen_number,
    DATE_SUB(CURDATE(), INTERVAL FLOOR(RAND(n*83)*365) DAY) AS testing_date,
    10 + FLOOR(RAND(n*89)*21) AS weeks_of_pregnancy,  -- 10..30
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*97)*200) DAY) AS due_date,
    ELT(1 + FLOOR(RAND(n*101)*5), '台北醫院','榮總','長庚','馬偕醫院','新光醫院') AS inspection_institution,
    20000000 + FLOOR(RAND(n*103)*80000000) AS inspection_institution_phone,
    CONCAT('業務', n) AS responsible_business_person,
    900000000 + FLOOR(RAND(n*107)*99999999) AS responsible_business_phone,  -- 9e8..~1e9+ (INT 安全)
    CONCAT('sales', n, '@example.com') AS responsible_business_email,
    CONCAT('主管', n) AS business_manager,
    900000000 + FLOOR(RAND(n*109)*99999999) AS business_manager_phone,
    CONCAT('manager', n, '@example.com') AS business_manager_email,
    ELT(1 + FLOOR(RAND(n*113)*3), '郵寄','快遞','親送') AS abnormal_report_delivery_method,
    ELT(1 + FLOOR(RAND(n*127)*3), '電話','簡訊','Email') AS abnormal_report_notification_method,
    CONCAT('組別', 1 + FLOOR(RAND(n*131)*5)) AS inspection_group,
    CONCAT('情形', 1 + FLOOR(RAND(n*137)*5)) AS notification_circumstances,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*139)*60) DAY) AS prenatal_testing_project_tracking_time,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*149)*60) DAY) AS confirm_specimen_submission_time,
    ELT(1 + FLOOR(RAND(n*151)*2), '血液','羊水') AS confirm_specimen_type,
    ELT(1 + FLOOR(RAND(n*157)*2), '陰性','陽性') AS confirm_the_test_report_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*163)*60) DAY) AS tracking_time,
    ELT(1 + FLOOR(RAND(n*167)*3), '已追蹤','待追蹤','不需追蹤') AS tracking,
    ELT(1 + FLOOR(RAND(n*173)*2), '正常','異常') AS tracking_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*179)*60) DAY) AS tracking_the_followup_status_of_NIPS_cases,
    ELT(1 + FLOOR(RAND(n*181)*5), '台北醫院','榮總','長庚','馬偕醫院','新光醫院') AS referral_institution,
    CONCAT('醫師', 1 + FLOOR(RAND(n*191)*50)) AS referring_physician,
    ELT(1 + FLOOR(RAND(n*193)*3), '郵寄','快遞','親送') AS written_report_processing_methood,
    ELT(1 + FLOOR(RAND(n*197)*2), '陰性','陽性') AS fmr1_report_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*199)*60) DAY) AS chr_report_date,
    ELT(1 + FLOOR(RAND(n*211)*2), '正常','異常') AS chr_report_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*223)*60) DAY) AS wafer_report_date,
    ELT(1 + FLOOR(RAND(n*227)*2), '正常','異常') AS wafer_report_results,
    ELT(1 + FLOOR(RAND(n*229)*2), '陰性','陽性') AS v2_v3_testing_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*233)*60) DAY) AS gene_report_date,
    ELT(1 + FLOOR(RAND(n*239)*2), '正常','異常') AS gene_report_results,
    DATE_ADD(CURDATE(), INTERVAL FLOOR(RAND(n*241)*60) DAY) AS other_report_date,
    ELT(1 + FLOOR(RAND(n*251)*2), '正常','異常') AS other_report_results,
    NOW() AS created_at,
    NOW() AS updated_at,
    NULL AS deleted_at
    FROM (
        SELECT (a.d + b.d*10) AS n
        FROM (SELECT 0 d UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4
            UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) a
        CROSS JOIN
            (SELECT 0 d UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4
            UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9) b
    ) nums
    WHERE n BETWEEN 1 AND 100;";
        _db.Execute(sql);
        return Ok(new { success = true });
    }

    public IActionResult TestOverTime()
    {
        var testItemId = new Random().Next(10);
        var reportDueDate = DateTime.Today.ToString("yyyy-MM-dd");
        var sql = @"
        INSERT INTO reports (
            report_id, medical_order, consent_form_state, specimen_dely_state, send_email_state, product_name,
            tracking_status, notification_status, partition_id, department_id, submission_date, received_date,
            name, id_number, mr_number,test_item_id, cost, return_date, sending_physician_name, remark, report_date,
            report_results, create_id, modify_id, specimen_number, specimen_type, testing_date, inspection_progress,
            assessment_status, weeks_of_pregnancy, due_date,report_due_date, inspection_institution, inspection_institution_phone,
            responsible_business_person, responsible_business_phone, responsible_business_email, business_manager,
            business_manager_phone, business_manager_email, abnormal_report_delivery_method,
            abnormal_report_notification_method, inspection_group, notification_circumstances,
            prenatal_testing_project_tracking_time, confirm_specimen_submission_time, confirm_specimen_type,
            confirm_the_test_report_results, tracking_time, tracking, tracking_results,
            tracking_the_followup_status_of_NIPS_cases, referral_institution, referring_physician,
            written_report_processing_methood, fmr1_report_results, chr_report_date, chr_report_results,
            wafer_report_date, wafer_report_results, v2_v3_testing_results, gene_report_date, gene_report_results,
            other_report_date, other_report_results
        ) VALUES (
            CONCAT('RPT', LPAD(FLOOR(RAND()*99999), 5, '0')),
            CONCAT('MO', LPAD(FLOOR(RAND()*999), 3, '0')),
            ELT(1 + FLOOR(RAND()*2), '已簽', '未簽'),
            ELT(1 + FLOOR(RAND()*2), '已送達', '運送中'),
            ELT(1 + FLOOR(RAND()*2), '已發送', '未發送'),
            ELT(1 + FLOOR(RAND()*5), '產品A', '產品B', '產品C', '產品D', '產品E'),
            '待追蹤',
            '待通知',
            1 + FLOOR(RAND()*3),
            1 + FLOOR(RAND()*3),
            NOW(),
            NOW(),
            CONCAT('測試姓名', FLOOR(RAND()*100)),
            CONCAT(CHAR(65 + FLOOR(RAND()*26)), LPAD(FLOOR(RAND()*999999999), 9, '0')),
            1000 + FLOOR(RAND()*1000),
            5,
            4000 + FLOOR(RAND()*2000),
            NOW(),
            CONCAT('醫師', FLOOR(RAND()*10)),
            CONCAT('備註', FLOOR(RAND()*10)),
            ELT(1 + FLOOR(RAND()*3), CURDATE() - INTERVAL 25 DAY, CURDATE() - INTERVAL 55 DAY, CURDATE() - INTERVAL 65 DAY),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常'),
            1 + FLOOR(RAND()*5),
            1 + FLOOR(RAND()*5),
            CONCAT('SP', LPAD(FLOOR(RAND()*9999), 4, '0')),
            ELT(1 + FLOOR(RAND()*2), '血液', '羊水'),
            NOW(),
            ELT(1 + FLOOR(RAND()*3), '進行中', '已完成', '待處理'),
            ELT(1 + FLOOR(RAND()*2), '中度', '一般'),
            10 + FLOOR(RAND()*30),
            NOW(),
            NOW(),
            ELT(1 + FLOOR(RAND()*5), '台北醫院', '榮總', '長庚', '馬偕醫院', '新光醫院'),
            20000000 + FLOOR(RAND()*80000000),
            CONCAT('業務', FLOOR(RAND()*10)),
            900000000 + FLOOR(RAND()*99999999),
            CONCAT('sales', FLOOR(RAND()*10), '@example.com'),
            CONCAT('主管', FLOOR(RAND()*10)),
            900000000 + FLOOR(RAND()*99999999),
            CONCAT('manager', FLOOR(RAND()*10), '@example.com'),
            ELT(1 + FLOOR(RAND()*3), '郵寄', '快遞', '親送'),
            ELT(1 + FLOOR(RAND()*3), '電話', '簡訊', 'Email'),
            CONCAT('組別', FLOOR(RAND()*5)),
            CONCAT('情形', FLOOR(RAND()*5)),
            NOW(),
            NOW(),
            ELT(1 + FLOOR(RAND()*2), '血液', '羊水'),
            ELT(1 + FLOOR(RAND()*2), '陰性', '陽性'),
            NOW(),
            ELT(1 + FLOOR(RAND()*3), '已追蹤', '待追蹤', '不需追蹤'),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常'),
            NOW(),
            ELT(1 + FLOOR(RAND()*5), '台北醫院', '榮總', '長庚', '馬偕醫院', '新光醫院'),
            CONCAT('醫師', FLOOR(RAND()*50)),
            ELT(1 + FLOOR(RAND()*3), '郵寄', '快遞', '親送'),
            ELT(1 + FLOOR(RAND()*2), '陰性', '陽性'),
            NOW(),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常'),
            NOW(),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常'),
            ELT(1 + FLOOR(RAND()*2), '陰性', '陽性'),
            NOW(),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常'),
            NOW(),
            ELT(1 + FLOOR(RAND()*2), '正常', '異常')
        );
        ";
        _db.Execute(sql);
        return Ok(new { success = true });
    }

    public IActionResult TestHightLight()
    {
        var name = "測試姓名" + new Random().Next(100);
        var mrNumber = 1000 + new Random().Next(999);
        var testingDate = DateTime.Today.ToString("yyyy-MM-dd");
        var reportId = "MTT" + new Random().Next(9999);
        var testItemId = new Random().Next(10);
        var reportDueDate = DateTime.Today.ToString("yyyy-MM-dd");
        var sql = $@"
        INSERT INTO reports (
            report_id, medical_order, consent_form_state, specimen_dely_state, send_email_state, product_name,
            tracking_status, notification_status, partition_id, department_id, submission_date, received_date,
            name, id_number, mr_number,test_item_id, cost, return_date, sending_physician_name, remark, report_date,
            report_results, create_id, modify_id, specimen_number, specimen_type, testing_date, inspection_progress,
            assessment_status, weeks_of_pregnancy, due_date,report_due_date, inspection_institution, inspection_institution_phone,
            responsible_business_person, responsible_business_phone, responsible_business_email, business_manager,
            business_manager_phone, business_manager_email, abnormal_report_delivery_method,
            abnormal_report_notification_method, inspection_group, notification_circumstances,
            prenatal_testing_project_tracking_time, confirm_specimen_submission_time, confirm_specimen_type,
            confirm_the_test_report_results, tracking_time, tracking, tracking_results,
            tracking_the_followup_status_of_NIPS_cases, referral_institution, referring_physician,
            written_report_processing_methood, fmr1_report_results, chr_report_date, chr_report_results,
            wafer_report_date, wafer_report_results, v2_v3_testing_results, gene_report_date, gene_report_results,
            other_report_date, other_report_results
        ) VALUES (
            '{reportId}', 'MO001', '已簽', '已送達', '已發送', '產品A',
            '待追蹤', '待通知', 1, 1, NOW(), NOW(),
            '{name}', 'A123456789', {mrNumber},'{testItemId}', 5000, NOW(), '醫師A', '備註內容',
            CURDATE() - INTERVAL 25 DAY,
            '異常', 1, 1, 'SP0001', '血液', '{testingDate}', '進行中',
            '重大', 12, CURDATE(),'{reportDueDate}', '台北馬偕', 0223456789,
            '業務A', 0912345678, 'sales@example.com', '主管A',
            0923456789, 'manager@example.com', '郵寄',
            '電話', '組別1', '情形1',
            NOW(), NOW(), '血液',
            '陽性', NOW(), '追蹤中', '已追蹤',
            NOW(), '轉介院所A', '轉介醫師A',
            '親送', '陽性', NOW(), '異常',
            NOW(), '異常', '陰性', NOW(), '異常',
            NOW(), '異常'
        );
        ";

        _db.Execute(sql);

        return Ok(new {
            success = true,
            name,
            mrNumber,
            testingDate
        });

    }
}