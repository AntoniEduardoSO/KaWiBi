using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaWiBi.Api.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Ticket",
                newName: "DepartmentToExecute");

            migrationBuilder.AddColumn<long>(
                name: "DepartmentOwner",
                table: "Ticket",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Executer",
                table: "Ticket",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Ticket",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentOwner",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Executer",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "DepartmentToExecute",
                table: "Ticket",
                newName: "DepartmentId");
        }
    }
}
