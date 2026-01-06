using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class AddCnpToPacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNP",
                table: "Pacient",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNP",
                table: "Pacient");
        }
    }
}
