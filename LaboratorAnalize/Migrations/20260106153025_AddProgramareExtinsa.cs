using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramareExtinsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observatii",
                table: "Programare",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OraProgramare",
                table: "Programare",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observatii",
                table: "Programare");

            migrationBuilder.DropColumn(
                name: "OraProgramare",
                table: "Programare");
        }
    }
}
