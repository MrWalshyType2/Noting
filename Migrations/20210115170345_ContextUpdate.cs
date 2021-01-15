using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class ContextUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.DropIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.RenameColumn(
                name: "SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts",
                newName: "SpacedRepetitionHistoryId2");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId2",
                unique: true,
                filter: "[SpacedRepetitionHistoryId2] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId2",
                principalTable: "SpacedRepetitionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.DropIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.RenameColumn(
                name: "SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts",
                newName: "SpacedRepetitionHistoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId1",
                unique: true,
                filter: "[SpacedRepetitionHistoryId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId1",
                table: "SpacedRepetitionAttempts",
                column: "SpacedRepetitionHistoryId1",
                principalTable: "SpacedRepetitionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
