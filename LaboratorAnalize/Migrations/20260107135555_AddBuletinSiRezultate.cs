using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class AddBuletinSiRezultate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuletinAnalize",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramareID = table.Column<int>(type: "int", nullable: true),
                    DataEliberare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observatii = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuletinAnalize", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BuletinAnalize_Programare_ProgramareID",
                        column: x => x.ProgramareID,
                        principalTable: "Programare",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RezultatAnaliza",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuletinAnalizeID = table.Column<int>(type: "int", nullable: true),
                    TipAnalizaID = table.Column<int>(type: "int", nullable: true),
                    Valoare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unitate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntervalReferinta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InLimite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezultatAnaliza", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RezultatAnaliza_BuletinAnalize_BuletinAnalizeID",
                        column: x => x.BuletinAnalizeID,
                        principalTable: "BuletinAnalize",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RezultatAnaliza_TipAnaliza_TipAnalizaID",
                        column: x => x.TipAnalizaID,
                        principalTable: "TipAnaliza",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuletinAnalize_ProgramareID",
                table: "BuletinAnalize",
                column: "ProgramareID");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatAnaliza_BuletinAnalizeID",
                table: "RezultatAnaliza",
                column: "BuletinAnalizeID");

            migrationBuilder.CreateIndex(
                name: "IX_RezultatAnaliza_TipAnalizaID",
                table: "RezultatAnaliza",
                column: "TipAnalizaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezultatAnaliza");

            migrationBuilder.DropTable(
                name: "BuletinAnalize");
        }
    }
}
