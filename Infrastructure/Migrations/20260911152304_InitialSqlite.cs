using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TGDay.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patient_medication_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MedicationName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Milligrams = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    WeightKg = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    ScheduledDay = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ScheduledTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    DoseCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    TakenAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_medication_logs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patient_medication_logs_PatientName_ScheduledDay",
                table: "patient_medication_logs",
                columns: new[] { "PatientName", "ScheduledDay" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patient_medication_logs");
        }
    }
}
