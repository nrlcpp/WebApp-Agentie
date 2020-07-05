using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ReservationsDbContext : DbContext
    {

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Remarks> Remarks { get; set; }

        public DbSet<User> Users { get; set; }
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many connection Reservation - Remarks
            modelBuilder.Entity<Reservation>()
                .HasMany(c => c.Remarks)
                .WithOne(e => e.Reservation)
                .OnDelete(DeleteBehavior.Cascade);

            //many to many connection Employee -Reservation
            //modelBuilder.Entity<ReservationEmployee>()
            //    .HasKey(sc => new { sc.EmployeeId, sc.ReservationId });
        }
    }
}


