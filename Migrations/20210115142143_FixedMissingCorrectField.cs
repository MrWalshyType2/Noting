using Microsoft.EntityFrameworkCore.Migrations;

namespace Noting.Migrations
{
    public partial class FixedMissingCorrectField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Correct",
                table: "SpacedRepetitionAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correct",
                table: "SpacedRepetitionAttempts");
        }
    }
}
