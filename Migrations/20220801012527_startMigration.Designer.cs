// <auto-generated />
using System;
using ERCTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERCTest.Migrations
{
    [DbContext(typeof(ERContext))]
    [Migration("20220801012527_startMigration")]
    partial class startMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ERCTest.EECounterDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasInHome")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EECountersDay");
                });

            modelBuilder.Entity("ERCTest.GVSCounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasInHome")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GVSCounters");
                });

            modelBuilder.Entity("ERCTest.HVSCounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasInHome")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("HVSCounters");
                });

            modelBuilder.Entity("ERCTest.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EEDeviceDayId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EEDeviceNightId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GVSDeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GVSTECounterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HVSDeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HomeNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OwnerName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ResidientsCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EEDeviceDayId");

                    b.HasIndex("EEDeviceNightId");

                    b.HasIndex("GVSDeviceId");

                    b.HasIndex("GVSTECounterId");

                    b.HasIndex("HVSDeviceId");

                    b.ToTable("Homes");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.EECounterNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasInHome")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EECountersNight");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.GVSTECounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasInHome")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GVSTECounters");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.Measurment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AmountOfConsumption")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CheckTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("CountOfResident")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EECounterDayId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EECounterNightId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GVSCounterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GVSTECounterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("HVSCounterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EECounterDayId");

                    b.HasIndex("EECounterNightId");

                    b.HasIndex("GVSCounterId");

                    b.HasIndex("GVSTECounterId");

                    b.HasIndex("HVSCounterId");

                    b.ToTable("Measurments");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.Tariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ServiceName")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TariffPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TariffWithoutCouner")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitOfMeasurment")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("ERCTest.Home", b =>
                {
                    b.HasOne("ERCTest.EECounterDay", "EEDeviceDay")
                        .WithMany()
                        .HasForeignKey("EEDeviceDayId");

                    b.HasOne("ERCTest.Models.Counters.EECounterNight", "EEDeviceNight")
                        .WithMany()
                        .HasForeignKey("EEDeviceNightId");

                    b.HasOne("ERCTest.GVSCounter", "GVSDevice")
                        .WithMany()
                        .HasForeignKey("GVSDeviceId");

                    b.HasOne("ERCTest.Models.Counters.GVSTECounter", "GVSTECounter")
                        .WithMany()
                        .HasForeignKey("GVSTECounterId");

                    b.HasOne("ERCTest.HVSCounter", "HVSDevice")
                        .WithMany()
                        .HasForeignKey("HVSDeviceId");

                    b.Navigation("EEDeviceDay");

                    b.Navigation("EEDeviceNight");

                    b.Navigation("GVSDevice");

                    b.Navigation("GVSTECounter");

                    b.Navigation("HVSDevice");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.Measurment", b =>
                {
                    b.HasOne("ERCTest.EECounterDay", null)
                        .WithMany("Measurments")
                        .HasForeignKey("EECounterDayId");

                    b.HasOne("ERCTest.Models.Counters.EECounterNight", null)
                        .WithMany("Measurments")
                        .HasForeignKey("EECounterNightId");

                    b.HasOne("ERCTest.GVSCounter", null)
                        .WithMany("Measurments")
                        .HasForeignKey("GVSCounterId");

                    b.HasOne("ERCTest.Models.Counters.GVSTECounter", null)
                        .WithMany("Measurments")
                        .HasForeignKey("GVSTECounterId");

                    b.HasOne("ERCTest.HVSCounter", null)
                        .WithMany("Measurments")
                        .HasForeignKey("HVSCounterId");
                });

            modelBuilder.Entity("ERCTest.EECounterDay", b =>
                {
                    b.Navigation("Measurments");
                });

            modelBuilder.Entity("ERCTest.GVSCounter", b =>
                {
                    b.Navigation("Measurments");
                });

            modelBuilder.Entity("ERCTest.HVSCounter", b =>
                {
                    b.Navigation("Measurments");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.EECounterNight", b =>
                {
                    b.Navigation("Measurments");
                });

            modelBuilder.Entity("ERCTest.Models.Counters.GVSTECounter", b =>
                {
                    b.Navigation("Measurments");
                });
#pragma warning restore 612, 618
        }
    }
}
