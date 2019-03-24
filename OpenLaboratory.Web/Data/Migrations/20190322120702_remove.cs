using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenLaboratory.Web.Data.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyOfficerId",
                table: "Laboratories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DutyOfficerId",
                table: "Laboratories",
                nullable: false,
                defaultValue: 0);
        }
    }
}
