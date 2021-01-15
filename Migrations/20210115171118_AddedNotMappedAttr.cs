using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class AddedNotMappedAttr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpacedRepetitionAttempts_SpacedRepetitionHistories_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.DropIndex(
                name: "IX_SpacedRepetitionAttempts_SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts");

            migrationBuilder.DropColumn(
                name: "SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpacedRepetitionHistoryId2",
                table: "SpacedRepetitionAttempts",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
