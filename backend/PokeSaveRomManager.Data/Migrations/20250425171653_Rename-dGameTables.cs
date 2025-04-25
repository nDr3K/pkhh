using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeSaveRomManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedGameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveGames_Games_GameId",
                table: "MoveGames");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveGames_Moves_MoveId",
                table: "MoveGames");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_MoveGames_GameInMoveId",
                table: "MoveLearning");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeGames_Games_GameId",
                table: "TypeGames");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeGames_Types_TypeId",
                table: "TypeGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeGames",
                table: "TypeGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveGames",
                table: "MoveGames");

            migrationBuilder.RenameTable(
                name: "TypeGames",
                newName: "GameTypes");

            migrationBuilder.RenameTable(
                name: "MoveGames",
                newName: "GameMoves");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGames_TypeId",
                table: "GameTypes",
                newName: "IX_GameTypes_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TypeGames_GameId",
                table: "GameTypes",
                newName: "IX_GameTypes_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGames_MoveId",
                table: "GameMoves",
                newName: "IX_GameMoves_MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveGames_GameId",
                table: "GameMoves",
                newName: "IX_GameMoves_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTypes",
                table: "GameTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameMoves",
                table: "GameMoves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMoves_Games_GameId",
                table: "GameMoves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameMoves_Moves_MoveId",
                table: "GameMoves",
                column: "MoveId",
                principalTable: "Moves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTypes_Games_GameId",
                table: "GameTypes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTypes_Types_TypeId",
                table: "GameTypes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveLearning_GameMoves_GameInMoveId",
                table: "MoveLearning",
                column: "GameInMoveId",
                principalTable: "GameMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMoves_Games_GameId",
                table: "GameMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_GameMoves_Moves_MoveId",
                table: "GameMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTypes_Games_GameId",
                table: "GameTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTypes_Types_TypeId",
                table: "GameTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveLearning_GameMoves_GameInMoveId",
                table: "MoveLearning");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTypes",
                table: "GameTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameMoves",
                table: "GameMoves");

            migrationBuilder.RenameTable(
                name: "GameTypes",
                newName: "TypeGames");

            migrationBuilder.RenameTable(
                name: "GameMoves",
                newName: "MoveGames");

            migrationBuilder.RenameIndex(
                name: "IX_GameTypes_TypeId",
                table: "TypeGames",
                newName: "IX_TypeGames_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTypes_GameId",
                table: "TypeGames",
                newName: "IX_TypeGames_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMoves_MoveId",
                table: "MoveGames",
                newName: "IX_MoveGames_MoveId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMoves_GameId",
                table: "MoveGames",
                newName: "IX_MoveGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeGames",
                table: "TypeGames",
                column: "Id");

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
                name: "FK_MoveLearning_MoveGames_GameInMoveId",
                table: "MoveLearning",
                column: "GameInMoveId",
                principalTable: "MoveGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGames_Games_GameId",
                table: "TypeGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeGames_Types_TypeId",
                table: "TypeGames",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
