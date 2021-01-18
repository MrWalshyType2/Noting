using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class AddedCreatedAtToSpacedRepetitionHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpacedRepetitionHistories_Note_NoteId",
                table: "SpacedRepetitionHistories");

            migrationBuilder.DropIndex(
                name: "IX_SpacedRepetitionHistories_NoteId",
                table: "SpacedRepetitionHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "SpacedRepetitionHistories",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NoteId",
                table: "SpacedRepetitionHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SpacedRepetitionHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionHistories_NoteId",
                table: "SpacedRepetitionHistories",
                column: "NoteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpacedRepetitionHistories_Note_NoteId",
                table: "SpacedRepetitionHistories",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpacedRepetitionHistories_Note_NoteId",
                table: "SpacedRepetitionHistories");

            migrationBuilder.DropIndex(
                name: "IX_SpacedRepetitionHistories_NoteId",
                table: "SpacedRepetitionHistories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SpacedRepetitionHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "SpacedRepetitionHistories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "NoteId",
                table: "SpacedRepetitionHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionHistories_NoteId",
                table: "SpacedRepetitionHistories",
                column: "NoteId",
                unique: true,
                filter: "[NoteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SpacedRepetitionHistories_Note_NoteId",
                table: "SpacedRepetitionHistories",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
