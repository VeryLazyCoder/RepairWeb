using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReportNowContainsPropertiesForDaysAndHoursSeparately : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSpent",
                table: "Reports",
                newName: "DurationTime");

            migrationBuilder.AddColumn<int>(
                name: "DurationDays",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationDays",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "DurationTime",
                table: "Reports",
                newName: "TimeSpent");
        }
    }
}
