using Microsoft.EntityFrameworkCore.Migrations;

namespace ScrumBlazor.Migrations
{
    public partial class EstimateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estimate",
                table: "Members",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "Members");
        }
    }
}
