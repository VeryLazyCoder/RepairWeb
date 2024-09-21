using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDatesToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FulfillDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FulfillDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Requests");
        }
    }
}
