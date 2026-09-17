using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TGDay.Migrations
{
    /// <inheritdoc />
    public partial class AddIsArchivedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "patient_medication_logs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "patient_medication_logs");
        }
    }
}
