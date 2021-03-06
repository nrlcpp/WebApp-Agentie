﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class ReservationsDbContext : IdentityDbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Remarks> Remarks { get; set; }

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

            //many to many connection Traveller -Reservation
            //modelBuilder.Entity<ReservationTraveller>()
            //    .HasKey(sc => new { sc.TravellerId, sc.ReservationId });

            base.OnModelCreating(modelBuilder);

        }
    }
}


