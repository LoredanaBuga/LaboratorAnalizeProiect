using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class ProgramareTipAnaliza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipAnaliza",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipProba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NecesitaNemancat = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipAnaliza", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramareTipAnaliza",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramareID = table.Column<int>(type: "int", nullable: false),
                    TipAnalizaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramareTipAnaliza", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProgramareTipAnaliza_Programare_ProgramareID",
                        column: x => x.ProgramareID,
                        principalTable: "Programare",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramareTipAnaliza_TipAnaliza_TipAnalizaID",
                        column: x => x.TipAnalizaID,
                        principalTable: "TipAnaliza",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramareTipAnaliza_ProgramareID",
                table: "ProgramareTipAnaliza",
                column: "ProgramareID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramareTipAnaliza_TipAnalizaID",
                table: "ProgramareTipAnaliza",
                column: "TipAnalizaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramareTipAnaliza");

            migrationBuilder.DropTable(
                name: "TipAnaliza");
        }
    }
}
