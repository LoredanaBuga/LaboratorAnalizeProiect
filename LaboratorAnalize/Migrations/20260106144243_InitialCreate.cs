using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PachetAnalize",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PachetAnalize", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Programare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataProgramare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacientID = table.Column<int>(type: "int", nullable: true),
                    PachetAnalizeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Programare_PachetAnalize_PachetAnalizeID",
                        column: x => x.PachetAnalizeID,
                        principalTable: "PachetAnalize",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Programare_Pacient_PacientID",
                        column: x => x.PacientID,
                        principalTable: "Pacient",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programare_PachetAnalizeID",
                table: "Programare",
                column: "PachetAnalizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Programare_PacientID",
                table: "Programare",
                column: "PacientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programare");

            migrationBuilder.DropTable(
                name: "PachetAnalize");

            migrationBuilder.DropTable(
                name: "Pacient");
        }
    }
}
