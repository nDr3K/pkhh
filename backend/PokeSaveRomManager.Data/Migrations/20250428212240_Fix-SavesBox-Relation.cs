using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSavesBoxRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Saves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Saves_TeamId",
                table: "Saves",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saves_SaveTeams_TeamId",
                table: "Saves",
                column: "TeamId",
                principalTable: "SaveTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saves_SaveTeams_TeamId",
                table: "Saves");

            migrationBuilder.DropIndex(
                name: "IX_Saves_TeamId",
                table: "Saves");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Saves");
        }
    }
}
