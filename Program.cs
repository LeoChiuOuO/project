

using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Repository;
using WebApplication_Dianthus.Models.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 註冊你的服務
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
    
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
