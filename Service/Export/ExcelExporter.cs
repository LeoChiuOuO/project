using WebApplication_Dianthus.Models.DTO;

namespace WebApplication_Dianthus.Models.Service.Export
{
    public class ExcelExporter
    {
        public byte[] ExportReports(IEnumerable<ReportDTO> reports)
        {
            using var wb = new ClosedXML.Excel.XLWorkbook();
            var ws = wb.Worksheets.Add("Reports");

            // 表頭
            ws.Cell(1, 1).Value = "檢體編號";
            ws.Cell(1, 2).Value = "姓名";
            ws.Cell(1, 3).Value = "檢測項目";
            ws.Cell(1, 4).Value = "檢驗日期";
            ws.Cell(1, 5).Value = "通知狀態";
            ws.Cell(1, 6).Value = "追蹤狀態";

            // 資料
            int row = 2;
            foreach (var r in reports)
            {
                ws.Cell(row, 1).Value = r.SpecimenNumber;
                ws.Cell(row, 2).Value = r.Name;
                ws.Cell(row, 3).Value = r.TestItemName;
                ws.Cell(row, 4).Value = r.TestingDate;
                ws.Cell(row, 5).Value = r.NotificationStatus;
                ws.Cell(row, 6).Value = r.TrackingStatus;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var ms = new MemoryStream();
            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}