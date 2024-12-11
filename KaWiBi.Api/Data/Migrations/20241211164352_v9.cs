using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaWiBi.Api.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "Executer",
                table: "Ticket",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(160)",
                oldMaxLength: 160);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Executer",
                table: "Ticket",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(160)",
                oldMaxLength: 160,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Ticket",
                type: "NVARCHAR(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "");
        }
    }
}
