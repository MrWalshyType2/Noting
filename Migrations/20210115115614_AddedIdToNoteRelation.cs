using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class AddedIdToNoteRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "NoteRelation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "NoteRelation");
        }
    }
}
