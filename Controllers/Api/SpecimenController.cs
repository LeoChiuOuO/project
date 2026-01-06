using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DocumentFormat.OpenXml.EMMA;
using K4os.Compression.LZ4.Streams.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Ocsp;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Controllers.api
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class SpecimenController: ControllerBase
    {
        private readonly IReportService _reportService;


        public SpecimenController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpPost]
        public async Task<IActionResult> ReceiveSpecimen([FromBody] SpecimenRequest request)
        {
           
            if (request == null)
            {
                return BadRequest(new {message ="Invalid report data"});
            }
            //新增資料
            var report = new Report();
            report.MedicalOrder = request.uuid;
            report.MrNumber = int.Parse(request.patientId);
            report.Name = request.patient;
            report.IdNumber = request.twid;
            report.PartitionId = PartitionNameToID(request.clinic);
            report.ReferringPhysician = request.doctor;
            report.SendingPhysicianName = request.doctor;
            report.ProductName = request.title;
            report.TestingDate= new DateTime(int.Parse(request.collectionDate));
            report.ReceivedDate = new DateTime(int.Parse(request.receivedDate));
            report.ReportDate= new DateTime(int.Parse(request.reportDate));


            _reportService.CreateReport(report);
           
           
            return Ok(new {message="資料已接收成功", data = request});
        }


        // 發送測試
        [HttpGet("report-sent")]
        public async Task<IActionResult> SentReport(string CG = "ALL")
        {
            string apiUrl = "https://192.168.1.125:5001/api/specimen";


            var specimen = new SpecimenRequest
            {
                uuid = "2500099053",
                patientId = "0687747",
                patient = "劉昏君",
                twid = "L224530043",
                birthday = "1996-07-13",
                clinic = "禾馨安和婦幼診所",
                doctor = "蘇怡寧",
                title = "慧智帶因篩檢 v3.0(新)",
                collectionDate = "2025-11-25",
                receivedDate = "2025-11-27",
                reportDate = "2025-12-16",
                type = "pdf",
                endpoint = "https://api.sofiva.com.tw/download?token=..."
            };


            string json = JsonSerializer.Serialize(specimen);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            string status = "0";
            string responseBody = string.Empty;


            using var client = new HttpClient();
            try
            {
                var response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response received:");
                Console.WriteLine(responseBody);
                status = "1";
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                responseBody = ex.Message;
                status = "2";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                responseBody = ex.Message;
                status = "3";
            }


            return Ok(new { status, responseBody });


        }


        static int PartitionNameToID(string info)
        {
            switch (info)
            {
                case var s when s.Contains("禾馨"):
                    return 1;
                case var s when s.Contains("慧智"):
                    return 2;
                case var s when s.Contains("馬偕"):
                    return 3;
                case var s when s.Contains("新光"):
                    return 4;
                case var s when s.Contains("中榮"):
                    return 5;
                default:
                    return 0; // 預設值，表示未匹配任何條件
            }
        }
    }
}
