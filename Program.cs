

using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Repository;
using WebApplication_Dianthus.Models.Service;
using WebApplication_Dianthus.Models.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// 註冊你的服務
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IOBPatientService, OBPatientService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();// 啟用身份驗證
app.UseAuthorization(); // 啟用授權，指的是Controller、Action可加上驗證 [Authorize] 屬性

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Index}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
