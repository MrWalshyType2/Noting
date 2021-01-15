using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class AddedSpacedRepetitionObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteRelation_Note_ParentId",
                table: "NoteRelation");

            migrationBuilder.DropIndex(
                name: "IX_NoteRelation_ParentId",
                table: "NoteRelation");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "NoteRelation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SpacedRepetitionHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextScheduledAttempt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacedRepetitionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpacedRepetitionHistories_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpacedRepetitionAttempts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpacedRepetitionHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AttemptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpacedRepetitionHistoryId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacedRepetitionAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId",
                        column: x => x.SpacedRepetitionHistoryId,
                        principalTable: "SpacedRepetitionHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId1",
                        column: x => x.SpacedRepetitionHistoryId1,
                        principalTable: "SpacedRepetitionHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteRelation_ChildId",
                table: "NoteRelation",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId1",
                unique: true,
                filter: "[SpacedRepetitionHistoryId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionHistories_NoteId",
                table: "SpacedRepetitionHistories",
                column: "NoteId",
                unique: true,
                filter: "[NoteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRelation_Note_ChildId",
                table: "NoteRelation",
                column: "ChildId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteRelation_Note_ChildId",
                table: "NoteRelation");

            migrationBuilder.DropTable(
                name: "SpacedRepetitionAttempts");

            migrationBuilder.DropTable(
                name: "SpacedRepetitionHistories");

            migrationBuilder.DropIndex(
                name: "IX_NoteRelation_ChildId",
                table: "NoteRelation");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "NoteRelation",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChildId",
                table: "NoteRelation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteRelation_ParentId",
                table: "NoteRelation",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteRelation_Note_ParentId",
                table: "NoteRelation",
                column: "ParentId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
