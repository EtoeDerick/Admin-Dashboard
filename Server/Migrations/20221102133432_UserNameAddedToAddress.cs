using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Server.Migrations
{
    public partial class UserNameAddedToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Addresses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Addresses");
        }
    }
}
