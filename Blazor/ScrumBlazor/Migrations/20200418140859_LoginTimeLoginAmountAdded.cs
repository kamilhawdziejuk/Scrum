using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScrumBlazor.Migrations
{
    public partial class LoginTimeLoginAmountAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginAmount",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginTime",
                table: "Teams",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginAmount",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LoginTime",
                table: "Teams");
        }
    }
}
