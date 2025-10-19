using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialValueToGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitialValue",
                table: "Goals",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialValue",
                table: "Goals");
        }
    }
}
