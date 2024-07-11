using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Server.Migrations
{
    public partial class AnnouncementAppUpdateFeatureList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdateFeatures",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateFeatures",
                table: "Announcements");
        }
    }
}
