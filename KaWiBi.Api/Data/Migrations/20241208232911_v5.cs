using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaWiBi.Api.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Department_DepartmentId",
                table: "Asset");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "IdentityUser",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "IdentityUser",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Department_DepartmentId",
                table: "Asset",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Department_DepartmentId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "IdentityUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Department_DepartmentId",
                table: "Asset",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
