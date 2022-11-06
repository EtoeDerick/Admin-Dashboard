using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Server.Migrations
{
    public partial class ExamCategoryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.AddColumn<string>(
                name: "CategoryBgColor",
                table: "ExamCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryTextColor",
                table: "ExamCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ExamCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryBgColor",
                table: "ExamCategories");

            migrationBuilder.DropColumn(
                name: "CategoryTextColor",
                table: "ExamCategories");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ExamCategories");
        }
    }
}
