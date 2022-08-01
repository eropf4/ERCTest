using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERCTest.Migrations
{
    public partial class startMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EECountersDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasInHome = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EECountersDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EECountersNight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasInHome = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EECountersNight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GVSCounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasInHome = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GVSCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GVSTECounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasInHome = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GVSTECounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HVSCounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HasInHome = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HVSCounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceName = table.Column<string>(type: "TEXT", nullable: true),
                    TariffPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TariffWithoutCouner = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitOfMeasurment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerName = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    HomeNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    EEDeviceDayId = table.Column<int>(type: "INTEGER", nullable: true),
                    EEDeviceNightId = table.Column<int>(type: "INTEGER", nullable: true),
                    GVSDeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    HVSDeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    GVSTECounterId = table.Column<int>(type: "INTEGER", nullable: true),
                    ResidientsCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homes_EECountersDay_EEDeviceDayId",
                        column: x => x.EEDeviceDayId,
                        principalTable: "EECountersDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homes_EECountersNight_EEDeviceNightId",
                        column: x => x.EEDeviceNightId,
                        principalTable: "EECountersNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homes_GVSCounters_GVSDeviceId",
                        column: x => x.GVSDeviceId,
                        principalTable: "GVSCounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homes_GVSTECounters_GVSTECounterId",
                        column: x => x.GVSTECounterId,
                        principalTable: "GVSTECounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homes_HVSCounters_HVSDeviceId",
                        column: x => x.HVSDeviceId,
                        principalTable: "HVSCounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Measurments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountOfConsumption = table.Column<decimal>(type: "TEXT", nullable: false),
                    CheckTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CountOfResident = table.Column<int>(type: "INTEGER", nullable: false),
                    EECounterDayId = table.Column<int>(type: "INTEGER", nullable: true),
                    EECounterNightId = table.Column<int>(type: "INTEGER", nullable: true),
                    GVSCounterId = table.Column<int>(type: "INTEGER", nullable: true),
                    GVSTECounterId = table.Column<int>(type: "INTEGER", nullable: true),
                    HVSCounterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurments_EECountersDay_EECounterDayId",
                        column: x => x.EECounterDayId,
                        principalTable: "EECountersDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurments_EECountersNight_EECounterNightId",
                        column: x => x.EECounterNightId,
                        principalTable: "EECountersNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurments_GVSCounters_GVSCounterId",
                        column: x => x.GVSCounterId,
                        principalTable: "GVSCounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurments_GVSTECounters_GVSTECounterId",
                        column: x => x.GVSTECounterId,
                        principalTable: "GVSTECounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Measurments_HVSCounters_HVSCounterId",
                        column: x => x.HVSCounterId,
                        principalTable: "HVSCounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homes_EEDeviceDayId",
                table: "Homes",
                column: "EEDeviceDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_EEDeviceNightId",
                table: "Homes",
                column: "EEDeviceNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_GVSDeviceId",
                table: "Homes",
                column: "GVSDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_GVSTECounterId",
                table: "Homes",
                column: "GVSTECounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_HVSDeviceId",
                table: "Homes",
                column: "HVSDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurments_EECounterDayId",
                table: "Measurments",
                column: "EECounterDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurments_EECounterNightId",
                table: "Measurments",
                column: "EECounterNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurments_GVSCounterId",
                table: "Measurments",
                column: "GVSCounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurments_GVSTECounterId",
                table: "Measurments",
                column: "GVSTECounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurments_HVSCounterId",
                table: "Measurments",
                column: "HVSCounterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Measurments");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "EECountersDay");

            migrationBuilder.DropTable(
                name: "EECountersNight");

            migrationBuilder.DropTable(
                name: "GVSCounters");

            migrationBuilder.DropTable(
                name: "GVSTECounters");

            migrationBuilder.DropTable(
                name: "HVSCounters");
        }
    }
}
