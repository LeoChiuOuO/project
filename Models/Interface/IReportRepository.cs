using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace WebApplication_Dianthus.Models.Interface
{
    public interface IReportRepository
    {
        void Create(Report instance);
 
        void Update(Report instance);
 
        void Delete(Report instance);
 
        Report GetReportByID(int ReportID);
 
        List<Report> GetAll();
 
        void SaveChanges();
    }
}