using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Requests_RequestId1",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_RequestId1",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "RequestId1",
                table: "Notification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestId1",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notification_RequestId1",
                table: "Notification",
                column: "RequestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Requests_RequestId1",
                table: "Notification",
                column: "RequestId1",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
