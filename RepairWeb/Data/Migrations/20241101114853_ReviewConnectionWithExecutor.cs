using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReviewConnectionWithExecutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExecutorId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReviewDate",
                table: "Review",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<float>(
                name: "AverageRating",
                table: "Executors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ExecutorId",
                table: "Review",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Executors_ExecutorId",
                table: "Review",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Executors_ExecutorId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ExecutorId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Executors");
        }
    }
}
