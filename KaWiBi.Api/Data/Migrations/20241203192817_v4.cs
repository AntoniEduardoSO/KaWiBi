using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaWiBi.Api.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                table: "Ticket",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedTimeToFinish",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "IdentityUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Lat = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: true),
                    Lon = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(180)", maxLength: 180, nullable: false),
                    Stamp = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    Pattern = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    SerialNumber = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    IpAddress = table.Column<byte[]>(type: "VARBINARY(16)", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_DepartmentId",
                table: "Asset",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EstimatedTimeToFinish",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "IdentityUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Ticket",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
