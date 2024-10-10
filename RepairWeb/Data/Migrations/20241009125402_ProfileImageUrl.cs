using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfileImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageURL",
                table: "AspNetUsers");
        }
    }
}
