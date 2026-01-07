using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorAnalize.Migrations
{
    /// <inheritdoc />
    public partial class ValidarePacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RezultatAnaliza_BuletinAnalize_BuletinAnalizeID",
                table: "RezultatAnaliza");

            migrationBuilder.DropForeignKey(
                name: "FK_RezultatAnaliza_TipAnaliza_TipAnalizaID",
                table: "RezultatAnaliza");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valoare",
                table: "RezultatAnaliza",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unitate",
                table: "RezultatAnaliza",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipAnalizaID",
                table: "RezultatAnaliza",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IntervalReferinta",
                table: "RezultatAnaliza",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BuletinAnalizeID",
                table: "RezultatAnaliza",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prenume",
                table: "Pacient",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Pacient",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CNP",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Denumire",
                table: "PachetAnalize",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_RezultatAnaliza_BuletinAnalize_BuletinAnalizeID",
                table: "RezultatAnaliza",
                column: "BuletinAnalizeID",
                principalTable: "BuletinAnalize",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RezultatAnaliza_TipAnaliza_TipAnalizaID",
                table: "RezultatAnaliza",
                column: "TipAnalizaID",
                principalTable: "TipAnaliza",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RezultatAnaliza_BuletinAnalize_BuletinAnalizeID",
                table: "RezultatAnaliza");

            migrationBuilder.DropForeignKey(
                name: "FK_RezultatAnaliza_TipAnaliza_TipAnalizaID",
                table: "RezultatAnaliza");

            migrationBuilder.AlterColumn<string>(
                name: "Valoare",
                table: "RezultatAnaliza",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Unitate",
                table: "RezultatAnaliza",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "TipAnalizaID",
                table: "RezultatAnaliza",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IntervalReferinta",
                table: "RezultatAnaliza",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<int>(
                name: "BuletinAnalizeID",
                table: "RezultatAnaliza",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Prenume",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Pacient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "CNP",
                table: "Pacient",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Denumire",
                table: "PachetAnalize",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AddForeignKey(
                name: "FK_RezultatAnaliza_BuletinAnalize_BuletinAnalizeID",
                table: "RezultatAnaliza",
                column: "BuletinAnalizeID",
                principalTable: "BuletinAnalize",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RezultatAnaliza_TipAnaliza_TipAnalizaID",
                table: "RezultatAnaliza",
                column: "TipAnalizaID",
                principalTable: "TipAnaliza",
                principalColumn: "ID");
        }
    }
}
