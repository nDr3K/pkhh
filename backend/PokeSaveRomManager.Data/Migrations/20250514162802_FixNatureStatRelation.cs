using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixNatureStatRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Natures_Stats_StatId",
                table: "Natures");

            migrationBuilder.DropForeignKey(
                name: "FK_Natures_Stats_StatId1",
                table: "Natures");

            migrationBuilder.DropIndex(
                name: "IX_Natures_StatId",
                table: "Natures");

            migrationBuilder.DropIndex(
                name: "IX_Natures_StatId1",
                table: "Natures");

            migrationBuilder.DropColumn(
                name: "StatId",
                table: "Natures");

            migrationBuilder.DropColumn(
                name: "StatId1",
                table: "Natures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatId",
                table: "Natures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatId1",
                table: "Natures",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Natures_StatId",
                table: "Natures",
                column: "StatId");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_StatId1",
                table: "Natures",
                column: "StatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Natures_Stats_StatId",
                table: "Natures",
                column: "StatId",
                principalTable: "Stats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Natures_Stats_StatId1",
                table: "Natures",
                column: "StatId1",
                principalTable: "Stats",
                principalColumn: "Id");
        }
    }
}
