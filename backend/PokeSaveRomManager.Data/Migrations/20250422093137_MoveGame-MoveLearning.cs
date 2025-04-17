using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveGameMoveLearning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_Games_GameId",
                table: "MoveLearning");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_Moves_MoveId",
                table: "MoveLearning");

            migrationBuilder.DropIndex(
                name: "IX_MoveLearning_GameId",
                table: "MoveLearning");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "MoveLearning");

            migrationBuilder.RenameColumn(
                name: "MoveId",
                table: "MoveLearning",
                newName: "MoveGameId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveLearning_MoveId",
                table: "MoveLearning",
                newName: "IX_MoveLearning_MoveGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_MoveGame_MoveGameId",
                table: "MoveLearning",
                column: "MoveGameId",
                principalTable: "MoveGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGame_MoveGameId",
                table: "MoveLearning");

            migrationBuilder.RenameColumn(
                name: "MoveGameId",
                table: "MoveLearning",
                newName: "MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveLearning_MoveGameId",
                table: "MoveLearning",
                newName: "IX_MoveLearning_MoveId");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "MoveLearning",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearning_GameId",
                table: "MoveLearning",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_Games_GameId",
                table: "MoveLearning",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_Moves_MoveId",
                table: "MoveLearning",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
