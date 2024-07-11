using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Server.Migrations
{
    public partial class downloadzilfileforallyearpapers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipFileUrl",
                table: "Downloadpdfs",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipFileUrl",
                table: "Downloadpdfs");
        }
    }
}
