
// ==========================================
// 1. Model/ViewModel
// ==========================================

// Models/OBPatientRecord.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Dianthus.Models;

namespace WebApplication_Dianthus.Models
{
    public class OBPatientRecord
    {
        [Display(Name = "病歷號")]
        public string 病歷號 { get; set; } = string.Empty;

        [Display(Name = "姓名")]
        public string 姓名 { get; set; } = string.Empty;

        [Display(Name = "床號")]
        public string 床號 { get; set; } = string.Empty;

        [Display(Name = "入住日期")]
        [DataType(DataType.DateTime)]
        public DateTime? 入住日期 { get; set; }

        [Display(Name = "離開日期")]
        [DataType(DataType.DateTime)]
        public DateTime? 離開日期 { get; set; }

        [Display(Name = "分娩方式")]
        public string 分娩方式 { get; set; } = string.Empty;

        [Display(Name = "產後床號")]
        public string 產後床號 { get; set; } = string.Empty;

        [Display(Name = "產後床入住日期")]
        [DataType(DataType.DateTime)]
        public DateTime? 產後床入住日期 { get; set; }

        [Display(Name = "產後床離開日期")]
        [DataType(DataType.DateTime)]
        public DateTime? 產後床離開日期 { get; set; }

        [Display(Name = "原始入住日期")]
        [DataType(DataType.DateTime)]
        public DateTime? 原始入住日期 { get; set; }

        [Display(Name = "轉床否")]
        public string 轉床否 { get; set; } = string.Empty;
    }
}