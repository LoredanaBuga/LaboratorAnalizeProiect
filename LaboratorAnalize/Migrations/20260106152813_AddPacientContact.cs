using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class AddPacientContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pacient");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Pacient");
        }
    }
}
