using System;
using System.Collections.Generic;
using System.Text;
using ERCTest.Models.Counters;
using Microsoft.EntityFrameworkCore;

namespace ERCTest
{
    class ERContext : DbContext
    {
        public DbSet<Home> Homes { get; set; }
        public DbSet<EECounterDay> EECountersDay { get; set; }
        public DbSet<EECounterNight> EECountersNight { get; set; }
        public DbSet<GVSCounter> GVSCounters { get; set; }
        public DbSet<HVSCounter> HVSCounters { get; set; }
        public DbSet<Measurment> Measurments { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        public ERContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ERCTest.db");
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Home>().ToTable("Homes");
            modelBuilder.Entity<EECounterDay>().ToTable("EECountersDay");
            modelBuilder.Entity<EECounterNight>().ToTable("EECountersNight");
            modelBuilder.Entity<GVSCounter>().ToTable("GVSCounters");
            modelBuilder.Entity<HVSCounter>().ToTable("HVSCounters");
            modelBuilder.Entity<Measurment>().ToTable("Measurments");
            modelBuilder.Entity<Tariff>().ToTable("Tariffs");
        }
    }
}
