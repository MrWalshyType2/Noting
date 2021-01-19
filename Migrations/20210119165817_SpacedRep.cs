using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class SpacedRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyword_Note_NoteId",
                table: "Keyword");

            migrationBuilder.DropIndex(
                name: "IX_Keyword_NoteId",
                table: "Keyword");

            migrationBuilder.AlterColumn<string>(
                name: "NoteId",
                table: "Keyword",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NoteBoxRelations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteBoxRelations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteBoxRelations");

            migrationBuilder.AlterColumn<string>(
                name: "NoteId",
                table: "Keyword",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_NoteId",
                table: "Keyword",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Note_NoteId",
                table: "Keyword",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
