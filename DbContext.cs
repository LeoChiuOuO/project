using Microsoft.EntityFrameworkCore;
using WebApplication_Dianthus.Models;
public class AppDbContext : DbContext
{public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Partition> Partitions { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<TestItem> TestItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 設定資料表名稱與主鍵
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Permission>().HasKey(p => p.Id);
        modelBuilder.Entity<RolePermission>().HasKey(rp => rp.Id);
        modelBuilder.Entity<UserRole>().HasKey(ur => ur.Id);
        modelBuilder.Entity<Partition>().HasKey(pa => pa.Id);
        modelBuilder.Entity<Department>().HasKey(d => d.Id);
        modelBuilder.Entity<Group>().HasKey(g => g.Id);
        modelBuilder.Entity<Report>().HasKey(r => r.Id);
        modelBuilder.Entity<TestItem>().HasKey(t => t.Id);

        // 設定關聯
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionsId);

        modelBuilder.Entity<Department>()
            .HasOne(d => d.Partition)
            .WithMany()
            .HasForeignKey(d => d.PartitionId);

        modelBuilder.Entity<Group>()
            .HasOne(g => g.Department)
            .WithMany()
            .HasForeignKey(g => g.DepartmentId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.TestItem)
            .WithMany(t => t.Reports)
            .HasForeignKey(r => r.TestItemId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.Department)
            .WithMany(d => d.Reports)
            .HasForeignKey(r => r.DepartmentId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.Partition)
            .WithMany(p => p.Reports)
            .HasForeignKey(r => r.PartitionId);

        modelBuilder.Entity<Report>()
        .HasOne(r => r.Group)
        .WithMany(g => g.Reports)
        .HasForeignKey(r => r.GroupId)
        .HasConstraintName("FK_Report_Group")
        .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<OperationLog>()
        .Property(o => o.CreatedAt)
        .HasColumnName("created_at")
        .HasDefaultValueSql("CURRENT_TIMESTAMP");


    }
}