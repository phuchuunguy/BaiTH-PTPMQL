using DemoMVC.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<DaiLy> DaiLys { get; set; }
        public DbSet<HeThongPhanPhoi> HeThongPhanPhois { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DaiLy>()
                .HasOne(d => d.HeThongPhanPhoi)
                .WithMany(h => h.DaiLys)
                .HasForeignKey(d => d.MaHTPP)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}