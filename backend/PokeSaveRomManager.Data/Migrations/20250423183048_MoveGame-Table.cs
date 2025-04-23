using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoveGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveGame_Games_GameId",
                table: "MoveGame");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveGame_Moves_MoveId",
                table: "MoveGame");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGame_MoveGameId",
                table: "MoveLearning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveGame",
                table: "MoveGame");

            migrationBuilder.RenameTable(
                name: "MoveGame",
                newName: "MoveGames");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGame_MoveId",
                table: "MoveGames",
                newName: "IX_MoveGames_MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGame_GameId",
                table: "MoveGames",
                newName: "IX_MoveGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveGames",
                table: "MoveGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGames_Games_GameId",
                table: "MoveGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGames_Moves_MoveId",
                table: "MoveGames",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_MoveGames_MoveGameId",
                table: "MoveLearning",
                column: "MoveGameId",
                principalTable: "MoveGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveGames_Games_GameId",
                table: "MoveGames");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveGames_Moves_MoveId",
                table: "MoveGames");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGames_MoveGameId",
                table: "MoveLearning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveGames",
                table: "MoveGames");

            migrationBuilder.RenameTable(
                name: "MoveGames",
                newName: "MoveGame");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGames_MoveId",
                table: "MoveGame",
                newName: "IX_MoveGame_MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGames_GameId",
                table: "MoveGame",
                newName: "IX_MoveGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveGame",
                table: "MoveGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGame_Games_GameId",
                table: "MoveGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGame_Moves_MoveId",
                table: "MoveGame",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_MoveGame_MoveGameId",
                table: "MoveLearning",
                column: "MoveGameId",
                principalTable: "MoveGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
