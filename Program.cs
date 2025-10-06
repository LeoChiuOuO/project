

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Profiles;
using WebApplication_Dianthus.Models.Repository;
using WebApplication_Dianthus.Models.Service;
using WebApplication_Dianthus.Models.Service.Interface;
using WebApplication_Dianthus.Services;

var builder = WebApplication.CreateBuilder(args);

// 加入 MVC
builder.Services.AddControllersWithViews();

// MySQL 連線字串（appsettings.json）
var cs = builder.Configuration.GetConnectionString("DefaultConnection");

// 每個 HTTP 請求建立新的 MySQL 連線並開啟
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var conn = new MySqlConnection(cs);
    conn.Open();
    return conn;
});


// 加入 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 36))));

// 加入 Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 註冊服務
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

builder.Services.AddTransient<ExcelExporter>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IOBPatientService, OBPatientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddAutoMapper(typeof(ReportProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 中介軟體順序
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // 這行很重要，讓 CSS/JS/圖片能被載入
app.UseRouting();
app.UseSession();

// 如果有登入驗證，這裡要加 app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // 包含 /api/report

// 路由設定
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
