using ClosedXML.Excel;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;

public class ExcelExporter
{
    public byte[] ExportReports(IEnumerable<ReportDTO> rows)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Reports");

        // 表頭
        var headers = new[]
        {
            "檢體編號","姓名","檢測項目","檢驗日期","回報日期",
            "通知狀態","追蹤狀態","醫療院所","主治醫師","備註"
        };
        for (int i = 0; i < headers.Length; i++)
            ws.Cell(1, i + 1).Value = headers[i];

        // 資料
        int r = 2;
        foreach (var x in rows)
        {
            ws.Cell(r, 1).Value = x.SpecimenNumber;
            ws.Cell(r, 2).Value = x.Name;
            ws.Cell(r, 3).Value = x.TestItemName;
            ws.Cell(r, 4).Value = x.TestingDate;   ws.Cell(r, 4).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 5).Value = x.ReportDate;    ws.Cell(r, 5).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 6).Value = x.NotificationStatus;
            ws.Cell(r, 7).Value = x.TrackingStatus;
            ws.Cell(r, 8).Value = x.InspectionInstitution;
            ws.Cell(r, 9).Value = x.SendingPhysicianName;
            ws.Cell(r,10).Value = x.Remark;
            r++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        return ms.ToArray();
    }

    internal byte[] ExportReports(IEnumerable<Report> reports)
    {
        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("完整報告");

        // 表頭（可根據 Report 欄位調整）
        var headers = new[]
        {
            "檢驗報告序號", "醫令", "同意書簽核狀態", "檢體傳送狀態", "發信狀態", "檢驗產品名稱",
            "追蹤狀態", "通知狀態", "公司", "單位", "送檢日期", "姓名", "身分證", "病歷號", "檢測項目",
            "費用", "回診日期", "送檢醫師", "附註", "報告日期", "報告結果", "建立者編號", "修改人員",
            "檢體編號", "採檢日期", "懷孕週數", "預產期", "送檢院所", "送檢院所電話", "負責業務", "負責業務電話",
            "負責業務Email", "業務主管", "業務主管電話", "業務主管Email", "異常報告寄送方式", "異常報告通知方式",
            "檢驗組別", "通知情形", "產前檢測項目追蹤時間", "Confirm檢體進件時間", "Confirm檢體類別", "Confirm檢體報告結果",
            "追蹤時間", "追蹤情形", "追蹤結果", "追蹤NIPS個案後續狀況時間", "轉介院所", "轉介醫師", "書面報告處理方式",
            "FMR1結果", "染色體報告日期", "染色體報告結果", "晶片報告日期", "晶片結果", "V2.0+V3.0基因結果", "基因檢測報告日期",
            "基因檢測報告結果", "其他檢測報告日期", "其他檢測報告結果", 
        };
        for (int i = 0; i < headers.Length; i++)
            ws.Cell(1, i + 1).Value = headers[i];
        
        // 資料列
        int r = 2;
        foreach (var x in reports)
        {
            ws.Cell(r, 1).Value = x.ReportId;
            ws.Cell(r, 2).Value = x.MedicalOrder;
            ws.Cell(r, 3).Value = x.ConsentFormState;
            ws.Cell(r, 4).Value = x.SpecimenDelyState;
            ws.Cell(r, 5).Value = x.SendEmailState;
            ws.Cell(r, 6).Value = x.ProductName;
            ws.Cell(r, 7).Value = x.TrackingStatus;
            ws.Cell(r, 8).Value = x.NotificationStatus;
            ws.Cell(r, 9).Value = x.Partition?.Name ??"";
            ws.Cell(r, 10).Value = x.Department?.Name ??"";
            ws.Cell(r, 11).Value = x.SubmissionDate;
            ws.Cell(r, 13).Value = x.Name;
            ws.Cell(r, 14).Value = x.IdNumber;
            ws.Cell(r, 15).Value = x.MrNumber;
            ws.Cell(r, 16).Value = x.TestItem?.name ?? ""; // 若已 Include TestItem
            ws.Cell(r, 17).Value = x.Cost;
            ws.Cell(r, 18).Value = x.ReturnDate;  ws.Cell(r,18).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 19).Value = x.SendingPhysicianName;
            ws.Cell(r, 20).Value = x.Remark;
            ws.Cell(r, 21).Value = x.ReportDate;  ws.Cell(r, 22).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 22).Value = x.ReportResults;
            ws.Cell(r, 23).Value = x.CreateId;
            ws.Cell(r, 24).Value = x.ModifyId;
            ws.Cell(r, 25).Value = x.SpecimenNumber;
            ws.Cell(r, 26).Value = x.TestingDate;  ws.Cell(r, 26).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 27).Value = x.WeeksOfPregnancy;
            ws.Cell(r, 28).Value = x.DueDate;  ws.Cell(r, 28).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 29).Value = x.InspectionInstitution;
            ws.Cell(r, 30).Value = x.InspectionInstitutionPhone;
            ws.Cell(r, 31).Value = x.ResponsibleBusinessPerson;
            ws.Cell(r, 32).Value = x.ResponsibleBusinessPhone;
            ws.Cell(r, 33).Value = x.ResponsibleBusinessEmail;
            ws.Cell(r, 34).Value = x.BusinessManager;
            ws.Cell(r, 35).Value = x.BusinessManagerPhone;
            ws.Cell(r, 36).Value = x.BusinessManagerEmail;
            ws.Cell(r, 37).Value = x.AbnormalReportDeliveryMethod;
            ws.Cell(r, 38).Value = x.AbnormalReportNotificationMethod;
            ws.Cell(r, 39).Value = x.InspectionGroup;
            ws.Cell(r, 40).Value = x.NotificationCircumstances;
            ws.Cell(r, 41).Value = x.PrenatalTestingProjectTrackingTime;  ws.Cell(r, 41).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";
            ws.Cell(r, 42).Value = x.ConfirmSpecimenSubmissionTime;  ws.Cell(r, 42).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";
            ws.Cell(r, 43).Value = x.ConfirmSpecimenType;
            ws.Cell(r, 44).Value = x.ConfirmTheTestReportResults;
            ws.Cell(r, 45).Value = x.TrackingTime;  ws.Cell(r, 45).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";
            ws.Cell(r, 46).Value = x.Tracking;
            ws.Cell(r, 47).Value = x.TrackingResults;
            ws.Cell(r, 48).Value = x.TrackingTheFollowupStatusOfNIPS_Cases;
            ws.Cell(r, 49).Value = x.ReferralInstitution;
            ws.Cell(r, 50).Value = x.ReferringPhysician;
            ws.Cell(r, 51).Value = x.WrittenReportProcessingMethood;
            ws.Cell(r, 52).Value = x.Fmr1ReportResults;
            ws.Cell(r, 53).Value = x.ChrReportDate;  ws.Cell(r, 53).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 54).Value = x.ChrReportResults;
            ws.Cell(r, 55).Value = x.WaferReportDate;  ws.Cell(r, 55).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 56).Value = x.WaferReportResults;
            ws.Cell(r, 57).Value = x.V2V3TestingResults;
            ws.Cell(r, 58).Value = x.GeneReportDate;  ws.Cell(r, 58).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 59).Value = x.GeneReportResults;
            ws.Cell(r, 60).Value = x.OtherReportDate;  ws.Cell(r, 60).Style.DateFormat.Format = "yyyy-MM-dd";
            ws.Cell(r, 60).Value = x.OtherReportResults;

            r++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        return ms.ToArray();
    }

}
