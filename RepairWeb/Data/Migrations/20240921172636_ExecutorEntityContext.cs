using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExecutorEntityContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Executor_ExecutorId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executor",
                table: "Executor");

            migrationBuilder.RenameTable(
                name: "Executor",
                newName: "Executors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executors",
                table: "Executors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Executors_ExecutorId",
                table: "Requests",
                column: "ExecutorId",
                principalTable: "Executors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Executors_ExecutorId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executors",
                table: "Executors");

            migrationBuilder.RenameTable(
                name: "Executors",
                newName: "Executor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executor",
                table: "Executor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Executor_ExecutorId",
                table: "Requests",
                column: "ExecutorId",
                principalTable: "Executor",
                principalColumn: "Id");
        }
    }
}
