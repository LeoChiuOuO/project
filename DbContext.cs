using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Report> Reports { get; set; }
    // 這裡一個表配一個 DbSet<你的model>
}