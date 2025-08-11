using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Repository
{
    public class ReportRepository : IReportRepository
    {
        
        // 範例模擬DB
        private static readonly List<Report> _reposts = new List<Report>
        {
            new Report { Number = 001, TestID=123456, CustName = "客戶01" , Item = "item01", DetectionDate = new DateTime(2025,01,04), ReportDate = new DateTime(2025,01,04), DetectionDepartment="院所01", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 002, TestID=654321, CustName = "客戶02" , Item = "item02", DetectionDate = new DateTime(2025,02,13), ReportDate = new DateTime(2025,02,13), DetectionDepartment="院所02", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 003, TestID=132465, CustName = "客戶03" , Item = "item03", DetectionDate = new DateTime(2025,05,24), ReportDate = new DateTime(2025,05,24), DetectionDepartment="院所03", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 004, TestID=142356, CustName = "客戶04" , Item = "item04", DetectionDate = new DateTime(2025,02,06), ReportDate = new DateTime(2025,02,06), DetectionDepartment="院所04", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 005, TestID=512436, CustName = "客戶05" , Item = "item05", DetectionDate = new DateTime(2025,03,09), ReportDate = new DateTime(2025,03,09), DetectionDepartment="院所05", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 006, TestID=645321, CustName = "客戶06" , Item = "item06", DetectionDate = new DateTime(2025,08,22), ReportDate = new DateTime(2025,08,22), DetectionDepartment="院所06", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 007, TestID=124356, CustName = "客戶07" , Item = "item07", DetectionDate = new DateTime(2025,07,18), ReportDate = new DateTime(2025,07,18), DetectionDepartment="院所07", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 008, TestID=213465, CustName = "客戶08" , Item = "item08", DetectionDate = new DateTime(2025,02,26), ReportDate = new DateTime(2025,02,26), DetectionDepartment="院所08", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 009, TestID=314265, CustName = "客戶09" , Item = "item09", DetectionDate = new DateTime(2025,04,11), ReportDate = new DateTime(2025,04,11), DetectionDepartment="院所09", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 010, TestID=162534, CustName = "客戶10" , Item = "item10", DetectionDate = new DateTime(2025,09,19), ReportDate = new DateTime(2025,09,19), DetectionDepartment="院所10", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
            new Report { Number = 011, TestID=342516, CustName = "客戶11" , Item = "item11", DetectionDate = new DateTime(2025,11,30), ReportDate = new DateTime(2025,11,30), DetectionDepartment="院所11", DetectionDoctor = "XXX醫師", Notify = 1 , Track = 2, Edit = 3, Review = 4, Describe = 5, Document = 6},
        };

        public void Create(Report instance)
        {
            throw new NotImplementedException();
        }

        public void Edit(Report instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(Report instance)
        {
            throw new NotImplementedException();
        }

        public void GetReportByID(Report instance)
        {
            throw new NotImplementedException();
        }

        public List<Report> GetAll()
        {
            return _reposts;
        }

        public void Update(Report instance)
        {
            throw new NotImplementedException();
        }

        public Report GetReportByID(int ReportID)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}