using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class RemovedParentObjectAccessorFromNoteRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteRelation_Note_ParentId1",
                table: "NoteRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteRelation",
                table: "NoteRelation");

            migrationBuilder.DropIndex(
                name: "IX_NoteRelation_ParentId1",
                table: "NoteRelation");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "NoteRelation");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteRelation",
                table: "NoteRelation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NoteRelation_ParentId",
                table: "NoteRelation",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteRelation",
                table: "NoteRelation");

            migrationBuilder.DropIndex(
                name: "IX_NoteRelation_ParentId",
                table: "NoteRelation");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NoteRelation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ParentId1",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteRelation",
                table: "NoteRelation",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteRelation_ParentId1",
                table: "NoteRelation",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRelation_Note_ParentId1",
                table: "NoteRelation",
                column: "ParentId1",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
