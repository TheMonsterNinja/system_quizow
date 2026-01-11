using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWspolne.Migrations
{
    /// <inheritdoc />
    public partial class RelacjeQuizPytania : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odpowiedzi_Pytania_PytanieId",
                table: "Odpowiedzi");

            migrationBuilder.DropIndex(
                name: "IX_Odpowiedzi_PytanieId",
                table: "Odpowiedzi");

            migrationBuilder.DropColumn(
                name: "PytanieId",
                table: "Odpowiedzi");

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedzi_QuestionId",
                table: "Odpowiedzi",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odpowiedzi_Pytania_QuestionId",
                table: "Odpowiedzi",
                column: "QuestionId",
                principalTable: "Pytania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odpowiedzi_Pytania_QuestionId",
                table: "Odpowiedzi");

            migrationBuilder.DropIndex(
                name: "IX_Odpowiedzi_QuestionId",
                table: "Odpowiedzi");

            migrationBuilder.AddColumn<int>(
                name: "PytanieId",
                table: "Odpowiedzi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedzi_PytanieId",
                table: "Odpowiedzi",
                column: "PytanieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odpowiedzi_Pytania_PytanieId",
                table: "Odpowiedzi",
                column: "PytanieId",
                principalTable: "Pytania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
