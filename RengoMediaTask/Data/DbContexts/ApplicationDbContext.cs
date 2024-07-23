using Microsoft.EntityFrameworkCore;
using RengoMediaTask.Data.DomianModels;

namespace RengoMediaTask.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId);
        }
    }
}
