using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.DTO;

public class ExcelExporter
{
    public byte[] ExportReports(IEnumerable<ReportDTO> rows)
    {
        using var wb = new ClosedXML.Excel.XLWorkbook();
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
            // ws.Cell(r, 1).Value = x.SpecimenNumber;
            // ws.Cell(r, 2).Value = x.Name;
            // ws.Cell(r, 3).Value = x.TestItemName;
            // ws.Cell(r, 4).Value = x.TestingDate;   ws.Cell(r, 4).Style.DateFormat.Format = "yyyy-MM-dd";
            // ws.Cell(r, 5).Value = x.ReportDate;    ws.Cell(r, 5).Style.DateFormat.Format = "yyyy-MM-dd";
            // ws.Cell(r, 6).Value = x.NotificationStatus;
            // ws.Cell(r, 7).Value = x.TrackingStatus;
            // ws.Cell(r, 8).Value = x.InspectionInstitution;
            // ws.Cell(r, 9).Value = x.SendingPhysicianName;
            // ws.Cell(r,10).Value = x.Remark;
            r++;
        }

        ws.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        wb.SaveAs(ms);
        return ms.ToArray();
    }

    internal byte[] ExportReports(IEnumerable<Report> reports)
    {
        throw new NotImplementedException();
    }
}
